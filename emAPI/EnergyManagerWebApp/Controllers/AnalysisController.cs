
/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
 * DAVID SAUNTSON 12993011                                               *
 * MSc Software Engineering - Final Year Project                         *
 *                                                                       *
 * Energy Management API & Software                                      *
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
 * 
 * AnalysisController.cs
 * Code source: Handwritten
 */
		

using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using emAPI.ClassLibrary;
using Newtonsoft.Json;
using EnergyManagerWebApp.Models;
using EnergyManagerWebApp.Helpers;

namespace EnergyManagerWebApp.Controllers
{
    public class AnalysisController : Controller
    {
        emAPIService.IemAPIClient emAPI = new emAPIService.IemAPIClient();

        //
        // GET: /Analysis/

        public ViewResult Energy(int propertyId)
        {
            try
            {
                AnalysisOptionsModel model = new AnalysisOptionsModel();
                ///cannot convert from JSON to a List<Period>, despite JSON being valid.  Therefore SCPeriods is populated on AnalysisOptionsModel creation, see AnalysisOptionsModel.cs.

                ///poplate model view with dates and benchmarks for display on analysis page
                string propertyJSON = emAPI.getProperty(propertyId);
                string benchmarkJSON = emAPI.getBenchmarkForProperty(propertyId);
                string userPropertiesJSON = emAPI.getPropertiesForUser(int.Parse(User.Identity.Name));

                model.property = new PropertyViewModel();
                model.property.Property = ResponseReader.convertTo<Property>(propertyJSON);
                model.property.Benchmark = ResponseReader.convertTo<BenchmarkProperty>(benchmarkJSON);
                model.UsersProperties = ResponseReader.convertTo<List<Property>>(userPropertiesJSON);

                model.propertyId = propertyId;
                model.periodId = 3;
                TimeSpan oneYear = new TimeSpan(365, 0, 0, 0);

                string mostRecentReadingDateJSON = emAPI.getMostRecentDate(propertyId, (int)DataType.Energy);
                model.endDate = ResponseReader.convertTo<DateTime>(mostRecentReadingDateJSON);
                model.startDate = model.endDate - oneYear;

                return View(model);
            }
            catch
            {
                return View("Error");
            }
        }

        [HttpPost]
        public ViewResult Energy(AnalysisOptionsModel model)
        {
            try
            {
                ///populate with data as per GET mthod above
                string propertyJSON = emAPI.getProperty(model.propertyId);
                string benchmarkJSON = emAPI.getBenchmarkForProperty(model.propertyId);
                string userPropertiesJSON = emAPI.getPropertiesForUser(int.Parse(User.Identity.Name));

                model.property = new PropertyViewModel();
                model.property.Property = ResponseReader.convertTo<Property>(propertyJSON);
                model.property.Benchmark = ResponseReader.convertTo<BenchmarkProperty>(benchmarkJSON);
                model.UsersProperties = ResponseReader.convertTo<List<Property>>(userPropertiesJSON);


                return View(model);
            }
            catch
            {
                return View("Error");
            }
        }

        public ActionResult CO2(int propertyId)
        {
            try
            {
                AnalysisOptionsModel model = new AnalysisOptionsModel();
                ///cannot convert from JSON to a List<Period>, despite JSON being valid.  Therefore SCPeriods is populated on AnalysisOptionsModel creation, see AnalysisOptionsModel.cs.

                ///populate viewModel object with benchmarks etc
                string propertyJSON = emAPI.getProperty(propertyId);
                string benchmarkJSON = emAPI.getBenchmarkForProperty(propertyId);
                string userPropertiesJSON = emAPI.getPropertiesForUser(int.Parse(User.Identity.Name));

                model.property = new PropertyViewModel();
                model.property.Property = ResponseReader.convertTo<Property>(propertyJSON);
                model.property.Benchmark = ResponseReader.convertTo<BenchmarkProperty>(benchmarkJSON);
                model.UsersProperties = ResponseReader.convertTo<List<Property>>(userPropertiesJSON);

                model.propertyId = propertyId;
                model.periodId = 3;
                TimeSpan oneYear = new TimeSpan(365, 0, 0, 0);

                string mostRecentInvoiceDate = emAPI.getMostRecentDate(propertyId, (int)DataType.Cost);


                model.endDate = ResponseReader.convertTo<DateTime>(mostRecentInvoiceDate);
                model.startDate = model.endDate - oneYear;
                
                return View(model);
            }
            catch
            {
                return View("Error");
            }
        }

