
/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
 * DAVID SAUNTSON 12993011                                               *
 * MSc Software Engineering - Final Year Project                         *
 *                                                                       *
 * Energy Management API & Software                                      *
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
 * 
 * ResponseReader.cs - 
 * Code source: Handwritten
 */
		

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using emAPI.ClassLibrary;
using System.Web.Mvc;

namespace EnergyManagerWebApp.Helpers
{
    public static class ResponseReader
    {
        public static T convertTo<T>(string responseJSON)
        {
            EMResponse response = JsonConvert.DeserializeObject<EMResponse>(responseJSON);

            T result = default(T); 

            if (response.status == EMResponse.OK)
            {
                result = JsonConvert.DeserializeObject<T>(response.data);
            }

            return result;
        }
    }
}