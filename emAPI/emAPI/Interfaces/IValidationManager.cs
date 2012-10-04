
/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
 * DAVID SAUNTSON 12993011                                               *
 * MSc Software Engineering - Final Year Project                         *
 *                                                                       *
 * Energy Management API & Software                                      *
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
 * 
 * IValidationManager.cs
 * Code source: Handwritten
 */
		

using System;
using emAPI.ClassLibrary;

namespace emAPI.Controllers
{
    /// <summary>
    /// Interface for ValidationManager objects
    /// </summary>
    internal interface IValidationManager
    {
        double getDataFromMeter(int meterId, string startDate, string endDate, int dataTypeId);
        Invoice validateInvoice(Invoice invoice, bool saveAfterValidation);
        string validateInvoice(int invoiceId, bool saveAfterValidation);
    }
}
