
/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
 * DAVID SAUNTSON 12993011                                               *
 * MSc Software Engineering - Final Year Project                         *
 *                                                                       *
 * Energy Management API & Software                                      *
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
 * 
 * Validator.cs
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
    /// Responsible for consumption validation and basic validation methods.  Super class to other validator types.
    /// </summary>
    internal abstract class Validator
    {
        /// link to validation manager required to get consumption data from readings
        protected ValidationManager validationMgr;

        /// <summary>
        /// Determines whether the cost of the invoice can be validated.
        /// </summary>
        /// <param name="invoice">Invoice to validate</param>
        /// <returns>bool</returns>
        internal static bool canValidateCost(Invoice invoice)
        {
            ///can't be validated if there is no meter associated with the invoice
            if (invoice.Meter == null)
                return false;
            ///can't validate if there are no tariffs on the meter
            if (invoice.Meter.Tariffs == null | invoice.Meter.Tariffs.Count == 0)
                return false;

            ///can't validate if there is no tariff that applies to this invoice
            ///so sort tariffs by oldest first
            invoice.Meter.Tariffs = invoice.Meter.Tariffs.OrderBy(t => t.StartDate).ToList();
            
            ///if the oldest is still newer than the date of the invoice then invoice can be validated
            if (invoice.Meter.Tariffs.ElementAt(0).StartDate > invoice.StartDate)
                return false;

            ///can't validate if there are no bands on the tariff
            if (invoice.Meter.Tariffs.ElementAt(0).Bands == null | invoice.Meter.Tariffs.ElementAt(0).Bands.Count == 0)
                return false;

            return true;
        }


        /// <summary>
        /// Determines if the consumption on the invoice can be validated.
        /// </summary>
        /// <param name="invoice">Invoice to validate</param>
        /// <returns>bool</returns>
        internal static bool canValidateConsumption(Invoice invoice)
        {
            ///can't be validated if there are no meter readings
            if (invoice.Meter.Register == null | invoice.Meter.Register.Count == 0)
                return false;

            return true;
        }

        /// <summary>
        /// Returns the relevant tariff from the meter to which the invoice is attached.  The relevant tariff is the one covering the dates
        /// of the invoice.
        /// </summary>
        /// <param name="invoice">Invoice being validated</param>
        /// <returns>Tariff object used for validation</returns>
        protected virtual Tariff getTariff(Invoice invoice)
        {
            ///order tariffs on meter from newest to oldest
            invoice.Meter.Tariffs = invoice.Meter.Tariffs.OrderByDescending(t => t.StartDate).ToList();

            ///get the first tariff with matching date range for invoice
            Tariff currentTariff = invoice.Meter.Tariffs.First(t => invoice.StartDate >= t.StartDate);
            return currentTariff;
        }

        /// <summary>
        /// Assesses the tariff to determine the daily cost in £ of the standing charge
        /// </summary>
        /// <param name="invoice">Invoice being validated</param>
        /// <returns>double £/day cost of standing charge</returns>
        protected virtual double standingChargeCostPerDay(Invoice invoice)
        {
            Tariff tariff = getTariff(invoice);
            double SCpencePerDay = tariff.SCValue / tariff.SCPeriod.NumbDays * 100;
            return SCpencePerDay;
        }

        /// <summary>
        /// Determines the length of an invoice in days
        /// </summary>
        /// <param name="invoice">Invoice being validated</param>
        /// <returns>int - number of days</returns>
        protected virtual int invoiceLengthInDays(Invoice invoice)
        {
            TimeSpan length = invoice.EndDate - invoice.StartDate;
            return length.Days;
        }

        /// <summary>
        /// Calculates the cost of consumption and standing charge
        /// </summary>
        /// <param name="lengthInDays">int - length of invoice in days</param>
        /// <param name="SCDailyRate">double - standing charge cost in £/day</param>
        /// <param name="kWh">kWh of invoice</param>
        /// <param name="unitRate">unit rate for kWh figure</param>
        /// <returns>double - caluclated cost of invoice, in £</returns>
        protected virtual double calculateCost(double lengthInDays, double SCDailyRate, double kWh, double unitRate)
        {
            return ((lengthInDays * SCDailyRate) + (kWh * unitRate))/100;
        }


        /// <summary>
        /// Validates the consumption kWh firgure of an invoice
        /// </summary>
        /// <param name="invoice">Invoice being validated</param>
        /// <returns>Updated invoice with kWh variation and costIsValid bool</returns>
        internal static Invoice validateConsumption(Invoice invoice)
        {
            if ((bool)invoice.ConsumptionCanBeValidated)
            {
                invoice.ConsumptionCanBeValidated = true;

                ValidationManager validationMgr = new ValidationManager();

                double meteredkWh = validationMgr.getDataFromMeter(invoice.Meter.Id, invoice.StartDate.ToString(), invoice.EndDate.ToString(), (int)DataType.Energy);
                invoice.KWhVariance = invoice.KWh - meteredkWh;

                ///consumption within 15% is considered valid
                ///margin of error due to:
                /// - rounding
                /// - apportionment estimation
                invoice.ConsumptionIsValid = (Math.Abs((decimal)invoice.KWhVariance) < (decimal)(meteredkWh * 0.15));
            
            }

            return invoice;
        }

        internal abstract Invoice validateCost(Invoice invoice);
    }
}
