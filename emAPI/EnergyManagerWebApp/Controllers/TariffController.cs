using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using emAPI.ClassLibrary;
using EnergyManagerWebApp.Models;
using EnergyManagerWebApp.Helpers;
using System.Xml.Serialization;
using System.IO;

namespace EnergyManagerWebApp.Controllers
{
    public class TariffController : Controller
    {
        emAPIService.IemAPIClient emAPI = new emAPIService.IemAPIClient();
        

        //
        // GET: /Tariff/Create

        public ActionResult Create(int meterId, int propertyId, string fuel)
        {
            try
            {
                TariffViewModel model = new TariffViewModel();
                model.fuel = fuel;
                model.meterId = meterId;
                model.BelongsToProperty = propertyId;
                model.MinimumStartDate = Convert.ToDateTime(ResponseReader.convertTo<string>(emAPI.getMinimumTariffDate(meterId)));
                model.StartDate = model.MinimumStartDate;

                ///cannot convert from JSON to a List<Period>, despite JSON being valid.  Therefore SCPeriods is populated on ViewModel creation, see TariffViewModel.cs.
                return View(model);
            }
            catch
            {
                return View("Error");
            }
        } 

        //
        // POST: /Tariff/Create

        [HttpPost]
        public ActionResult Create(TariffViewModel tariffViewModel)
        {
            string fuel = tariffViewModel.fuel;
            int meterId = tariffViewModel.meterId;
            int propertyId = tariffViewModel.BelongsToProperty;

            try
            {
                if (ModelState.IsValid)
                {
                    Tariff tariff = TariffConverter.createTariffFromViewModel(tariffViewModel);

                    EMResponse response = JsonConvert.DeserializeObject<EMResponse>(emAPI.createTariff(tariff.SCValue, tariff.StartDate.ToString(),
                                                                                tariff.SCPeriod.Id, tariff.BandPeriod.Id, tariffViewModel.meterId));

                    if (response.status == EMResponse.OK)
                    {
                        int tariffId = JsonConvert.DeserializeObject<int>(response.data);


                        foreach (TariffBand band in tariff.Bands)
                        {
                            EMResponse bandResponse = JsonConvert.DeserializeObject<EMResponse>(emAPI.createTariffBand(band.UpperkWhLimit, 
                                                                                                                        band.LowerkWhLimit, 
                                                                                                                        band.UnitRate, tariffId));
                            if (bandResponse.status != EMResponse.OK)
                            {
                                return View("Error");
                            }

                        }

                        return RedirectToAction("Home", "Meter", new { meterId = tariffViewModel.meterId, propertyId = tariffViewModel.BelongsToProperty, type = tariffViewModel.fuel });
                    }
                }
                else
                {
                    return View(tariffViewModel);
                }

                return View(tariffViewModel);
            }
            catch
            {
                return View("Error");
            }

        }

        
        //
        // GET: /Tariff/Edit/5
 
        public ActionResult Edit(int tariffId, int meterId, int propertyId, string type)
        {
            try
            {
                TariffViewModel model = new TariffViewModel();

                Tariff tariff = ResponseReader.convertTo<Tariff>(emAPI.getTariff(tariffId));
                model = TariffConverter.createTariffViewFromTariff(tariff, true);
                TimeSpan oneDay = new TimeSpan(1, 0, 0, 0);
                model.MinimumStartDate = Convert.ToDateTime(ResponseReader.convertTo<string>(emAPI.getMinimumTariffDateForEdit(meterId)));
                model.StartDate = model.MinimumStartDate + oneDay;
                model.meterId = meterId;
                model.BelongsToProperty = propertyId;
                model.fuel = type;

                return View(model);
            }
            catch
            {
                return View("Error");
            }
        }

        //
        // POST: /Tariff/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, TariffViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Tariff updatedTariff = TariffConverter.createTariffFromViewModel(model);
                    string updatedTariffJSON = JsonConvert.SerializeObject(updatedTariff);

                    EMResponse response = JsonConvert.DeserializeObject<EMResponse>(emAPI.editTariff(id, updatedTariffJSON));

                    if (response.status == EMResponse.OK)
                    {
                        return RedirectToAction("Home", "Meter", new { meterId = model.meterId, propertyId = model.BelongsToProperty, type = model.fuel });
                    }
                    else
                    {
                        return View("Error");
                    }
                }

                return View(model);
            }
            catch
            {
                return View("Error");
            }
        }

        //
        // GET: /Tariff/Delete/5
 
        public ActionResult Delete(int tariffId, int meterId, int propertyId, string type)
        {
            try
            {
                Tariff tariff = ResponseReader.convertTo<Tariff>(emAPI.getTariff(tariffId));
                TariffViewModel model = TariffConverter.createTariffViewFromTariff(tariff, true);
                model.meterId = meterId;
                model.Id = tariffId;
                model.fuel = type;
                model.BelongsToProperty = propertyId;
                TimeSpan oneDay = new TimeSpan(1, 0, 0, 0);
                model.MinimumStartDate = Convert.ToDateTime(ResponseReader.convertTo<string>(emAPI.getMinimumTariffDate(meterId)));
                model.StartDate = model.MinimumStartDate + oneDay;

                return PartialView(model);
            }
            catch
            {
                return View("Error");
            }
        }

        //
        // POST: /Tariff/Delete/5

        [HttpPost]
        public ActionResult Delete(TariffViewModel model)
        {
            try
            {
                EMResponse response = JsonConvert.DeserializeObject<EMResponse>(emAPI.deleteTariff(model.Id));
                if (response.status == EMResponse.OK)
                {
                    return RedirectToAction("Home", "Meter", new { meterId = model.meterId, propertyId = model.BelongsToProperty, type = model.fuel });
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
