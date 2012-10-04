
/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
 * DAVID SAUNTSON 12993011                                               *
 * MSc Software Engineering - Final Year Project                         *
 *                                                                       *
 * Energy Management API & Software                                      *
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
 * 
 * PropertyController.cs
 * Code source: Handwritten
 */
		

using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using emAPI.ClassLibrary;
using EnergyManagerWebApp.Models;
using EnergyManagerWebApp.Helpers;

namespace EnergyManagerWebApp.Controllers
{
    public class PropertyController : Controller
    {
        emAPIService.IemAPIClient emAPI = new emAPIService.IemAPIClient();

        //
        // GET: /Property/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Property/Details/5

        public ActionResult Home(int id)
        {
            try
            {
                ///update the benchmark stats in emAPI database for benchmark graph
                emAPI.updateBenchmarks();
                PropertyViewModel model = new PropertyViewModel();
                model.Property = ResponseReader.convertTo<Property>(emAPI.getProperty(id));
                
                int propertyTypeId = model.Property.PropertyType.Id;

                ///populate view model object with selection options
                model.Property.PropertyType.Walls = ResponseReader.convertTo<WallType>(emAPI.getWallType(propertyTypeId));
                model.Property.PropertyType.Building = ResponseReader.convertTo<BuildingType>(emAPI.getBuildingType(propertyTypeId));
                model.Property.PropertyType.Heating = ResponseReader.convertTo<HeatingType>(emAPI.getHeatingType(propertyTypeId));
                model.Property.FloorArea = (int)ResponseReader.convertTo<double>(emAPI.getFloorArea(id));

               

                return View(model);
            }
            catch
            {
                return View("Error");
            }

        }

        //
        // GET: /Property/Create

        public ActionResult Create(int userId)
        {
            PropertyViewModel model = new PropertyViewModel();
            model.BelongsToUser = userId;

            try
            {
                ///populate selection options
                model.HeatingTypes = ResponseReader.convertTo<List<HeatingType>>(emAPI.getHeatingTypes());
                model.BuildingTypes = ResponseReader.convertTo<List<BuildingType>>(emAPI.getBuildingTypes());
                model.WallTypes = ResponseReader.convertTo<List<WallType>>(emAPI.getWallTypes());
                return View(model);
            }
            catch
            {
                return View("Error");
            }
        }


        //
        // POST: /Property/Create

        [HttpPost]
        public ActionResult Create(PropertyViewModel model)
        {
            try
            {
                ///property type id
                int typeId = ResponseReader.convertTo<int>(emAPI.getPropertyTypeId(model.HeatingType.Id, model.BuildingType.Id, model.WallType.Id));
                
                ///substitute floor area and number of bedrooms if left empty in form
                int numbRooms = 0;
                int floorArea = 0;

                if (model.NumbBedrooms.HasValue)
                {
                    numbRooms = model.NumbBedrooms.Value;
                }

                if (model.FloorArea.HasValue)
                {
                    floorArea = model.FloorArea.Value;
                }
                

                EMResponse response = JsonConvert.DeserializeObject<EMResponse>(emAPI.createProperty(model.Property.Name, 
                                                                                 model.Property.Postcode, 
                                                                                 floorArea,
                                                                                 numbRooms, 
                                                                                 typeId, int.Parse(User.Identity.Name)));

                if (response.status == EMResponse.OK)
                {
                    return RedirectToAction("UserHome", "Home", new { id = model.BelongsToUser });
                }
                else
                {
                    ///need to populate model with selection lists and return model with errors for users correction
                    model.HeatingTypes = ResponseReader.convertTo<List<HeatingType>>(emAPI.getHeatingTypes());
                    model.BuildingTypes = ResponseReader.convertTo<List<BuildingType>>(emAPI.getBuildingTypes());
                    model.WallTypes = ResponseReader.convertTo<List<WallType>>(emAPI.getWallTypes());
                    return View(model);
                }
            }
            catch
            {
                return View("Error");
            }
        }
        
        //
        // GET: /Property/Edit/5
 
        public ActionResult Edit(int id)
        {
            try
            {
                ///populate selection options
                PropertyViewModel model = new PropertyViewModel();
                model.Property = ResponseReader.convertTo<Property>(emAPI.getProperty(id));
                model.WallTypes = ResponseReader.convertTo<List<WallType>>(emAPI.getWallTypes());
                model.BuildingTypes = ResponseReader.convertTo<List<BuildingType>>(emAPI.getBuildingTypes());
                model.HeatingTypes = ResponseReader.convertTo<List<HeatingType>>(emAPI.getHeatingTypes());

                model.FloorArea = model.Property.FloorArea;
                model.NumbBedrooms = model.Property.NumbBedrooms;

                return View(model);
            }
            catch
            {
                return View("Error");
            }
        }

