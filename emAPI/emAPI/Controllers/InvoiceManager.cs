
/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
 * DAVID SAUNTSON 12993011                                               *
 * MSc Software Engineering - Final Year Project                         *
 *                                                                       *
 * Energy Management API & Software                                      *
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
 * 
 * InvoiceManager.cs
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
    /// Controller object for Invoice model object related operations
    /// </summary>
    internal class InvoiceManager : IInvoiceManager
    {
        internal EMMediator mediator;

        internal InvoiceManager()
        {
            mediator = new EMMediator();
        }

        //* * * DATA ACCESS METHODS

        public List<Invoice> getInvoicesForMeter(int meterId)
        {
            List<Invoice> invoices = mediator.DataManager.getInvoicesForMeter(meterId);
            return invoices;
        }

        public Invoice getInvoice(int invoiceId)
        {
            Invoice invoice = mediator.DataManager.getInvoice(invoiceId);
            return invoice;
        }

        public Invoice getLastInvoice(int meterId)
        {
            Meter meter = mediator.DataManager.getMeter(meterId);
            if (meter.Invoices.Count == null | meter.Invoices.Count == 0)
            {
                return null;
            }
            else
            {
                meter.Invoices = meter.Invoices.OrderByDescending(inv => inv.StartDate).ToList();
                return meter.Invoices.ElementAt(0);
            }
        }

        //* * * CREATION METHODS

        public int createInvoice(int meterId, string billDate, string startDate, string endDate, int kWh, 
                                    double consumptionCharge, double standingCharge, double otherCharge, int propertyId)
        {
            Invoice newInvoice = new Invoice
            {
                BillDate = Convert.ToDateTime(billDate),
                StartDate = Convert.ToDateTime(startDate),
                EndDate = Convert.ToDateTime(endDate),

                KWh = kWh,

                ConsumptionCharge = consumptionCharge,
                StandingCharge = standingCharge,
                OtherCharge = otherCharge,
            };

            newInvoice.Meter = mediator.DataManager.getMeter(meterId);
            int invoiceId = mediator.DataManager.saveInvoice(newInvoice);
            mediator.validateInvoice(invoiceId, true);

            mediator.updatePropertyAnnualTotalCost(propertyId);

            return invoiceId;
        }


        //* * * UPDATE METHODS

        public string editInvoice(int invoiceId, string invoiceJSON, int propertyId)
        {
            Invoice updatedInvoice = EMConverter.fromJSONtoA<Invoice>(invoiceJSON);
            updatedInvoice = mediator.DataManager.editInvoice(invoiceId, updatedInvoice);

            ///validate updated invoice
            mediator.validateInvoice(updatedInvoice.Id, true);
            mediator.updatePropertyAnnualTotalCost(propertyId);
            return EMConverter.fromObjectToJSON(updatedInvoice);
        }

        //* * * DELETE METHODS

        public void deleteInvoice(int invoiceId, int propertyId)
        {
            mediator.DataManager.deleteInvoice(invoiceId);
            mediator.updatePropertyAnnualTotalCost(propertyId);
        }




    }
}
