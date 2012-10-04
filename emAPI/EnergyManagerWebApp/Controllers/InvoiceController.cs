
/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
 * DAVID SAUNTSON 12993011                                               *
 * MSc Software Engineering - Final Year Project                         *
 *                                                                       *
 * Energy Management API & Software                                      *
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
 * 
 * InvoiceController.cs
 * Code source: Handwritten
 */
		

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using emAPI.ClassLibrary;
using EnergyManagerWebApp.Models;
using Newtonsoft.Json;
using EnergyManagerWebApp.Helpers;

namespace EnergyManagerWebApp.Controllers
{
    public class InvoiceController : Controller
    {
        emAPIService.IemAPIClient emAPI = new emAPIService.IemAPIClient();

        //
        // GET: /Invoice/

        public ActionResult Index(int meterId, int propertyId, string fuel)
        {
            try
            {
                InvoiceListViewModel model = new InvoiceListViewModel();
                model.BelongsToProperty = propertyId;
                model.Invoices = ResponseReader.convertTo<List<Invoice>>(emAPI.getInvoicesForMeter(meterId));
                model.Invoices = model.Invoices.OrderByDescending(inv => inv.BillDate).ToList(); 
                model.MeterDetails = ResponseReader.convertTo<Meter>(emAPI.getDetailsForMeter(meterId));
                return View(model);
            }
            catch
            {
                return View("Error");
            }
        }

        //
        // GET: /Invoice/Create

        public ActionResult Create(int meterId, int propertyId, string fuel)
        {
            try
            {
                InvoiceViewModel model = new InvoiceViewModel();
                model.BelongsToProperty = propertyId;
                model.Fuel = fuel;
                model.MeterId = meterId;
                model.Invoice = new Invoice();

                return PartialView(model);
            }
            catch
            {
                return View("Error");
            }
        } 

        //
        // POST: /Invoice/Create

        [HttpPost]
        public ActionResult Create(InvoiceViewModel model)
        {
            try
            {
                int meterId = model.MeterId;
                EMResponse response = JsonConvert.DeserializeObject<EMResponse>(emAPI.createInvoice(meterId, model.Invoice.BillDate.ToString(),
                                                                                model.Invoice.StartDate.ToString(), model.Invoice.EndDate.ToString(),
                                                                                model.Invoice.KWh, model.Invoice.ConsumptionCharge,
                                                                                model.Invoice.StandingCharge, model.Invoice.OtherCharge, model.BelongsToProperty));

                if (response.status == EMResponse.OK)
                {
                    string meterType = model.Fuel;
                    return RedirectToAction("Home", "Meter", new { meterId = model.MeterId, propertyId = model.BelongsToProperty, type = model.Fuel });
                }
                else
                {
                    return View("Error");
                }
            }
            catch
            {
                return View("Error");
            }
        }
        
        //
        // GET: /Invoice/Edit/5
 
        public ActionResult Edit(int invoiceId, int meterId, int propertyId, string fuel)
        {
            try
            {
                InvoiceViewModel model = new InvoiceViewModel();
                model.Invoice = ResponseReader.convertTo<Invoice>(emAPI.getInvoice(invoiceId));
                model.MeterId = meterId;
                model.BelongsToProperty = propertyId;
                model.Fuel = fuel;
                return PartialView(model);
            }
            catch
            {
                return View("Error");
            }
        }

        //
        // POST: /Invoice/Edit/5

        [HttpPost]
        public ActionResult Edit(InvoiceViewModel model)
        {
            try
            {
                model.Invoice.Meter = new Meter();
                model.Invoice.Meter.Id = model.MeterId;

                string invoiceJSON = JsonConvert.SerializeObject(model.Invoice);

                EMResponse response = JsonConvert.DeserializeObject<EMResponse>(emAPI.editInvoice(model.Invoice.Id, invoiceJSON, model.BelongsToProperty));
                if (response.status == EMResponse.OK)
                {
                    return RedirectToAction("Index", "Invoice", new { meterId = model.MeterId, propertyId = model.BelongsToProperty, fuel = model.Fuel });
                }
                else
                {
                    return View("Error");
                }
            }
            catch
            {
                return View("Error");
            }
        }

        //
        // GET: /Invoice/Delete/5
 
        public ActionResult Delete(int invoiceId, int meterId, int propertyId, string fuel)
        {
            try
            {
                InvoiceViewModel model = new InvoiceViewModel();
                string invoiceJSON = emAPI.getInvoice(invoiceId);
                model.Invoice = ResponseReader.convertTo<Invoice>(invoiceJSON);
                model.BelongsToProperty = propertyId;
                model.MeterId = meterId;
                model.Fuel = fuel;
                return PartialView(model);
            }
            catch
            {
                return View("Error");
            }
        }

        //
        // POST: /Invoice/Delete/5

        [HttpPost]
        public ActionResult Delete(InvoiceViewModel model)
        {
            try
            {
                EMResponse response = JsonConvert.DeserializeObject<EMResponse>(emAPI.deleteInvoice(model.Invoice.Id, model.BelongsToProperty));
                if (response.status == EMResponse.OK)
                {
                    return RedirectToAction("Index", "Invoice", new { meterId = model.MeterId, propertyId = model.BelongsToProperty, fuel = model.Fuel });
                }
                else
                {
                    return View("Error");
                }
            }
            catch
            {
                return View("Error");
            }
        }

        [HttpPost]
        public ActionResult Validate(int invoiceId, int meterId, int propertyId, string fuel)
        {
            try
            {
                EMResponse repsonse = JsonConvert.DeserializeObject<EMResponse>(emAPI.validateInvoice(invoiceId, true));
                if (repsonse.status == EMResponse.OK)
                {
                    return RedirectToAction("Index", "Invoice", new { meterId = meterId, propertyId = propertyId, fuel = fuel });
                }
                else
                {
                    return View("Error");
                }
            }
            catch
            {
                return View("Error");
            }
        }
    }
}
