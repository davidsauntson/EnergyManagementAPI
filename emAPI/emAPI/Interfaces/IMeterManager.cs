/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
 * DAVID SAUNTSON 12993011                                               *
 * MSc Software Engineering - Final Year Project                         *
 *                                                                       *
 * Energy Management API & Software                                      *
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
 * 
 * IMeterManager.cs
 * Code source: Handwritten
 */
		
using System;
using emAPI.ClassLibrary;

namespace emAPI.Controllers
{
    /// <summary>
    /// Interface for MeterManager objects
    /// </summary>
    interface IMeterManager
    {
        void addInvoiceToMeter(int invoiceId, int meterId);
        void addTariffToMeter(int meterId, int tariffId);
        int calculatekWh(MeterReading reading, Meter meter);
        int createElectricityMeter(string serialNo, double scalingFactor, int numbDigits, string startDate, int propertyId);
        int createGasMeter(string serialNo, double meterCoefficient, int numDigits, string startDate, int propertyId);
        int createMeterReading(string date, int reading, int meterId, int propertyId);
        void deleteMeter(int meterId, int propertyId);
        void deleteReading(int readingId, int meterId, int propertyId);
        Meter editMeter(int meterId, string meterJSON);
        void editMeterReading(int readingId, int meterId, string readingJSON, int propertyId);
        double getCO2FromMeter(int meterId, string startDate, string endDate);
        double getDataFromMeter(int meterId, string startDate, string endDate, int dataTypeId);
        System.Collections.Generic.List<Chunk> getDataFromMeterByInterval(int meterId, string startDate, string endDate, int intervalId, int dataTypeId);
        Meter getDetailsForMeter(int meterId);
        string getLastInvoiceDate(int meterId);
        Meter getMeter(int meterId);
        System.Collections.Generic.List<MeterReading> getMeterReadings(int meterId);
        System.Collections.Generic.List<Meter> getMeters(int propertyId);
        string getMinimumReadingDate(int meterId);
        string getMinimumReadingDateForEdit(int meterId);
        MeterReading getReading(int readingId);
    }
}
