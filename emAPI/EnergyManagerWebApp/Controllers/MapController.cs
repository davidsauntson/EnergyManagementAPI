
/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
 * DAVID SAUNTSON 12993011                                               *
 * MSc Software Engineering - Final Year Project                         *
 *                                                                       *
 * Energy Management API & Software                                      *
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
 * 
 * MapController.cs
 * Code source: Handwritten
 */
		

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using emAPI.ClassLibrary;
using EnergyManagerWebApp.Helpers;

namespace EnergyManagerWebApp.Controllers
{
    public class MapController : Controller
    {
        emAPIService.IemAPIClient emAPI = new emAPIService.IemAPIClient();

        //
        // GET: /Map/

        public ActionResult UserMap()
        {
            try
            {
                ///retrieve user id from cookie
                int userId = int.Parse(User.Identity.Name);

                string userPropertiesJSON = emAPI.getPropertiesForUser(userId);
                List<Property> userProperties = ResponseReader.convertTo<List<Property>>(userPropertiesJSON);

                List<int> propertyIds = new List<int>();
                foreach (Property property in userProperties)
                {
                    propertyIds.Add(property.Id);
                }

                ///get list of anonymous properties for display
                string propertyIdListJSON = JsonConvert.SerializeObject(propertyIds);
                string anonymousPropertiesJSON = emAPI.getAnonymousProperties(propertyIdListJSON);
                List<AnonymousProperty> anonymousProperties = ResponseReader.convertTo<List<AnonymousProperty>>(anonymousPropertiesJSON);

                return PartialView("GoogleMap", anonymousProperties);
            }
            catch
            {
                return View("Error");
            }
        }

    }
}
