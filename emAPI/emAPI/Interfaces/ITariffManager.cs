
/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
 * DAVID SAUNTSON 12993011                                               *
 * MSc Software Engineering - Final Year Project                         *
 *                                                                       *
 * Energy Management API & Software                                      *
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
 * 
 * ITariffManager.cs
 * Code source: Handwritten
 */
		

using System;
using emAPI.ClassLibrary;

namespace emAPI.Interfaces
{
    /// <summary>
    /// Interface for TariffManager objects
    /// </summary>
    interface ITariffManager
    {
        void addBandToTariff(int bandId, int tariffId);
        int createTariff(double value, string startDate, int standingChargePeriodId, int bandingPeriodId, int meterId);
        int createTariffBand(int uppperLimit, int lowerLimit, double rate, int tariffId);
        void deleteTariff(int tariffId);
        Tariff editTariff(int tariffId, string tariffJSON);
        string getMinimumTariffDate(int meterId);
        string getMinimumTariffDateForEdit(int meterId);
        System.Collections.Generic.List<Period> getSCPeriods();
        Tariff getTariff(int tariffId);
    }
}