        [HttpPost]
        public ViewResult CO2(AnalysisOptionsModel model)
        {
            try
            {
                string propertyJSON = emAPI.getProperty(model.propertyId);
                string benchmarkJSON = emAPI.getBenchmarkForProperty(model.propertyId);
                string userPropertiesJSON = emAPI.getPropertiesForUser(int.Parse(User.Identity.Name));

                model.property = new PropertyViewModel();
                model.property.Property = ResponseReader.convertTo<Property>(propertyJSON);
                model.property.Benchmark = ResponseReader.convertTo<BenchmarkProperty>(benchmarkJSON);
                model.UsersProperties = ResponseReader.convertTo<List<Property>>(userPropertiesJSON);


                return View(model);
            }
            catch
            {
                return View("Error");
            }
        }

        public ActionResult Money(int propertyId)
        {
            try
            {
                ///populate viewModel object with bechmarks etc
                AnalysisOptionsModel model = new AnalysisOptionsModel();

                string propertyJSON = emAPI.getProperty(propertyId);
                string userPropertiesJSON = emAPI.getPropertiesForUser(int.Parse(User.Identity.Name));

                model.property = new PropertyViewModel();
                model.property.Property = ResponseReader.convertTo<Property>(propertyJSON);
                model.UsersProperties = ResponseReader.convertTo<List<Property>>(userPropertiesJSON);

                model.propertyId = propertyId;
                model.periodId = 3;
                TimeSpan oneYear = new TimeSpan(365, 0, 0, 0);

                model.endDate = ResponseReader.convertTo<DateTime>(emAPI.getMostRecentDate(propertyId, (int)DataType.Cost));
                model.startDate = model.endDate - oneYear;
                return View(model);
            }
            catch
            {
                return View("Error");
            }
        }

        [HttpPost]
        public ViewResult Money(AnalysisOptionsModel model)
        {
            try
            {
                string propertyJSON = emAPI.getProperty(model.propertyId);
                string userPropertiesJSON = emAPI.getPropertiesForUser(int.Parse(User.Identity.Name));

                model.property = new PropertyViewModel();
                model.property.Property = ResponseReader.convertTo<Property>(propertyJSON);
                model.UsersProperties = ResponseReader.convertTo<List<Property>>(userPropertiesJSON);

                return View(model);
            }
            catch
            {
                return View("Error");
            }
        }


        //GRPAHING METHODS

