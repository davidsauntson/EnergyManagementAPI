
/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
 * DAVID SAUNTSON 12993011                                               *
 * MSc Software Engineering - Final Year Project                         *
 *                                                                       *
 * Energy Management API & Software                                      *
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
 * 
 * EMMediator.cs
 * Code source: Handwritten
 */
		

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using emAPI.DAL;
using emAPI.ClassLibrary;
using emAPI.Interfaces;

namespace emAPI.Controllers
{
    /// <summary>
    /// Mediator to all controller objects.
    /// </summary>
    internal class EMMediator : IEMMediator
    {
        internal EMDataManager DataManager { get; set; }

        private AccountManager accountMgr;
        private AnnotationManager annotationMgr;
        private ApportionmentManager apportionmentMgr;
        private ForecastingManager forecastingMgr;
        private InvoiceManager invoiceMgr;
        private MeterManager meterMgr;
        private PropertyManager propertyMgr;
        private TariffManager tariffMgr;
        private ValidationManager validationMgr;

        internal EMMediator()
        {
            DataManager = new EMDataManager();
        }

        public void addPropertyToUser(int propertyId, int userId)
        {
            DataManager.addPropertyToUser(propertyId, userId);
        }

        //--> PROPERTY MANAGER
        public void updatePropertyAnnualTotalkWh(int propertyId)
        {
            propertyMgr = new PropertyManager();
            propertyMgr.updateAnnualTotalkWh(propertyId);
        }

        public void updatePropertyAnnualTotalCost(int propertyId)
        {
            propertyMgr = new PropertyManager();
            propertyMgr.updateAnnualTotalCost(propertyId);
        }

        public void updateAnnualCO2(int propertyId)
        {
            propertyMgr = new PropertyManager();
            propertyMgr.updateAnnualCO2(propertyId);
        }

        //--> METER MANAGER

        public Meter getMeter(int meterId)
        {
            meterMgr = new MeterManager();
            return meterMgr.getMeter(meterId);
        }

        public double getDataFromMeter(int meterId, string startDate, string endDate, int dataTypeId)
        {
            meterMgr = new MeterManager();
            return meterMgr.getDataFromMeter(meterId, startDate, endDate, dataTypeId);
        }

        public List<Chunk> getDataFromMeterByInterval(int meterId, string startDate, string endDate, int intervalId, int dataTypeId)
        {
            meterMgr = new MeterManager();
            return meterMgr.getDataFromMeterByInterval(meterId, startDate, endDate, intervalId, dataTypeId);
        }

        //public double getCostFromMeter(int meterId, string startDate, string endDate)
        //{
        //    meterMgr = new MeterManager();
        //    return meterMgr.getCostFromMeter(meterId, startDate, endDate);
        //}

        //public List<Chunk> getCostFromMeterByInterval(int meterId, string startDate, string endDate, int intervalId)
        //{
        //    meterMgr = new MeterManager();
        //    return meterMgr.getCostFromMeterByInterval(meterId, startDate, endDate, intervalId);
        //}

        public double getCO2FromMeter(int meterId, string startDate, string endDate)
        {
            meterMgr = new MeterManager();
            return meterMgr.getCO2FromMeter(meterId, startDate, endDate);
        }



        //--> APPORTIONMENT MANAGER

        public List<Chunk> convertReadingsToChunks(List<MeterReading> readings)
        {
            apportionmentMgr = new ApportionmentManager();
            return apportionmentMgr.convertReadingsToChunks(readings);
        }

        public List<Chunk> convertInvoicesToChunks(List<Invoice> invoices)
        {
            apportionmentMgr = new ApportionmentManager();
            return apportionmentMgr.convertInvoicesToChunks(invoices);
        }

        public List<Chunk> apportionToPeriod(List<Chunk> dataIn, DateTime startDate, DateTime endDate,  AppotionmentPeriod interval)
        {
            apportionmentMgr = new ApportionmentManager();
            return apportionmentMgr.apportionToPeriod(dataIn, startDate, endDate, interval);
        }

        public double apportionToDates(List<Chunk> dataIn, string startDate, string endDate)
        {
            apportionmentMgr = new ApportionmentManager();
            return apportionmentMgr.apportionToDates(dataIn, startDate, endDate);
        }

        public List<Chunk> setupDatedChunksForApportionToPeriod(DateTime startDate, DateTime endDate, AppotionmentPeriod interval)
        {
            apportionmentMgr = new ApportionmentManager();
            return apportionmentMgr.setupDatedChunksForApportionToPeriod(startDate, endDate, interval);
        }



        //-->INVOICE MAAGER

        public Invoice getLastInvoice(int meterId)
        {
            invoiceMgr = new InvoiceManager();
            return invoiceMgr.getLastInvoice(meterId);
        }

        //-->VALIDATION MANAGER



        public string validateInvoice(int invoiceId, bool saveAfterValidation)
        {
            validationMgr = new ValidationManager();
            return validationMgr.validateInvoice(invoiceId, saveAfterValidation);
        }

        public Invoice validateInvoice(Invoice invoice, bool safeAfterValidation)
        {
            validationMgr = new ValidationManager();
            return validationMgr.validateInvoice(invoice, safeAfterValidation);
        }
    }
}
