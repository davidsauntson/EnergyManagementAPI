
/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
 * DAVID SAUNTSON 12993011                                               *
 * MSc Software Engineering - Final Year Project                         *
 *                                                                       *
 * Energy Management API & Software                                      *
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
 * 
 * ReadingController.cs
 * Code source: Handwritten
 */
		

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using emAPI.ClassLibrary;
using EnergyManagerWebApp.Models;
using EnergyManagerWebApp.Helpers;

namespace EnergyManagerWebApp.Controllers
{
    public class ReadingController : Controller
    {
        emAPIService.IemAPIClient emAPI = new emAPIService.IemAPIClient();

        //
        // GET: /Reading/

        public ActionResult Index(int meterId, int propertyId, string fuel)
        {
            MeterReadingListViewModel model = new MeterReadingListViewModel();
            Meter meter = new Meter();

            try
            {
                ///retreive data form emAPI and set up MeterReadingListViewModel object
                model.Readings = ResponseReader.convertTo<List<MeterReading>>(emAPI.getMeterReadings(meterId));
                meter = ResponseReader.convertTo<Meter>(emAPI.getDetailsForMeter(meterId));

                model.Readings = model.Readings.OrderByDescending(rdg => rdg.Date).ToList();
                model.MeterDetails = new MeterViewModel(meter);
                model.MeterDetails.Meter.Id = meterId;
                model.MeterDetails.Meter.Fuel = fuel;
                model.MeterDetails.BelongsToProperty = propertyId;
                return View(model);
            }
            catch
            {
                return View("Error");
            }

        }


        //
        // GET: /Reading/Create

        public ActionResult Create(int meterId, int propertyId, string fuel)
        {
            try
            {
                ///populate ViewModel object with parameters to allow POST method
                MeterReadingViewModel model = new MeterReadingViewModel();
                model.BelongsToMeter = meterId;
                model.Fuel = fuel;
                model.BelongsToProperty = propertyId;

                string date = ResponseReader.convertTo<string>(emAPI.getMinimumReadingDate(meterId));

                model.MinimumDate = Convert.ToDateTime(date);
                model.Date = model.MinimumDate;
                return PartialView(model);
            }
            catch
            {
                return View("Error");
            }
        } 

        //
        // POST: /Reading/Create

        [HttpPost]
        public ActionResult Create(MeterReadingViewModel model)
        {
            if (ModelState.IsValid)
            {

                ///create reading and check for success
                EMResponse response = JsonConvert.DeserializeObject<EMResponse>(
                    emAPI.createMeterReading(model.Date.ToString(), model.MtrReading.Reading, model.BelongsToMeter, model.BelongsToProperty));

                if (response.status == EMResponse.OK)
                {
                    return RedirectToAction("Home", "Meter", new { meterId = model.BelongsToMeter, propertyId = model.BelongsToProperty, type = model.Fuel });
                }
                else
                {
                    return View("Error");
                }
            }

            ///return model with errors to view for user correction
            return View(model);
        }
        
        //
        // GET: /Reading/Edit/5
 
        public ActionResult Edit(int readingId, int meterId, int propertyId, string fuel)
        {
            try
            {
                ///populate view model with parameters to allow POST method
                MeterReadingViewModel model = new MeterReadingViewModel();

                model.MtrReading = ResponseReader.convertTo<MeterReading>(emAPI.getReading(readingId));

                ///minimum date is date of last reading for edit + one day
                TimeSpan oneDay = new TimeSpan(1, 0, 0, 0);

                string date = ResponseReader.convertTo<string>(emAPI.getMinimumReadingDateForEdit(meterId));
                model.MinimumEditDate = Convert.ToDateTime(date) + oneDay;

                model.BelongsToMeter = meterId;
                model.BelongsToProperty = propertyId;
                return PartialView(model);
            }
            catch
            {
                return View("Error");
            }
        }

        //
        // POST: /Reading/Edit/5

        [HttpPost]
        public ActionResult Edit(MeterReadingViewModel model)
        {
            string readingJSON = JsonConvert.SerializeObject(model.MtrReading);

            ///submit edit and check for success
            EMResponse response = JsonConvert.DeserializeObject<EMResponse>(emAPI.editMeterReading(model.MtrReading.Id, model.BelongsToMeter, 
                                                                                readingJSON, model.BelongsToProperty));

            if (response.status == EMResponse.OK)
            {
                return RedirectToAction("Index", "Reading", new { meterId = model.BelongsToMeter, propertyId = model.BelongsToProperty, fuel = model.Fuel });
            }
            else
            {
                return View("Error");
            }
        }

        //
        // GET: /Reading/Delete/5
 
        public ActionResult Delete(int readingId, int meterId, int propertyId, string fuel)
        {
            try
            {
                ///populate view model with paramters to allow POST method
                MeterReadingViewModel model = new MeterReadingViewModel();
                model.BelongsToMeter = meterId;
                model.BelongsToProperty = propertyId;
                model.Fuel = fuel;

                model.MtrReading = ResponseReader.convertTo<MeterReading>(emAPI.getReading(readingId));
                return PartialView(model);
            }
            catch
            {
                return View("Error");
            }

        }

        //
        // POST: /Reading/Delete/5

        [HttpPost]
        public ActionResult Delete(MeterReadingViewModel model)
        {
            ///submit deletion and check for success
            EMResponse response = JsonConvert.DeserializeObject<EMResponse>(emAPI.deleteReading(model.MtrReading.Id, model.BelongsToMeter, model.BelongsToProperty));

            if (response.status == EMResponse.OK)
            {
                return RedirectToAction("Index", "Reading", new { meterId = model.BelongsToMeter, propertyId = model.BelongsToProperty, fuel = model.Fuel });
            }
            else
            {
                return View("Error");
            }
        }
    }
}
