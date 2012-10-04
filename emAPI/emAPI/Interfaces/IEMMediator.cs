
/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
 * DAVID SAUNTSON 12993011                                               *
 * MSc Software Engineering - Final Year Project                         *
 *                                                                       *
 * Energy Management API & Software                                      *
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
 * 
 * IEMMediator.cs
 * Code source: Handwritten
 */
		

using System;
using emAPI.ClassLibrary;

namespace emAPI.Interfaces
{
    /// <summary>
    /// Interface for EMMediator objects
    /// </summary>
    internal interface IEMMediator
    {
        void addPropertyToUser(int propertyId, int userId);
        double apportionToDates(System.Collections.Generic.List<Chunk> dataIn, string startDate, string endDate);
        System.Collections.Generic.List<Chunk> apportionToPeriod(System.Collections.Generic.List<Chunk> dataIn, DateTime startDate, DateTime endDate, AppotionmentPeriod interval);
        System.Collections.Generic.List<Chunk> convertInvoicesToChunks(System.Collections.Generic.List<Invoice> invoices);
        System.Collections.Generic.List<Chunk> convertReadingsToChunks(System.Collections.Generic.List<MeterReading> readings);
        double getCO2FromMeter(int meterId, string startDate, string endDate);
        double getDataFromMeter(int meterId, string startDate, string endDate, int dataTypeId);
        System.Collections.Generic.List<Chunk> getDataFromMeterByInterval(int meterId, string startDate, string endDate, int intervalId, int dataTypeId);
        Invoice getLastInvoice(int meterId);
        Meter getMeter(int meterId);
        System.Collections.Generic.List<Chunk> setupDatedChunksForApportionToPeriod(DateTime startDate, DateTime endDate, AppotionmentPeriod interval);
        void updateAnnualCO2(int propertyId);
        void updatePropertyAnnualTotalCost(int propertyId);
        void updatePropertyAnnualTotalkWh(int propertyId);
        Invoice validateInvoice(Invoice invoice, bool safeAfterValidation);
        string validateInvoice(int invoiceId, bool saveAfterValidation);
    }
}
