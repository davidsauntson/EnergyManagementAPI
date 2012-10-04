
/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
 * DAVID SAUNTSON 12993011                                               *
 * MSc Software Engineering - Final Year Project                         *
 *                                                                       *
 * Energy Management API & Software                                      *
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
 * 
 * FroecastingManager.cs
 * Code source: Handwritten
 */
		

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using emAPI.ClassLibrary;
using emAPI.Interfaces;

namespace emAPI.Controllers
{
    /// <summary>
    /// Controller object that manages invoice and consumption forecasting operations.
    /// </summary>
    internal class ForecastingManager : IForecastingManager
    {
        internal EMMediator mediator = new EMMediator();

        /// <summary>
        /// Determines whether the next invoice on a specified meter can be forecasted and if so, will return the forecasted invoice as an Invoice object.
        /// Returns null if an invoice cannot be forecasted.
        /// </summary>
        /// <param name="meterId">id of the meter for which the forecast is required</param>
        /// <returns>Foecasted Invoice object, or null, wrapped in EMResponse.</returns>
        public Invoice forecastNextInvoice(int meterId)
        {
            Invoice lastInvoice = new Invoice();
            try
            {
                lastInvoice = mediator.getLastInvoice(meterId);
            }
            catch
            {
                return null;
            }

            Invoice forecastedInvoice = new Invoice();

            if (canForecast(meterId))
            {
                forecastedInvoice = forecastInvoice(lastInvoice);
            }
            else
            {
                return null;
            }

            return forecastedInvoice;
        }

        /// <summary>
        /// Determines whether an invoice can be forecasted for that meter.  False if:
        ///     - there are no invoices on that meter
        ///     - there are no meter readings on that meter
        ///     - there are no tariffs on the meter
        /// </summary>
        /// <param name="meterId"></param>
        /// <returns>true if incvoice can be forecasted</returns>
        private bool canForecast(int meterId)
        {
            Meter meter = mediator.DataManager.getMeter(meterId);

            ///can't forecast if there are no invoices on the meter
            if (meter.Invoices == null | meter.Invoices.Count == 0)
                return false;

            ///can't forecast if there are no meter readings on meter
            if (meter.Register == null | meter.Register.Count == 0)
                return false;

            ///can't forecast if there are no tariffs on the meter
            if (meter.Tariffs == null | meter.Tariffs.Count == 0)
                return false;

            return true;
        }

        /// <summary>
        /// Generates a 'dummy' invoice with a kWh amount based on change in performance from the same period last year, then uses validator to calculate the cost
        /// of the forecasted consumption & standing charge.  The invoice is not saved to the database.
        /// </summary>
        /// <param name="lastInvoice">most recent invoice on meter, determined using InvoiceManager.getLastInvoice(meterId)</param>
        /// <returns>Invoice object with forecasted data</returns>
        private Invoice forecastInvoice(Invoice lastInvoice)
        {
            Invoice forecastedInvoice = new Invoice();

            TimeSpan lengthOfInvoice = lastInvoice.EndDate - lastInvoice.StartDate;
            TimeSpan oneDay = new TimeSpan(1, 0, 0, 0);

            ///to account for seasonality, determine consumption of the same time period one year ago
            TimeSpan oneYear = new TimeSpan(365, 0, 0, 0);
            DateTime kWhLastYearStart = lastInvoice.StartDate - oneYear;
            DateTime kWhLastYearEnd = lastInvoice.EndDate - oneYear;

            double kWhSameTimeLastYear = mediator.getDataFromMeter(lastInvoice.Meter.Id, kWhLastYearStart.ToString(), kWhLastYearEnd.ToString(), (int)DataType.Energy);
            
            ///determine the variance between the kWh consumed in the two time periods - this accounts for change in performance from then to now
            double variance;

            ///if kWh used during the same period last year is 0, we have no reason to believe the performance has changed, therefore kWh will be the same
            ///and the variance will be one
            ///otherwise the variance is the change in kWh as a percentage of the kWh of the last invoice
            ///where -ve value means kWh has decreased
            if (kWhSameTimeLastYear == 0)
            {
                variance = 1;
            }
            else
            {
                variance = (lastInvoice.KWh - kWhSameTimeLastYear) / (double)lastInvoice.KWh;
            }

            ///apply variance to give forecasted energy consumption for next invoice
            forecastedInvoice.KWh = (int)Math.Round( (double)(lastInvoice.KWh) * variance);

            ///set start and end dates of forecasted invoice
            forecastedInvoice.StartDate = lastInvoice.StartDate + oneDay;
            forecastedInvoice.EndDate = forecastedInvoice.StartDate + lengthOfInvoice;

            ///assign the releveant meter to the invoice as this contains tariff data used to forcast costs
            forecastedInvoice.Meter = lastInvoice.Meter;

            ///use validation manager to populated cost variance attribute
            forecastedInvoice = mediator.validateInvoice(forecastedInvoice, false);


            if (forecastedInvoice.CostCanBeValidated.Value)
            {
                ///use the cost variance to generated forecasted invoice cost
                ///NB cost variance will be negative since calulated cost is bigger than cost on invoice (0)
                forecastedInvoice.ConsumptionCharge = 0 - forecastedInvoice.CostVariance;
                return forecastedInvoice;
            }

            else
            {
                return null;
            }
        }
    }
}
