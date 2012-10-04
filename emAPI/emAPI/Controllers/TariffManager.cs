
/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
 * DAVID SAUNTSON 12993011                                               *
 * MSc Software Engineering - Final Year Project                         *
 *                                                                       *
 * Energy Management API & Software                                      *
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
 * 
 * TariffManager.cs
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
    /// Controller object for Tariff model object related operations
    /// </summary>
    internal class TariffManager : ITariffManager
    {
        internal EMMediator mediator;

        internal TariffManager()
        {
            mediator = new EMMediator();
        }

        //* * * DATA ACCESS METHODS

        public Tariff getTariff(int tariffId)
        {
            ///total disconnection from the EF4.1 proxy object is required
            Tariff tariff = mediator.DataManager.getTariff(tariffId);
            return tariff;
        }

        public List<Period> getSCPeriods()
        {
            return mediator.DataManager.getPeriods();
        }


        //* * * CREATION METHODS

        public int createTariff(double value, string startDate, int standingChargePeriodId, int bandingPeriodId, int meterId)
        {
            Tariff tariff = new Tariff
            {
                StartDate = Convert.ToDateTime(startDate),
                SCValue = value
            };

            tariff.SCPeriod = mediator.DataManager.getPeriod(standingChargePeriodId);
            tariff.BandPeriod = mediator.DataManager.getPeriod(bandingPeriodId);
            
            int tariffId = mediator.DataManager.saveTariff(tariff);
            mediator.DataManager.addTariffToMeter(tariffId, meterId);
            return tariffId;
        }

        public int createTariffBand(int uppperLimit, int lowerLimit, double rate, int tariffId)
        {
            TariffBand band = new TariffBand()
            {
                UpperkWhLimit = uppperLimit,
                LowerkWhLimit = lowerLimit,
                UnitRate = rate
            };

            int bandId = mediator.DataManager.saveTariffBand(band);
            addBandToTariff(bandId, tariffId);
            return bandId;
        }


        //* * * UPDATE METHODS

        public Tariff editTariff(int tariffId, string tariffJSON)
        {
            Tariff updatedTariff = EMConverter.fromJSONtoA<Tariff>(tariffJSON);
            updatedTariff = mediator.DataManager.editTariff(tariffId, updatedTariff);
            return updatedTariff;
        }


        //* * * DELETE METHODS

        public void deleteTariff(int tariffId)
        {
            mediator.DataManager.deleteTariff(tariffId);
        }

        //* * * OTHER METHODS

        public void addBandToTariff(int bandId, int tariffId)
        {
            mediator.DataManager.addBandToTariff(bandId, tariffId);
        }

        public string getMinimumTariffDate(int meterId)
        {
            Meter meter = mediator.DataManager.getMeter(meterId);
            DateTime minimumDate = meter.StartDate;

            if (meter.Tariffs.Count != 0)
            {
                foreach (Tariff tariff in meter.Tariffs)
                {
                    if (tariff.StartDate > minimumDate)
                        minimumDate = tariff.StartDate;
                }
            }

            return minimumDate.ToShortDateString();

        }

        public string getMinimumTariffDateForEdit(int meterId)
        {
            Meter meter = mediator.DataManager.getMeter(meterId);
            DateTime minimumDate = meter.StartDate;

            if (meter.Tariffs.Count > 1)
            {
                ///there is more than one meter on the list and therefore the tariff being edited is not the only tariff on the meter
                ///therefore minimum date for editing the tariff is the start date of the second tariff on the list
                meter.Tariffs = meter.Tariffs.OrderByDescending(t => t.StartDate).ToList();
                minimumDate = meter.Tariffs.ElementAt(1).StartDate;
            }

            ///otherwise the minimum start date is the start date of the meter, assigned above
            ///

            return minimumDate.ToString();
        }
    }
}
