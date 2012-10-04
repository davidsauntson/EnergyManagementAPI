
/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
 * DAVID SAUNTSON 12993011                                               *
 * MSc Software Engineering - Final Year Project                         *
 *                                                                       *
 * Energy Management API & Software                                      *
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
 * 
 * IInvoiceManager.cs
 * Code source: Handwritten
 */
		

using System;
using emAPI.ClassLibrary;

namespace emAPI.Controllers
{
    /// <summary>
    /// Interface for InvoiceManager objects
    /// </summary>
    interface IInvoiceManager
    {
        int createInvoice(int meterId, string billDate, string startDate, string endDate, int kWh, double consumptionCharge, double standingCharge, double otherCharge, int propertyId);
        void deleteInvoice(int invoiceId, int propertyId);
        string editInvoice(int invoiceId, string invoiceJSON, int propertyId);
        Invoice getInvoice(int invoiceId);
        System.Collections.Generic.List<Invoice> getInvoicesForMeter(int meterId);
        Invoice getLastInvoice(int meterId);
    }
}
