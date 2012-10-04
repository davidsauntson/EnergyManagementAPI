
/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
 * DAVID SAUNTSON 12993011                                               *
 * MSc Software Engineering - Final Year Project                         *
 *                                                                       *
 * Energy Management API & Software                                      *
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
 * 
 * ConsumptionBandedValidator.cs
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
    /// Responsbible for validating invoices with consumption banded tariffs, ie where the unit rate changes depending
    /// on how much energy has been consumed.  Child of Validator.
    /// </summary>
    internal class ConsumptionBandedValidator : Validator
    {
        //override virtual abstract base class methods

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

        // validation method

        /// <summary>
        /// Validates an invoice that is not on a banded tariff.
        /// </summary>
        /// <param name="invoice">Invoice to validate</param>
        /// <returns>validated Invoice object</returns>
        internal override Invoice validateCost(Invoice invoice)
        {
            if ((bool)invoice.CostCanBeValidated)
            {
                ///ALL FIGURES IN POUNDS
                
                ///get tariff for this invoice
                Tariff tariff = getTariff(invoice);

                ///validate the cost by building up totalConsumptionCost from each band in the tariff
                ///to do this we need to track how much of the total consumption is covered by each tariff
                ///so we know how much applies to the next tariff.
                double remainingConsumption = invoice.KWh;

                double calculatedkWhCost = 0;

                foreach (TariffBand band in tariff.Bands)
                {
                    if (band.UpperkWhLimit != -1)
                    {
                        ///the band contains tariffband data

                        if (band.UpperkWhLimit == 0 | band.UpperkWhLimit > remainingConsumption)
                        {
                            ///there is either no upperlimit for this band, or not enough consumption remains to enter the next band
                            ///therefore all remaining consumption is charged at this rate
                            calculatedkWhCost += remainingConsumption * band.UnitRate;
                        }
                        else
                        {
                            ///upperlimit of this band is less than remaining consumption, therefore charging for the whole band
                            ///adjust remaining consumption
                            remainingConsumption -= band.UpperkWhLimit;

                            ///add cost of consumption in this band to calculated cost
                            calculatedkWhCost += band.UpperkWhLimit * band.UnitRate;
                        }
                    }
                }

                double SCdailyRate = standingChargeCostPerDay(invoice);
                int length = invoiceLengthInDays(invoice);

                double totalCaluclatedCost = (calculatedkWhCost + (SCdailyRate * length)) / 100; ///convert from p to pounds
                invoice.CostVariance = invoice.ConsumptionCharge + invoice.StandingCharge - totalCaluclatedCost;

                ///costs within 2% are considered valid
                invoice.CostIsValid = (Math.Abs((decimal)invoice.CostVariance) < (decimal)(totalCaluclatedCost * 0.02));


                return invoice;
            }

            else
            {
                return null;
            }
        }
    }
}
