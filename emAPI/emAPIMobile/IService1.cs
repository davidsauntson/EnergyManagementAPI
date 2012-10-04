
/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
 * DAVID SAUNTSON 12993011                                               *
 * MSc Software Engineering - Final Year Project                         *
 *                                                                       *
 * Energy Management API & Software                                      *
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
 * 
 * IemAPIMobile.cs - Interface for emAPI Mobile
 * Code source: Handwritten
 */
		

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace emAPIMobile
{
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        string getPropertiesForUser(int userId);

        [OperationContract]
        string getMetersForProperty(int propertyId);

        [OperationContract]
        void createMeterReading(string date, int reading, int meterId, int propertyId);
    }
}
