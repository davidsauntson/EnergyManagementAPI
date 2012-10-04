
/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
 * DAVID SAUNTSON 12993011                                               *
 * MSc Software Engineering - Final Year Project                         *
 *                                                                       *
 * Energy Management API & Software                                      *
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
 * 
 * emAPIMobile.cs - Mobile extension of emAPI
 * Code source: Handwritten
 */
		

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Newtonsoft.Json;
using emAPI.ClassLibrary;


namespace emAPIMobile
{
    public class Service1 : IService1
    {

        emAPIService.IemAPIClient emAPI = new emAPIService.IemAPIClient();

       public string getPropertiesForUser(int userId)
        {
            string responseJSON = emAPI.getPropertiesForUser(userId);

            EMResponse response = JsonConvert.DeserializeObject<EMResponse>(responseJSON);
            if (response.status == EMResponse.OK)
            {

                List<Property> properties = JsonConvert.DeserializeObject<List<Property>>(response.data);
                List<dynamic> mobileProperties = new List<dynamic>();

                foreach (Property p in properties)
                {
                    mobileProperties.Add(new { Id = p.Id, Name = p.Name, BelongsToUser = userId });
                }

                return JsonConvert.SerializeObject(mobileProperties);
            }
            else
            {
                return EMResponse.Error.ToString();
            }
        }

        public string getMetersForProperty(int propertyId)
        {

            string responseJSON = emAPI.getProperty(propertyId);
            EMResponse response = JsonConvert.DeserializeObject<EMResponse>(responseJSON);

            if (response.status == EMResponse.OK)
            {

                Property property = JsonConvert.DeserializeObject<Property>(response.data);

                List<dynamic> mobileMeters = new List<dynamic>();

                foreach (Meter m in property.Meters)
                {
                    string minimumReadingDateResponse = emAPI.getMinimumReadingDate(m.Id);

                    EMResponse dateResponse = JsonConvert.DeserializeObject<EMResponse>(minimumReadingDateResponse);
                    if (dateResponse.status == EMResponse.OK)
                    {
                        mobileMeters.Add(new { Id = m.Id, BelongsToProperty = property.Id, SerialNo = m.SerialNo, Fuel = m.Fuel, 
                                                MinimumDate = JsonConvert.DeserializeObject<string>(dateResponse.data), NumbDigits = m.NumbDigits });
                    }
                }

                return JsonConvert.SerializeObject(mobileMeters);
            }
            else
            {
                return EMResponse.Error.ToString();
            }
        }

        public void createMeterReading(string date, int reading, int meterId, int propertyId)
        {
            string response = emAPI.createMeterReading(date, reading, meterId, propertyId);
        }
    }
}
