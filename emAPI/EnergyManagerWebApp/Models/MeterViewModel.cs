
/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
 * DAVID SAUNTSON 12993011                                               *
 * MSc Software Engineering - Final Year Project                         *
 *                                                                       *
 * Energy Management API & Software                                      *
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
 * 
 * MeterViewModel.cs - 
 * Code source: Handwritten
 */
		

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using emAPI.ClassLibrary;
using EnergyManagerWebApp.Helpers;

namespace EnergyManagerWebApp.Models
{
    public class MeterViewModel
    {
        public Meter Meter { get; set; }
        public TariffViewModel Tariff { get; set; }
        public int BelongsToProperty { get; set; }
        public Invoice ForecastedInvoice { get; set; }

        ///elec meter properties
        public double ScalingFactor { get; set; }

        /// gas meter properties
        public string MeterUnits { get; set; }
        public double MeterCoefficient { get; set; }
        public double CorrectionFactor { get; set; }
        public double CalorificValue { get; set; }



        public MeterViewModel()
        { }

        public MeterViewModel(Meter m)
        {
            ///populate fiel specific properties
            ///

            ElectricityMeter elecMeter = m as ElectricityMeter;
            GasMeter gasMeter = m as GasMeter;

            if (elecMeter != null)
            {
                this.ScalingFactor = elecMeter.ScalingFactor;
            }
            else if ( gasMeter != null)
            {
                this.MeterCoefficient = gasMeter.MeterCoefficient;
                this.CorrectionFactor = gasMeter.CorrectionFactor;
                this.CalorificValue = gasMeter.CalorificValue;

                if (gasMeter.MeterCoefficient == 0.028316846)
                {
                    this.MeterUnits = "cubic feet";
                }
                if (gasMeter.MeterCoefficient == 0.28316846)
                {
                    this.MeterUnits = "10s of cubic feet";
                }
                if (gasMeter.MeterCoefficient == 2.8316846)
                {
                    this.MeterUnits = "100s of cubic feet";
                }
                if (gasMeter.MeterCoefficient == 1)
                {
                    this.MeterUnits = "cubic meters";
                }
            }


            ///load tariff view model with data from meter
            ///

            Meter = m;
            Tariff = new TariffViewModel();

            if (Meter.Tariffs.Count == 0)
            {
                ///no tariff set yet
                ///
                Tariff = null;
            }
            else
            {
                ///get most recent tariff
                ///
                Meter.Tariffs = Meter.Tariffs.OrderByDescending(t => t.StartDate).ToList();

                Tariff currentTariff = Meter.Tariffs.ElementAt(0);
                currentTariff.Bands = Meter.Tariffs.ElementAt(0).Bands.ToList();

                ///convert model to viewModel
                ///

                Tariff = TariffConverter.createTariffViewFromTariff(currentTariff, false);
            }
        }

    }
}