        /// <summary>
        /// Shows standard coloum graph
        /// </summary>
        /// <param name="options"></param>
        /// <param name="dataTypeId"></param>
        /// <returns></returns>
        public PartialViewResult IntervalGraph(AnalysisOptionsModel options, int dataTypeId)
        {
            ///graphJSON holds data for display by javascript in GraphFromArray.cshtml
            string graphJSON = "";

            try
            {
                ///get list of chunks for required period from API
                int propertyId = options.propertyId;
                string startDate = options.startDate.ToShortDateString();
                string endDate = options.endDate.ToShortDateString();
                int intervalId = options.periodId;

                graphJSON = emAPI.getDataAtProperty(propertyId, startDate, endDate, intervalId, dataTypeId);
            }
            catch
            {
                return PartialView("Error");
            }


            ///go to error page if not enough data
            List<Chunk> graphData = ResponseReader.convertTo<List<Chunk>>(graphJSON);
            if (graphData == null)
            {
                return PartialView("NotEnoughDataError");
            }

            ///create header of datatable
            ArrayList data = new ArrayList();
            ArrayList header = new ArrayList { "Date", "Amount" };
            data.Add(header);

            ///create variable used to assess whether any data has been returned.
            double totalAmount = 0;

            ///add each row to datatable
            foreach (Chunk chunk in graphData)
            {
                totalAmount += chunk.Amount;

                ArrayList point = new ArrayList();
                point.Add(chunk.EndDate.ToShortDateString());
                point.Add(Math.Round(chunk.Amount, 2));
                data.Add(point);
            }

            string json = JsonConvert.SerializeObject(data).ToString();
            json = json.Replace("\"", "'");

            ///configure graph
            Graph graph = new Graph();
            graph.Data = json;
            graph.Title = "";
            graph.Width = 900;
            graph.Height = 400;
            graph.hAxisTitle = "Interval Ending";
            graph.hAxisFormat = "";
            graph.vAxisFormat = "#,###";
            graph.Legend = "none";
            graph.Baseline = 0;
            graph.chartAreaWidth = 750;

            ///populate graph title with switch
            switch (options.periodId)
            {
                case 1:
                    graph.vAxisTitle = "Daily kWh";
                    break;

                case 2:
                    graph.vAxisTitle = "Weekly kWh";
                    break;

                case 3:
                    graph.vAxisTitle = "Monthly kWh";
                    break;

                case 4:
                    graph.vAxisTitle = "Quarterly kWh";
                    break;

                case 5:
                    graph.vAxisTitle = "Annual kWh";
                    break;

                default:
                    break;
            }


            ///show graph if there is data
            if (totalAmount > 0)
            {
                return PartialView("GraphFromArray", graph);
            }
            else
            {
                ///return error if no data
                return PartialView("NotEnoughDataError");
            }
        }


        public PartialViewResult ComparativeGraph()
        {
            string graphDataJSON = "";
            List<AnonymousProperty> graphData = new List<AnonymousProperty>();

            ArrayList data = new ArrayList();

            try
            {
                graphData = ResponseReader.convertTo<List<AnonymousProperty>>(emAPI.getComparativeCostsForUser(int.Parse(User.Identity.Name)));

                ArrayList header = new ArrayList { "Postcode", "Your Properties", "Other Properties" };
                data.Add(header);
            }
            catch
            {
                return PartialView("Error");
            }

            if (graphData == null | graphData.Count == 0)
            {
                return PartialView("NotEnoughDataError"); 
            }

            double totalAmount = 0;

            foreach (AnonymousProperty anonProperty in graphData)
            {
                if (anonProperty.AnnualCost != 0)
                {
                    ArrayList point = new ArrayList();
                    ///if the property is the user's, add 0 to other and cost to your

                    point.Add(anonProperty.Postcode);


                    if (anonProperty.isUsers)
                    {
                        point.Add(Math.Round(anonProperty.AnnualCost,2));
                        point.Add(0);
                    }
                    else
                    {
                        ///not the user's, so reverse the order
                        point.Add(0);
                        point.Add(Math.Round(anonProperty.AnnualCost,2));
                    }

                    data.Add(point);

                    totalAmount += anonProperty.AnnualCost;
                }
            }

            if (totalAmount != 0)
            {

                string json = JsonConvert.SerializeObject(data).ToString();
                json = json.Replace("\"", "'");

                Graph graph = new Graph();
                graph.Data = json;
                graph.Title = "";
                graph.Width = 900;
                graph.Height = 400;
                graph.hAxisTitle = "Postcode";
                graph.hAxisFormat = "";
                graph.vAxisTitle = "Annual Cost, ₤";
                graph.vAxisFormat = "#,###";
                graph.Legend = "bottom";
                graph.Baseline = 0;
                graph.chartAreaWidth = 750;

                return PartialView("GraphFromArray", graph);
            }
            else
            {
                return PartialView("NotEnoughDataError");
            }

        }
    }
}