        //
        // POST: /Property/Edit/5

        [HttpPost]
        public ActionResult Edit(PropertyViewModel model)
        {

            Property property = new Property();
            property.Name = model.Property.Name;

            ///substitute number rooms and floor area if left empty on form
            int numbRooms = 0;
            int floorArea = 0;

            if (model.NumbBedrooms.HasValue)
            {
                numbRooms = model.NumbBedrooms.Value;
            }

            if (model.FloorArea.HasValue)
            {
                floorArea = model.FloorArea.Value;
            }


            ///poplate model object
            property.Postcode = model.Property.Postcode.ToUpper();
            property.NumbBedrooms = numbRooms;
            property.FloorArea = floorArea;

            int heatingType = model.Property.PropertyType.Heating.Id;
            int buildingType = model.Property.PropertyType.Building.Id;
            int wallType = model.Property.PropertyType.Walls.Id;

            try
            {
                ///create newProperty and submit edits.
                int propertyTypeId = ResponseReader.convertTo<int>(emAPI.getPropertyTypeId(heatingType, buildingType, wallType));

                property.PropertyType = new PropertyType { Id = propertyTypeId };

                string propertyJSON = JsonConvert.SerializeObject(property);

                ///check for success
                EMResponse response = JsonConvert.DeserializeObject<EMResponse>(emAPI.editProperty(model.Property.Id, propertyJSON));
                if (response.status == EMResponse.OK)
                {
                    return RedirectToAction("Home", "Property", new { id = model.Property.Id });
                }
                else
                {
                    ///populate model with selection lists and try again
                    model.HeatingTypes = ResponseReader.convertTo<List<HeatingType>>(emAPI.getHeatingTypes());
                    model.BuildingTypes = ResponseReader.convertTo<List<BuildingType>>(emAPI.getBuildingTypes());
                    model.WallTypes = ResponseReader.convertTo<List<WallType>>(emAPI.getWallTypes());
                    return View(model);
                }
            }
            catch
            {
                return View("Error");
            }
        }
     

        //
        // GET: /Property/Delete/5
 
        public ActionResult Delete(int id)
        {
            try
            {
                Property property = new Property();
                property.Id = id;
                return PartialView(property);
            }
            catch
            {
                return View("Error");
            }
        }

        //
        // POST: /Property/Delete/5

        [HttpPost]
        public ActionResult Delete(Property property)
        {
            try
            {
                EMResponse response = JsonConvert.DeserializeObject<EMResponse>(emAPI.deleteProperty(property.Id));
                if (response.status == EMResponse.OK)
                {
                    return RedirectToAction("UserHome", "Home", new { id = int.Parse(User.Identity.Name) });
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
        // GET: /Property/Analysis

        public ActionResult Analysis(int propertyId)
        {
            PropertyViewModel model = new PropertyViewModel();

            try
            {
                model.Property = ResponseReader.convertTo<Property>(emAPI.getProperty(propertyId));
                return View(model);
            }
            catch
            {
                return View("Error");
            }
        }


        /// <summary>
        /// Creates benchmark graph
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public PartialViewResult Graph(int id)
        {
            try
            {
                ///get property object from API
                Property property = ResponseReader.convertTo<Property>(emAPI.getProperty(id));
                BenchmarkProperty benchmark = ResponseReader.convertTo<BenchmarkProperty>(emAPI.getBenchmarkForProperty(id));

                ///previous year's performance
                DateTime end = DateTime.Now;
                TimeSpan oneYear = new TimeSpan(365, 0, 0, 0);
                DateTime start = end - oneYear;

                ///set up graph object
                Graph graph = new Models.Graph();
                graph.Legend = "none";
                graph.vAxisFormat = "#,###";
                graph.vAxisTitle = "Annual kWh";
                graph.Title = "";
                graph.Width = 450;
                graph.Height = 300;
                graph.chartAreaWidth = 300;

                ///create data table
                ArrayList data = new ArrayList
                {
                    new ArrayList { "", "Amount"},
                    new ArrayList { property.Postcode, Math.Ceiling(property.AnnualkWh) },
                    new ArrayList { "Typical Practice", benchmark.BenchmarkkWhTypical },
                    new ArrayList { "Good Practice", benchmark.BenchmarkkWhGood }
                };

                ///serialise string for inclusion into javascript via Graph View
                string graphData = JsonConvert.SerializeObject(data);
                graphData = graphData.Replace("\"", "'");

                graph.Data = graphData;

                return PartialView("GraphFromArray", graph);
            }
            catch
            {
                return PartialView("Error");
            }
        }
    }
}
