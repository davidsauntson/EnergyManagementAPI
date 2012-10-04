
/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
 * DAVID SAUNTSON 12993011                                               *
 * MSc Software Engineering - Final Year Project                         *
 *                                                                       *
 * Energy Management API & Software                                      *
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
 * 
 * UnbandedValidator.cs
 * Code source: Handwritten
 */
		

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using emAPI.ClassLibrary;

namespace emAPI.Controllers.Validators
{
    /// <summary>
    /// Responsbile for validating invoices on unbanded tariffs (ie where the unit rate does not change).  Child of validator.
    /// </summary>
    internal class UnbandedValidator : Validator
    {
        //override base class methods

        /// <summary>
        /// Returns Validator.calculateCost - see Validator documentation for more details
        /// </summary>
        protected override double calculateCost(double lengthInDays, double SCDailyRate, double kWh, double unitRate)
        {
            return base.calculateCost(lengthInDays, SCDailyRate, kWh, unitRate);
        }

        /// <summary>
        /// Returns Validator.getTariff - see Validator documentation for more details
        /// </summary>
        protected override Tariff getTariff(Invoice invoice)
        {
            return base.getTariff(invoice);
        }

        /// <summary>
        /// Returns Validator.invoiceLengthInDays - see Validator documentation for more details
        /// </summary>
        protected override int invoiceLengthInDays(Invoice invoice)
        {
            return base.invoiceLengthInDays(invoice);
        }

        /// <summary>
        /// Returns Validator.standingChargeCostPerDay - see Validator documentation for more details
        /// </summary>
        protected override double standingChargeCostPerDay(Invoice invoice)
        {
            return base.standingChargeCostPerDay(invoice);
        }

        /// <summary>
        /// Returns the unit rate of the energy for this invoice
        /// </summary>
        /// <param name="invoice">the Invoice being validated</param>
        /// <returns>double containing the unit rate</returns>
        protected double pencePerkWh(Invoice invoice)
        {
            ///call base class to find appropriate triff
            Tariff tariff = getTariff(invoice);

            ///UnbandedValidator has been instantiated so we know the rate is just the UnitRate of the first band
            return tariff.Bands.ElementAt(0).UnitRate;
        }

        // validation method

        /// <summary>
        /// Validates the cost of an invoice on an unbanded tariff.
        /// </summary>
        /// <param name="invoice"></param>
        /// <returns>the updated invoice object</returns>
        internal override Invoice validateCost(Invoice invoice)
        {
            if ((bool)invoice.CostCanBeValidated)
            {
                double unitRate = pencePerkWh(invoice);
                double standingChargeDailyRate = standingChargeCostPerDay(invoice);
                int invoiceDuration = invoiceLengthInDays(invoice);
                int invoicekWh = invoice.KWh;

                double calcCost = calculateCost((double)invoiceDuration, standingChargeDailyRate, invoicekWh, unitRate);
                invoice.CostVariance = (invoice.ConsumptionCharge + invoice.StandingCharge) - calcCost;

                ///costs within 2% are considered valid
                invoice.CostIsValid = (Math.Abs((decimal)invoice.CostVariance) < (decimal)(calcCost * 0.02));


                return invoice;
            }
            else
            {
                return null;
            }
        } 
    }
}
