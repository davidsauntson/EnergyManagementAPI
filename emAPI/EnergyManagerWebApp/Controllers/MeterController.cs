
/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
 * DAVID SAUNTSON 12993011                                               *
 * MSc Software Engineering - Final Year Project                         *
 *                                                                       *
 * Energy Management API & Software                                      *
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
 * 
 * MeterController.cs
 * Code source: 
 */
		

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using EnergyManagerWebApp.Models;
using emAPI.ClassLibrary;
using EnergyManagerWebApp.Helpers;

namespace EnergyManagerWebApp.Controllers
{
    public class MeterController : Controller
    {
        emAPIService.IemAPIClient emAPI = new emAPIService.IemAPIClient();

        //
        // GET: /Meter/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Meter/Details/5

        public ActionResult Home(int meterId, int propertyId, string type)
        {
            if (type == "Electricity")
            {
                try
                {
                    ///get elec meter object from API
                    ElectricityMeter meter = ResponseReader.convertTo<ElectricityMeter>(emAPI.getMeter(meterId));
                    MeterViewModel model = new MeterViewModel(meter);
                    model.BelongsToProperty = propertyId;

                    model.Meter.Invoices = model.Meter.Invoices.OrderByDescending(inv => inv.BillDate).ToList();

                    ///forecast invoice if possible
                    Invoice nextInvoice = ResponseReader.convertTo<Invoice>(emAPI.forecastNextInvoice(meterId));
                    if (nextInvoice != null)
                    {
                        model.ForecastedInvoice = nextInvoice;
                    }

                    return View(model);
                }
                catch
                {
                    return View("Error");
                }
            }
            else if (type == "Gas")
            {

                try
                {
                    ///get gas meter from API
                    GasMeter meter = ResponseReader.convertTo<GasMeter>(emAPI.getMeter(meterId));
                    MeterViewModel model = new MeterViewModel(meter);
                    model.BelongsToProperty = propertyId;

                    model.Meter.Invoices = model.Meter.Invoices.OrderByDescending(inv => inv.BillDate).ToList();

                    ///forecast invoice if possible
                    Invoice nextInvoice = ResponseReader.convertTo<Invoice>(emAPI.forecastNextInvoice(meterId));
                    if (nextInvoice != null)
                    {
                        model.ForecastedInvoice = nextInvoice;
                    }

                    return View(model);
                }
                catch
                {
                    return View("Error");
                }

            }
            else
            {
                ///unrecognised meter type
                ///
                return View("Home", "User");
            }

        }

        //
        // GET: /Meter/Create

        public ActionResult CreateElec(int propertyId)
        {
            try
            {
                ElectricityMeterViewModel model = new ElectricityMeterViewModel();
                model.StartDate = DateTime.Now;
                model.BelongsToProperty = propertyId;
                return View(model);
            }
            catch
            {
                return View("Error");
            }
        }

        //
        // POST: /Meter/Create

        [HttpPost]
        public ActionResult CreateElec(ElectricityMeterViewModel model)
        {
            try
            {
                int propertyId = model.BelongsToProperty;

                ///submit creation and check for success
                EMResponse response = JsonConvert.DeserializeObject<EMResponse>( emAPI.createElectricityMeter(model.Meter.SerialNo, model.Meter.ScalingFactor, 
                                                                                    model.Meter.NumbDigits, model.StartDate.ToString(), propertyId));

                if (response.status == EMResponse.OK)
                {
                    return RedirectToAction("Home", "Property", new { id = propertyId });
                }
                else
                {
                    return View("Error");
                }
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Meter/Create

        public ActionResult CreateGas(int propertyId)
        {
            try
            {
                GasMeterViewModel model = new GasMeterViewModel();
                model.BelongsToProperty = propertyId;
                model.StartDate = DateTime.Now;
                return View(model);
            }
            catch
            {
                return View("Error");
            }
        }

        //
        // POST: /Meter/Create

        [HttpPost]
        public ActionResult CreateGas(GasMeterViewModel model)
        {
            try
            {
                ///submit create and check for success
                int propertyId = model.BelongsToProperty;
                EMResponse response = JsonConvert.DeserializeObject<EMResponse>(emAPI.createGasMeter(model.Meter.SerialNo, model.Meter.MeterCoefficient,
                                                                                  model.Meter.NumbDigits, model.StartDate.ToString(), propertyId));

                if (response.status == EMResponse.OK)
                {
                    return RedirectToAction("Home", "Property", new { id = propertyId });
                }
                else
                {
                    return View("Error");
                }
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Meter/Edit/5
 
        public ActionResult Edit(int meterId, int propertyId, string fuel)
        {
            try
            {
                Meter meter = ResponseReader.convertTo<Meter>(emAPI.getMeter(meterId));
                MeterViewModel model = new MeterViewModel(meter);
                model.BelongsToProperty = propertyId;
                model.Meter.Fuel = fuel;

                return PartialView(model);
            }
            catch
            {
                return View("Error");
            }
        }

        //
        // POST: /Meter/Edit/5

        [HttpPost]
        public ActionResult Edit(MeterViewModel model)
        {
            try
            {
                ///submit edits and check for success
                string meterJSON = JsonConvert.SerializeObject(model.Meter);
                EMResponse response = JsonConvert.DeserializeObject<EMResponse>(emAPI.editMeter(model.Meter.Id, meterJSON));

                if (response.status == EMResponse.OK)
                {
                    return RedirectToAction("Home", "Meter", new { meterId = model.Meter.Id, propertyId = model.BelongsToProperty, type = model.Meter.Fuel });
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
        // GET: /Meter/Delete/5
 
        public ActionResult Delete(int meterId, int propertyId)
        {
            try
            {
                Meter meter = ResponseReader.convertTo<Meter>(emAPI.getMeter(meterId));
                MeterViewModel model = new MeterViewModel(meter);
                model.BelongsToProperty = propertyId;

                return PartialView(model);
            }
            catch
            {
                return View("Error");
            }
        }

        //
        // POST: /Meter/Delete/5

        [HttpPost]
        public ActionResult Delete(MeterViewModel model)
        {
            try
            {
                ///submit delete and check for success
                EMResponse response = JsonConvert.DeserializeObject<EMResponse>(emAPI.deleteMeter(model.Meter.Id, model.BelongsToProperty));
                if (response.status == EMResponse.OK)
                {
                    return RedirectToAction("Home", "Property", new { id = model.BelongsToProperty });
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
