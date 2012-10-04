
/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
 * DAVID SAUNTSON 12993011                                               *
 * MSc Software Engineering - Final Year Project                         *
 *                                                                       *
 * Energy Management API & Software                                      *
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
 * 
 * EMConverter.cs
 * Code source: Handwritten
 */
		

using System.IO;
using System.Text;
using System.Xml.Serialization;
using emAPI.ClassLibrary;
using Newtonsoft.Json;

namespace emAPI.Controllers
{
    /// <summary>
    /// Responsible for converting to and from JSON representations.  Contains all static doubles used in coverting to and from kWh and kg CO2.
    /// </summary>
    internal static class EMConverter
    {
        //CONSTANTS FOR CONVERSIONS
        /// <summary>
        /// conversion factor from joules to kWh
        /// </summary>
        public static double fromJoulesTokWh = 3.6; 
        
        /// <summary>
        /// standard calorific value of 1m3 natural gas  
        /// </summary>
        public static double gasCalorificValue = 39.0;  
        
        /// <summary>
        /// standard correction factor for room temperature and pressure
        /// </summary>
        public static double gasCorrectionFactor = 1.02264;

        /// <summary>
        /// CO2 conversion factor kWh natural gas -> kg CO2
        /// </summary>
        public static double gaskWhFactor = 0.1854;      

        /// <summary>
        /// CO2 conversion factor kWh electricity -> kg CO2
        /// </summary>
        public static double eleckWhFactor = 0.544;       


        //CONVERSION METHODS
        /// <summary>
        /// Convert an object of any type to a JSON string
        /// </summary>
        /// <param name="obj">object to convert</param>
        /// <returns>JSON representation of object</returns>
        public static string fromObjectToJSON(object obj)
        {
            ///create the writer object for the seriliser
            ///NB: JSON will be sent to the string builder via the string & JSON writer objects
            StringBuilder JSON = new StringBuilder();
            StringWriter sw = new StringWriter(JSON);

            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                JsonSerializer converter = new JsonSerializer();

                ///ignore recursion loops & only serialise each child object once
                converter.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

                ///convert & return
                converter.Serialize(writer, obj);
            }

            return JSON.ToString();
        }


        /// <summary>
        /// Convert object to XML
        /// </summary>
        /// <param name="obj">object to convert</param>
        /// <returns>XML representation of object</returns>
        public static string fromObjectToXML(object obj)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Tariff));
            using (StringWriter writer = new StringWriter())
            {
                serializer.Serialize(writer, obj);
                return writer.ToString();
            }
        }


        /// <summary>
        /// Convert a JSON string to an object
        /// </summary>
        /// <typeparam name="T">type of object required</typeparam>
        /// <param name="JSON">JSON representation of object</param>
        /// <returns>Object of type T</returns>
        public static T fromJSONtoA<T>(string JSON)
        {
            T obj = JsonConvert.DeserializeObject<T>(JSON);
            return obj;
        }
    }
}
