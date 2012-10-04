
/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
 * DAVID SAUNTSON 12993011                                               *
 * MSc Software Engineering - Final Year Project                         *
 *                                                                       *
 * Energy Management API & Software                                      *
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
 * 
 * HomeController.cs
 * Code source: Handwritten
 */
		


using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using emAPI.ClassLibrary;
using Newtonsoft.Json;
using EnergyManagerWebApp.Helpers;

namespace EnergyManagerWebApp.Controllers
{
    public class HomeController : Controller
    {
        emAPIService.IemAPIClient emAPI = new emAPIService.IemAPIClient();
        

        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult UserHome(int id)
        {
            try
            {
                int userId = int.Parse(User.Identity.Name);

                User user = ResponseReader.convertTo<User>(emAPI.getUser(userId));
                return View(user);
            }
            catch
            {
                return View("Error");
            }
        }


        public ActionResult Help()
        {
            return View();
        }
    }
}
