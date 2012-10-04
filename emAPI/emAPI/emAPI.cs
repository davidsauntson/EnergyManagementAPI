/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
 * DAVID SAUNTSON 12993011                                               *
 * MSc Software Engineering - Final Year Project                         *
 * 	                                                                     *
 * Energy Management API & Software	                                     *
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
 * 
 * emAPI.cs - this is the facade class & entry point to the API
 * Code source: Handwritten
 * 
 */
				

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using emAPI.Controllers;
using Newtonsoft.Json;
using emAPI.ClassLibrary;

namespace emAPI
{
    /// <summary>
    /// Contains all method documentation for public emAPI methods.
    /// </summary>
    class emAPI : IemAPI
    {
        private AccountManager accountMgr;
        private AnnotationManager annotationMgr;
        private ForecastingManager forecastingMgr;
        private InvoiceManager invoiceMgr;
        private MeterManager meterMgr;
        private PropertyManager propertyMgr;
        private TariffManager tariffMgr;
        private ValidationManager validationMgr;


        //status codes
        private int ok = 200;
        private int error = 500;


        //* * * ACCOUNT MANAGER METHODS


        /// <summary>
        /// Checks existence of given username in the database
        /// </summary>
        /// <param name="username"></param>
        /// <returns>bool wrapped in EMResponse object</returns>
        public string usernameIsUnique(string username)
        {
            EMResponse response = new EMResponse();
            try
            {
                accountMgr = new AccountManager();
                response.data = EMConverter.fromObjectToJSON(accountMgr.usernameIsUnique(username));
                response.status = ok;
            }
            catch (Exception e)
            {
                response.status = error;
                response.data = e.Message;
            }

            return EMConverter.fromObjectToJSON(response);
        }

        /// <summary>
        /// Checks existance of given email in the database
        /// </summary>
        /// <param name="email"></param>
        /// <returns>bool wrapped in EMResponse object</returns>
        public string emailIsUnique(string email)
        {
            EMResponse response = new EMResponse();
            try
            {
                accountMgr = new AccountManager();
                response.data = EMConverter.fromObjectToJSON(accountMgr.emailIsUnique(email));
                
                response.status = ok;
            }
            catch (Exception e)
            {
                response.status = error;
                response.data = e.Message;
            }

            return EMConverter.fromObjectToJSON(response);
		
		
        }

        /// <summary>
        /// Creates a user object and saves it to the database.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="forename"></param>
        /// <param name="surname"></param>
        /// <param name="password">NB Plaintext passwords should not be stored in the database.  Consider a salty hash instead.</param>
        /// <param name="email"></param>
        /// <returns>Id of created user - int wrapped in EMResponse object</returns>
        public string createUser(string username, string forename, string surname, string password, string email)
        {
            EMResponse response = new EMResponse();
            try
            {
                accountMgr = new AccountManager();
                response.data = EMConverter.fromObjectToJSON(accountMgr.createUser(forename, surname, email, username, password));
                response.status = ok;
            }
            catch (Exception e)
            {
                response.status = error;
                response.data = e.Message;
            }

            return EMConverter.fromObjectToJSON(response);
		
        }

        /// <summary>
        /// Checks that the password provided matches the username given
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password">NB Plaintext passwords should not be stored in the database.  Consider a salty hash instead.</param>
        /// <returns>Id of user if the match is successful, or 0 if password does not match or user cannot be found.  Both are ints wrapped in EMResponse obect.</returns>
        public string validateUser(string username, string password)
        {
            EMResponse response = new EMResponse();
            try
            {
                accountMgr = new AccountManager();
                response.data = EMConverter.fromObjectToJSON(accountMgr.validateUser(username, password));
                response.status = ok;
            }
            catch (Exception e)
            {
                response.status = error;
                response.data = e.Message;
            }

            return EMConverter.fromObjectToJSON(response);
        }


        /// <summary>
        /// Returns representation of the user object with the specified id.  ALL child objects are returned
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>JSON representation of user object.  String wrapped in EMResponse object.</returns>
        public string getUser(int userId)
        {
            EMResponse response = new EMResponse();
            try
            {
                accountMgr = new AccountManager();
                response.data = EMConverter.fromObjectToJSON(accountMgr.getUser(userId));
                response.status = ok;
            }
            catch (Exception e)
            {
                response.status = error;
                response.data = e.Message;
            }

            return EMConverter.fromObjectToJSON(response);
		
		
        }


        /// <summary>
        /// Updates the user object in the database with the details in the newUser object.  NB Child objects are NOT updated.
        /// </summary>
        /// <param name="userId">id of the user to be updated</param>
        /// <param name="newUser">JSON representation of user object containing the details with which to update the user</param>
        /// <returns>JSON representaion of the updated user (incl child objects).  String wrapped in EMResponse object.</returns>
        public string editUser(int userId, string newUser)
        {
            EMResponse response = new EMResponse();
            try
            {
                accountMgr = new AccountManager();
                response.data = EMConverter.fromObjectToJSON(accountMgr.editUser(userId, newUser));
                response.status = ok;
            }
            catch (Exception e)
            {
                response.status = error;
                response.data = e.Message;
            }

            return EMConverter.fromObjectToJSON(response);
        }


        /// <summary>
        /// Updates the password of the user with given id.  NB ONLY USE THIS METHOD ONCE THE USER HAS BEEN AUTHENTICATED.
        /// </summary>
        /// <param name="userId">Id of user whose password will be updated</param>
        /// <param name="newPassword">Updated password.  NB Plaintext passwords should not be stored in the database.  Consider a salty hash instead.</param>
        /// <returns>Bool as JSON string | represents success of update | wrapped in EMResponse.</returns>
        public string updatePassword(int userId, string newPassword)
        {
            EMResponse response = new EMResponse();
            try
            {
                accountMgr = new AccountManager();
                response.data = EMConverter.fromObjectToJSON(accountMgr.updatePassword(userId, newPassword));
                response.status = ok;
            }
            catch (Exception e)
            {
                response.status = error;
                response.data = e.Message;
            }

            return EMConverter.fromObjectToJSON(response);
        }


        /// <summary>
        /// Retrieves all properties in the database belonging to user with specified id.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>JSON representation of List[Property] objects, wrapped in EMRepsonse object.</returns>
        public string getPropertiesForUser(int userId)
        {
            EMResponse response = new EMResponse();
            try
            {
                accountMgr = new AccountManager();
                response.data = EMConverter.fromObjectToJSON(accountMgr.getPropertiesForUser(userId));
                response.status = ok;
            }
            catch (Exception e)
            {
                response.status = error;
                response.data = e.Message;
            }

            return EMConverter.fromObjectToJSON(response);
		
		
        }


        /// <summary>
        /// Gets the annual energy costs of all properties in the database.  Properties belonging to the specified user are
        /// marked with a bool isUsers (true).
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>JSON representation of List[AnonymousProperty] objects, wrapped in EMReponse object</returns>
        public string getComparativeCostsForUser(int userId)
        {
            EMResponse response = new EMResponse();
            try
            {
                accountMgr = new AccountManager();
                response.data = EMConverter.fromObjectToJSON(accountMgr.getComparativeCostsForUser(userId));
                response.status = ok;
            }
            catch (Exception e)
            {
                response.status = error;
                response.data = e.Message;
            }

            return EMConverter.fromObjectToJSON(response);
		
		
        }


        //* * * PROPERTY MANAGER METHODS

        /// GET METHODS

        /// <summary>
        /// Retreives all property types in the database
        /// </summary>
        /// <returns>JSON representation of list[PropertyType], wrapped in EMRepsonse object</returns>
        public string getPropertyTypes()
        {
            EMResponse response = new EMResponse();
            try
            {
                propertyMgr = new PropertyManager();

                response.data = EMConverter.fromObjectToJSON(propertyMgr.getPropertyTypes());
                response.status = ok;
            }
            catch (Exception e)
            {
                response.status = error;
                response.data = e.Message;
            }

            return EMConverter.fromObjectToJSON(response);
		
		
        }


        /// <summary>
        /// Retreieves the id of the property type with matching walls, heating and building types.
        /// </summary>
        /// <param name="heatingId"></param>
        /// <param name="buildingId"></param>
        /// <param name="wallId"></param>
        /// <returns>Id of the matching property type - int wrapped in EMRepsonse object</returns>
        public string getPropertyTypeId(int heatingId, int buildingId, int wallId)
        {
            EMResponse response = new EMResponse();
            try
            {
                propertyMgr = new PropertyManager();
                response.data = EMConverter.fromObjectToJSON(propertyMgr.getPropertyTypeId(heatingId, buildingId, wallId));
                response.status = ok;
            }
            catch (Exception e)
            {
                response.status = error;
                response.data = e.Message;
            }

            return EMConverter.fromObjectToJSON(response);
		
		
        }


        /// <summary>
        /// Retreives list of all heating types in the database
        /// </summary>
        /// <returns>JSON repsentation of List[HeatingType], wrapped in EMResponse object</returns>
        public string getHeatingTypes()
        {
            EMResponse response = new EMResponse();
            try
            {
                propertyMgr = new PropertyManager();
                response.data = EMConverter.fromObjectToJSON(propertyMgr.getHeatingTypes());
                response.status = ok;
            }
            catch (Exception e)
            {
                response.status = error;
                response.data = e.Message;
            }

            return EMConverter.fromObjectToJSON(response);
		
		
        }


        /// <summary>
        /// Retreives list of all building types in the database
        /// </summary>
        /// <returns>JSON representation of List[BuildingType], wrapped in EMResponse object</returns>
        public string getBuildingTypes()
        {
            EMResponse response = new EMResponse();
            try
            {
                propertyMgr = new PropertyManager();
                response.data = EMConverter.fromObjectToJSON(propertyMgr.getBuildingTypes());
                response.status = ok;
            }
            catch (Exception e)
            {
                response.status = error;
                response.data = e.Message;
            }

            return EMConverter.fromObjectToJSON(response);
        }


        /// <summary>
        /// Retreives list of all wall types in the database
        /// </summary>
        /// <returns>JSOn representation of List[WallType], wrapped in EMResponse object</returns>
        public string getWallTypes()
        {

            EMResponse response = new EMResponse();
            try
            {

                propertyMgr = new PropertyManager();
                response.data = EMConverter.fromObjectToJSON(propertyMgr.getWallTypes());
                response.status = ok;
            }
            catch (Exception e)
            {
                response.status = error;
                response.data = e.Message;
            }

            return EMConverter.fromObjectToJSON(response);
		
		
        }


        /// <summary>
        /// Retreives property with the specified id.
        /// </summary>
        /// <param name="propertyId"></param>
        /// <returns>JSON representaion of Property object, wrapped in EMResponse object.</returns>
        public string getProperty(int propertyId)
        {
            EMResponse response = new EMResponse();
            try
            {
                propertyMgr = new PropertyManager();
                response.data = EMConverter.fromObjectToJSON(propertyMgr.getProperty(propertyId));
                response.status = ok;
            }
            catch (Exception e)
            {
                response.status = error;
                response.data = e.Message;
            }

            return EMConverter.fromObjectToJSON(response);
        }


        /// <summary>
        /// Retreives the BenchmarkProperty for the property with the specified id.
        /// </summary>
        /// <param name="propertyId"></param>
        /// <returns>JSON representation of BecnhmarkProperty object, wrapped in EMResponse obejct</returns>
        public string getBenchmarkForProperty(int propertyId)
        {
            EMResponse response = new EMResponse();
            try
            {
                propertyMgr = new PropertyManager();
                response.data = EMConverter.fromObjectToJSON(propertyMgr.getBenchmarkForProperty(propertyId));
                response.status = ok;
            }
            catch (Exception e)
            {
                response.status = error;
                response.data = e.Message;
            }

            return EMConverter.fromObjectToJSON(response);
        }

        public string getFloorArea(int propertyId)
        {


            EMResponse response = new EMResponse();
            try
            {
                propertyMgr = new PropertyManager();
                
                response.data = EMConverter.fromObjectToJSON(propertyMgr.getFloorArea(propertyId));
                response.status = ok;
            }
            catch (Exception e)
            {
                response.status = error;
                response.data = e.Message;
            }

            return EMConverter.fromObjectToJSON(response);
        }


        /// <summary>
        /// Retreives the total of the specified datatype between two dates at a property (ie the total over all meters).
        /// </summary>
        /// <param name="propertyId"></param>
        /// <param name="startDate">shortdate string</param>
        /// <param name="endDate">shortdate string</param>
        /// <param name="dataTypeId">(int)DataType</param>
        /// <returns>JSON representation of a double, wrapped in EMResponse obejct</returns>
        public string getTotalData(int propertyId, string startDate, string endDate, int dataTypeId)
        {
            EMResponse response = new EMResponse();
            try
            {
                propertyMgr = new PropertyManager();
                response.data = EMConverter.fromObjectToJSON(propertyMgr.getTotalData(propertyId, startDate, endDate, dataTypeId));
                response.status = ok;
            }
            catch (Exception e)
            {
                response.status = error;
                response.data = e.Message;
            }

            return EMConverter.fromObjectToJSON(response);
        }


        /// <summary>
        /// Retreives the total of the specified datatype for electricity between two dates at a property (ie the total over all electricity meters).
        /// </summary>
        /// <param name="propertyId"></param>
        /// <param name="startDate">shortdate string</param>
        /// <param name="endDate">shortdate string</param>
        /// <param name="dataTypeId">(int)DataType</param>
        /// <returns>JSON representation of a double, wrapped in EMResponse obejct</returns>
        public string getElecData(int propertyId, string startDate, string endDate, int dataTypeId)
        {
            EMResponse response = new EMResponse();
            try
            {
                propertyMgr = new PropertyManager();
                response.data = EMConverter.fromObjectToJSON(propertyMgr.getElecData(propertyId, startDate, endDate, dataTypeId));
                response.status = ok;
            }
            catch (Exception e)
            {
                response.status = error;
                response.data = e.Message;
            }

            return EMConverter.fromObjectToJSON(response);
        }


        /// <summary>
        /// Retrieves the total of the specified datatype for gas between two dates at a property (ie the total over all gas meters).
        /// </summary>
        /// <param name="propertyId"></param>
        /// <param name="startDate">shortdate string</param>
        /// <param name="endDate">shortdate string</param>
        /// <param name="dataTypeId">(int)DataType</param>
        /// <returns>JSON representation of a double, wrapped in EMResponse obejct</returns>
        public string getGasData(int propertyId, string startDate, string endDate, int dataTypeId)
        {
            EMResponse response = new EMResponse();
            try
            {
                propertyMgr = new PropertyManager();

                response.data = EMConverter.fromObjectToJSON(propertyMgr.getGasData(propertyId, startDate, endDate, dataTypeId));
                response.status = ok;
            }
            catch (Exception e)
            {
                response.status = error;
                response.data = e.Message;
            }

            return EMConverter.fromObjectToJSON(response);
        }


        /// <summary>
        /// Retreives list of anonymous properties with propert type data included.  isUsers determined by the list of property ids provided.
        /// ie if the id of the anonymous property is in the list provided, .isUsers will be true.
        /// </summary>
        /// <param name="propertyId">JSON representaion of List[int] property ids</param>
        /// <returns>JSON representaion of List[AnonymousProperty], wrapped in EMResponse obejct</returns>
        public string getAnonymousProperties(string propertyId)
        {
            EMResponse response = new EMResponse();
            try
            {
                propertyMgr = new PropertyManager();
                response.data = EMConverter.fromObjectToJSON(propertyMgr.getAnonymousProperties(propertyId));
                response.status = ok;
            }
            catch (Exception e)
            {
                response.status = error;
                response.data = e.Message;
            }

            return EMConverter.fromObjectToJSON(response);
        }


        /// <summary>
        /// Retreives a list of Chunk objects - an apportionment of the specified datatype at the property (ie over all meters) between the start and end dates, 
        /// broken down into the specified interval.  Will return null if the apportionment is not possible.
        /// </summary>
        /// <param name="propertyId"></param>
        /// <param name="startDate">shortdate string</param>
        /// <param name="endDate">shortdate string</param>
        /// <param name="intervalId">id of interval eg monthly, annually etc</param>
        /// <param name="dataTypeId">(int)DataType</param>
        /// <returns>JSON representation of List[Chunk] (or null), wrapped in EMResponse obejct</returns>
        public string getDataAtProperty(int propertyId, string startDate, string endDate, int intervalId, int dataTypeId)
        {
            EMResponse response = new EMResponse();
            try
            {
                propertyMgr = new PropertyManager();
                response.data = EMConverter.fromObjectToJSON(propertyMgr.getDataAtProperty(propertyId, startDate, endDate, intervalId, dataTypeId));
                response.status = ok;
            }
            catch (Exception e)
            {
                response.status = error;
                response.data = e.Message;
            }

            return EMConverter.fromObjectToJSON(response);
        }


        /// <summary>
        /// Retreieves building type of the property with specified id
        /// </summary>
        /// <param name="propertyTypeId"></param>
        /// <returns>JSON representation of BuildingType, wrapped in EMResponse obejct</returns>
        public string getBuildingType(int propertyTypeId)
        {
            EMResponse response = new EMResponse();
            try
            {
                propertyMgr = new PropertyManager();
                response.data = EMConverter.fromObjectToJSON(propertyMgr.getBuildingType(propertyTypeId));
                response.status = ok;
            }
            catch (Exception e)
            {
                response.status = error;
                response.data = e.Message;
            }

            return EMConverter.fromObjectToJSON(response);
        }


        /// <summary>
        /// Retrieves the heating type of the property with the specified id
        /// </summary>
        /// <param name="propertyTypeId"></param>
        /// <returns>JSON representation of HeatingType, wrapped in EMResponse obejct</returns>
        public string getHeatingType(int propertyTypeId)
        {
            EMResponse response = new EMResponse();
            try
            {
                propertyMgr = new PropertyManager();
                response.data = EMConverter.fromObjectToJSON(propertyMgr.getHeatingType(propertyTypeId));
                response.status = ok;
            }
            catch (Exception e)
            {
                response.status = error;
                response.data = e.Message;
            }

            return EMConverter.fromObjectToJSON(response);
        }


        /// <summary>
        /// Retrieves the wall type of the property with the specified id
        /// </summary>
        /// <param name="propertyTypeId"></param>
        /// <returns>JSON representation of WallType, wrapped in EMResponse obejct</returns>
        public string getWallType(int propertyTypeId)
        {
            EMResponse response = new EMResponse();
            try
            {
                propertyMgr = new PropertyManager();
                response.data = EMConverter.fromObjectToJSON(propertyMgr.getWallType(propertyTypeId));
                response.status = ok;
            }
            catch (Exception e)
            {
                response.status = error;
                response.data = e.Message;
            }

            return EMConverter.fromObjectToJSON(response);
        }


        /// <summary>
        /// Retrieves the most recent date of the specified datatype at the property (ie over all meters)
        /// </summary>
        /// <param name="propertyId"></param>
        /// <param name="dataTypeId"></param>
        /// <returns>JSON representaion of date as string, wrapped in EMResponse obejct</returns>
        public string getMostRecentDate(int propertyId, int dataTypeId)
        {
            EMResponse response = new EMResponse();
            try
            {
                propertyMgr = new PropertyManager();
                response.data = EMConverter.fromObjectToJSON(propertyMgr.getMostRecentDate(propertyId, (DataType)dataTypeId));
                response.status = ok;
            }
            catch (Exception e)
            {
                response.status = error;
                response.data = e.Message;
            }

            return EMConverter.fromObjectToJSON(response);
        }

        ///CREATE METHODS

        /// <summary>
        /// Creates a property with the specified parameters
        /// </summary>
        /// <param name="name">Title of the property</param>
        /// <param name="postcode">Postcode</param>
        /// <param name="floorArea">in m2, if left at zero the area of the benchmark property will be used</param>
        /// <param name="numbBedrooms"></param>
        /// <param name="typeId">can be determined using emAPI.getPropertyTypeId(int heatingId, int buildingId, int wallId)</param>
        /// <param name="userId">userId to which the property belongs</param>
        /// <returns>JSON representation of property id - int, wrapped in EMResponse obejct</returns>
        public string createProperty(string name, string postcode, int floorArea, int numbBedrooms, int typeId, int userId)
        {
            EMResponse response = new EMResponse();
            try
            {
                propertyMgr = new PropertyManager();

                response.data = EMConverter.fromObjectToJSON(propertyMgr.createProperty(name, postcode, floorArea, numbBedrooms, typeId, userId));
                response.status = ok;
            }
            catch (Exception e)
            {
                response.status = error;
                response.data = e.Message;
            }

            return EMConverter.fromObjectToJSON(response);		
        }

        /// EDIT METHODS

        /// <summary>
        /// Updates the property with the specifed id with the attributes of the newProperty object.  Child objects are not updated.
        /// </summary>
        /// <param name="propertyId">id of property to update</param>
        /// <param name="newProperty">JSON representation of property with updated details</param>
        /// <returns>JSON representation of updated Property, wrapped in EMResponse obejct</returns>
        public string editProperty(int propertyId, string newProperty)
        {
            EMResponse response = new EMResponse();
            try
            {
                propertyMgr = new PropertyManager();
                response.data = EMConverter.fromObjectToJSON(propertyMgr.editProperty(propertyId, newProperty));
                response.status = ok;
            }
            catch (Exception e)
            {
                response.status = error;
                response.data = e.Message;
            }

            return EMConverter.fromObjectToJSON(response);
        }


        ///DELETE METHODS

        /// <summary>
        /// Removes property with specifed id and all child objects from database
        /// </summary>
        /// <param name="propertyId"></param>
        /// <returns>void, wrapped in EMResponse obejct</returns>
        public string deleteProperty(int propertyId)
        {
            EMResponse response = new EMResponse();
            try
            {
                propertyMgr = new PropertyManager();
                propertyMgr.deleteProperty(propertyId);
                response.status = ok;
            }
            catch (Exception e)
            {
                response.status = error;
                response.data = e.Message;
            }

            return EMConverter.fromObjectToJSON(response);
        }

        ///OTHER
        
        /// <summary>
        /// Updates the best and average benchmarks for all property types in the database.
        /// </summary>
        /// <returns>void, wrapped in EMResponse obejct</returns>
        public string updateBenchmarks()
        {
            EMResponse response = new EMResponse();
            try
            {
                EMDatabaseStats.updateBenchmarkStats();
                response.status = ok;
            }
            catch (Exception e)
            {
                response.status = error;
                response.data = e.Message;
            }

            return EMConverter.fromObjectToJSON(response);
        }


        //* * * METER MANAGER METHODS

        /// GET METHODS
         
        /// <summary>
        /// Retreives the meter with specifed id, along with child objects.
        /// </summary>
        /// <param name="meterId"></param>
        /// <returns>JSON repsentation of Meter object, wrapped in EMResponse obejct</returns>
        public string getMeter(int meterId)
        {
            EMResponse response = new EMResponse();
            try
            {
                meterMgr = new MeterManager();
                response.data = EMConverter.fromObjectToJSON(meterMgr.getMeter(meterId));
                response.status = ok;
            }
            catch (Exception e)
            {
                response.status = error;
                response.data = e.Message;
            }

            return EMConverter.fromObjectToJSON(response);
        }


        /// <summary>
        /// Retreives the reading object with specified id
        /// </summary>
        /// <param name="readingId"></param>
        /// <returns>JSON representation of MeterReading obejct, wrapped in EMResponse obejct</returns>
        public string getReading(int readingId)
        {
            EMResponse response = new EMResponse();
            try
            {
                meterMgr = new MeterManager();
                response.data = EMConverter.fromObjectToJSON(meterMgr.getReading(readingId));
                response.status = ok;
            }
            catch (Exception e)
            {
                response.status = error;
                response.data = e.Message;
            }

            return EMConverter.fromObjectToJSON(response);
        }


        /// <summary>
        /// Retreives all meter readings on the specified meter
        /// </summary>
        /// <param name="meterId"></param>
        /// <returns>JSON representation of List[MeterReading], wrapped in EMResponse obejct</returns>
        public string getMeterReadings(int meterId)
        {
            EMResponse response = new EMResponse();
            try
            {
                meterMgr = new MeterManager();
                response.data = EMConverter.fromObjectToJSON(meterMgr.getMeterReadings(meterId));
                response.status = ok;
            }
            catch (Exception e)
            {
                response.status = error;
                response.data = e.Message;
            }

            return EMConverter.fromObjectToJSON(response);
        }


        /// <summary>
        /// Retreives the minimum date of a new reading, ie the day after the most recent reading on the specified meter
        /// </summary>
        /// <param name="meterId"></param>
        /// <returns>JSON representation of date string, wrapped in EMResponse obejct</returns>
        public string getMinimumReadingDate(int meterId)
        {
            EMResponse response = new EMResponse();
            try
            {
                meterMgr = new MeterManager();
                response.data = EMConverter.fromObjectToJSON(meterMgr.getMinimumReadingDate(meterId));
                response.status = ok;
            }
            catch (Exception e)
            {
                response.status = error;
                response.data = e.Message;
            }

            return EMConverter.fromObjectToJSON(response);
        }


        /// <summary>
        /// Retrieves the minimum date of a reading that's being edited, ie the date before the most recent reading on the specified meter.
        /// </summary>
        /// <param name="meterId"></param>
        /// <returns>JSON representation of date string, wrapped in EMResponse obejct</returns>
        public string getMinimumReadingDateForEdit(int meterId)
        {
            EMResponse response = new EMResponse();
            try
            {
                meterMgr = new MeterManager();
                response.data = EMConverter.fromObjectToJSON(meterMgr.getMinimumReadingDateForEdit(meterId));
                response.status = ok;
            }
            catch (Exception e)
            {
                response.status = error;
                response.data = e.Message;
            }

            return EMConverter.fromObjectToJSON(response);
        }


        /// <summary>
        /// Retrieves the date of the most recent invoice on the specifed meter
        /// </summary>
        /// <param name="meterId"></param>
        /// <returns>JSON representation of date string, wrapped in EMResponse obejct</returns>
        public string getLastInvoiceDate(int meterId)
        {
            EMResponse response = new EMResponse();
            try
            {
                meterMgr = new MeterManager();
                response.data = EMConverter.fromObjectToJSON(meterMgr.getLastInvoiceDate(meterId));
                response.status = ok;
            }
            catch (Exception e)
            {
                response.status = error;
                response.data = e.Message;
            }

            return EMConverter.fromObjectToJSON(response);
        }


        /// <summary>
        /// Retrieves a 'shell' meter object that does not contain any child objects.
        /// </summary>
        /// <param name="meterId"></param>
        /// <returns>JSON representation of List[Meter], containing no child objects, wrapped in EMResponse obejct</returns>
        public string getDetailsForMeter(int meterId)
        {
            EMResponse response = new EMResponse();
            try
            {
                meterMgr = new MeterManager();
                response.data = EMConverter.fromObjectToJSON(meterMgr.getDetailsForMeter(meterId));
                response.status = ok;
            }
            catch (Exception e)
            {
                response.status = error;
                response.data = e.Message;
            }

            return EMConverter.fromObjectToJSON(response);
        }


        /// <summary>
        /// Returns the total of the specifed datatype between the two dates for the specifed meter
        /// </summary>
        /// <param name="meterId"></param>
        /// <param name="startDate">shortdate string</param>
        /// <param name="endDate">shortdate string</param>
        /// <param name="dataTypeId">(int)DataType</param>
        /// <returns>JSON representation of double, wrapped in EMResponse obejct</returns>
        public string getDataFromMeter(int meterId, string startDate, string endDate, int dataTypeId)
        {
            EMResponse response = new EMResponse();
            try
            {
                meterMgr = new MeterManager();
                response.data = EMConverter.fromObjectToJSON(meterMgr.getDataFromMeter(meterId, startDate, endDate, dataTypeId));
                response.status = ok;
            }
            catch (Exception e)
            {
                response.status = error;
                response.data = e.Message;
            }

            return EMConverter.fromObjectToJSON(response);
        }


        /// <summary>
        /// Returns list of Chunk objects that represent the total of the specifed datatype between the provided dates, broken down
        /// into the specified interval, eg monthly, annually etc.
        /// </summary>
        /// <param name="meterId"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="intervalId"></param>
        /// <param name="dataTypeId"></param>
        /// <returns>JSON representation of List[Chunk] obejcts, wrapped in EMResponse obejct</returns>
        public string getDataFromMeterByInterval(int meterId, string startDate, string endDate, int intervalId, int dataTypeId)
        {
            EMResponse response = new EMResponse();
            try
            {
                meterMgr = new MeterManager();
                response.data = EMConverter.fromObjectToJSON(meterMgr.getDataFromMeterByInterval(meterId, startDate, endDate, intervalId, dataTypeId));
                response.status = ok;
            }
            catch (Exception e)
            {
                response.status = error;
                response.data = e.Message;
            }

            return EMConverter.fromObjectToJSON(response);
        }

        

        ///CREATE METHODS
        
        /// <summary>
        /// Creates an electricity meter with the specified attributes
        /// </summary>
        /// <param name="serialNo">string - serial number of the meter</param>
        /// <param name="scalingFactor">int - multiplier for the reading on the meter (eg some meters show x10kWh)</param>
        /// <param name="numbDigits">number of digits on the meter, eg 000123 = 6</param>
        /// <param name="startDate">start date of meter</param>
        /// <param name="propertyId">id of property to which the meter belongs</param>
        /// <returns>JSON representation of id of created meter - int , wrapped in EMResponse obejct</returns>
        public string createElectricityMeter(string serialNo, double scalingFactor, int numbDigits, string startDate, int propertyId)
        {
            EMResponse response = new EMResponse();
            try
            {
                meterMgr = new MeterManager();
                response.data = EMConverter.fromObjectToJSON(meterMgr.createElectricityMeter(serialNo, scalingFactor, numbDigits, startDate, propertyId));
                response.status = ok;
            }
            catch (Exception e)
            {
                response.status = error;
                response.data = e.Message;
            }

            return EMConverter.fromObjectToJSON(response);
        }


        /// <summary>
        /// Creates a gas meter with specified attributes
        /// </summary>
        /// <param name="serialNo">string - serial number</param>
        /// <param name="meterCoefficient">double - used to convert the units on the meter into m3, for onward conversion to kWh</param>
        /// <param name="numbDigits">max no digits on the meter eg 000123 = 6</param>
        /// <param name="startDate">start date of the meter</param>
        /// <param name="propertyId">id of the property to which the mter belongs.</param>
        /// <returns>JSON representation of id of created meter - int, wrapped in EMResponse obejct</returns>
        public string createGasMeter(string serialNo, double meterCoefficient, int numbDigits, string startDate, int propertyId)
        {
            EMResponse response = new EMResponse();
            try
            {
                meterMgr = new MeterManager();
                response.data = EMConverter.fromObjectToJSON(meterMgr.createGasMeter(serialNo, meterCoefficient, numbDigits, startDate, propertyId));
                response.status = ok;
            }
            catch (Exception e)
            {
                response.status = error;
                response.data = e.Message;
            }

            return EMConverter.fromObjectToJSON(response);
        }


        /// <summary>
        /// Creates a meter reading & updates annual CO2 and kWh at the property.
        /// </summary>
        /// <param name="date">Date of reading</param>
        /// <param name="reading">the reading, leadings zeros can be omitted</param>
        /// <param name="meterId">id of the meter to which the reading belongs</param>
        /// <param name="propertyId">id of the property to which the meter belongs.  Required to update the annual totals.</param>
        /// <returns>JSON representation of id of created reading obejct, int , wrapped in EMResponse obejct</returns>
        public string createMeterReading(string date, int reading, int meterId, int propertyId)
        {
            EMResponse response = new EMResponse();
            try
            {
                meterMgr = new MeterManager();
                response.data = EMConverter.fromObjectToJSON(meterMgr.createMeterReading(date, reading, meterId, propertyId));
                response.status = ok;
            }
            catch (Exception e)
            {
                response.status = error;
                response.data = e.Message;
            }

            return EMConverter.fromObjectToJSON(response);
        }



        ////EDIT METHODS
        
        /// <summary>
        /// Updates the meter with the specified id with the details of the newMeter object.  Child objects are not updated.
        /// </summary>
        /// <param name="meterId">id of the meter to update</param>
        /// <param name="newMeter">JSON representation of the meter with new details </param>
        /// <returns>JSON representation of the updated Meter object, wrapped in EMResponse obejct</returns>
        public string editMeter(int meterId, string newMeter)
        {
            EMResponse response = new EMResponse();
            try
            {
                meterMgr = new MeterManager();
                
                meterMgr.editMeter(meterId, newMeter);
                response.status = ok;
            }
            catch (Exception e)
            {
                response.status = error;
                response.data = e.Message;
            }

            return EMConverter.fromObjectToJSON(response);
        }


        /// <summary>
        /// Updates the meter reading with the specifed id with the details in the newMeterReading object & updates the annual totals of the
        /// relevant Property object.
        /// </summary>
        /// <param name="readingId">id of reading to be updated</param>
        /// <param name="meterId">id of meter to which the reading belongs</param>
        /// <param name="newMeterReading">JSON representation of MeterReading object with updated details</param>
        /// <param name="propertyId">id of property to which the meter belongs.  Required to update the annual totals.</param>
        /// <returns>JSON representation of updated MeterReading object, wrapped in EMResponse obejct</returns>
        public string editMeterReading(int readingId, int meterId, string newMeterReading, int propertyId)
        {
            EMResponse response = new EMResponse();
            try
            {
                meterMgr = new MeterManager();
                meterMgr.editMeterReading(readingId, meterId, newMeterReading, propertyId);
                response.status = ok;
            }
            catch (Exception e)
            {
                response.status = error;
                response.data = e.Message;
            }

            return EMConverter.fromObjectToJSON(response);
        }

        ///DELETE METHODS
        
        /// <summary>
        /// Removes the meter with specified id and ALL child objects from the database.
        /// </summary>
        /// <param name="meterId"></param>
        /// <returns>void, wrapped in EMResponse obejct</returns>
        public string deleteMeter(int meterId, int propertyId)
        {
            EMResponse response = new EMResponse();
            try
            {
                meterMgr = new MeterManager();
                meterMgr.deleteMeter(meterId, propertyId);
                response.status = ok;
            }
            catch (Exception e)
            {
                response.status = error;
                response.data = e.Message;
            }

            return EMConverter.fromObjectToJSON(response);
        }


        /// <summary>
        /// Removes the meter reading with the specified id from the database and updates the annual totals for the relvant Property object.
        /// </summary>
        /// <param name="readingId">id of reading to be deleted</param>
        /// <param name="meterId">id of meter to which the reading belongs.  Required to update the remaining readings on the meter.</param>
        /// <returns></returns>
        public string deleteReading(int readingId, int meterId, int propertyId)
        {
            EMResponse response = new EMResponse();
            try
            {
                meterMgr = new MeterManager();
                meterMgr.deleteReading(readingId, meterId, propertyId);

                response.status = ok;
            }
            catch (Exception e)
            {
                response.status = error;
                response.data = e.Message;
            }

            return EMConverter.fromObjectToJSON(response);
        }



        //* * * INVOICE MANAGER METHODS

        /// GET METHODS
        
        /// <summary>
        /// Retrieves all invoices on meter with specified id.
        /// </summary>
        /// <param name="meterId"></param>
        /// <returns>JSON representation of List[Invoice], wrapped in EMResponse obejct</returns>
        public string getInvoicesForMeter(int meterId)
        {
            EMResponse response = new EMResponse();
            try
            {
                invoiceMgr = new InvoiceManager();
                response.data = EMConverter.fromObjectToJSON(invoiceMgr.getInvoicesForMeter(meterId));
                response.status = ok;
            }
            catch (Exception e)
            {
                response.status = error;
                response.data = e.Message;
            }

            return EMConverter.fromObjectToJSON(response);
        }


        /// <summary>
        /// Retreives the invoice with the specified id.
        /// </summary>
        /// <param name="invoiceId"></param>
        /// <returns>JSON representation of Invoice, wrapped in EMResponse obejct</returns>
        public string getInvoice(int invoiceId)
        {
            EMResponse response = new EMResponse();
            try
            {
                invoiceMgr = new InvoiceManager();
                response.data = EMConverter.fromObjectToJSON(invoiceMgr.getInvoice(invoiceId));
                response.status = ok;
            }
            catch (Exception e)
            {
                response.status = error;
                response.data = e.Message;
            }

            return EMConverter.fromObjectToJSON(response);
        }

        ///CREATE METHODS

        /// <summary>
        /// Creates an invoice with the specified parameters and updates the annual cost of the relvant Property object.
        /// </summary>
        /// <param name="meterId">id of the meter to which the invoice belongs</param>
        /// <param name="billDate">the date of the invoice</param>
        /// <param name="startDate">the start date of the consumption being invoiced</param>
        /// <param name="endDate">the end date of the consumption being invoiced</param>
        /// <param name="kWh">the kWh on the invoice</param>
        /// <param name="consumptionCharge">the cost of the energy ONLY (in £)</param>
        /// <param name="standingCharge">the cost of the standing charge ONLY (in £)</param>
        /// <param name="otherCharge">total of all other costs (not used to validate the invoice) (in £)</param>
        /// <param name="propertyId">the id of the property to which the invoice belongs</param>
        /// <returns>JSON representation of the id of the created invoice - int, wrapped in EMResponse object</returns>
        public string createInvoice(int meterId, string billDate, string startDate, string endDate, int kWh,
            double consumptionCharge, double standingCharge, double otherCharge, int propertyId)
        {
            EMResponse response = new EMResponse();
            try
            {
                invoiceMgr = new InvoiceManager();
                response.data = EMConverter.fromObjectToJSON(invoiceMgr.createInvoice(meterId, billDate, startDate, endDate, 
                                                                                        kWh, consumptionCharge, 
                                                                                        standingCharge, otherCharge, propertyId));
                response.status = ok;
            }
            catch (Exception e)
            {
                response.status = error;
                response.data = e.Message;
            }

            return EMConverter.fromObjectToJSON(response);
        }

        ///EDIT METHODS

        /// <summary>
        /// Updates an invoice with the specifed id with the details of the provided invoice object and updates the
        /// annual cost of the relvant Property object.
        /// </summary>
        /// <param name="invoiceId">id of invoice to update</param>
        /// <param name="invoiceJSON">JSON representation of the updated invoice</param>
        /// <param name="propertyId">id of the property to which the invoice belongs.  Required to update the annual cost at the property.</param>
        /// <returns>JSON representation of updated Invoice object, wrapped in EMResponse object.</returns>
        public string editInvoice(int invoiceId, string invoiceJSON, int propertyId)
        {
            EMResponse response = new EMResponse();
            try
            {
                invoiceMgr = new InvoiceManager();
                invoiceMgr.editInvoice(invoiceId, invoiceJSON, propertyId);
                response.status = ok;
            }
            catch (Exception e)
            {
                response.status = error;
                response.data = e.Message;
            }

            return EMConverter.fromObjectToJSON(response);
        }

        ///DELETE METHODS
        
        /// <summary>
        /// Removes the specified invoice from the database and updates the annual cost for the relevant Property object.
        /// </summary>
        /// <param name="invoiceId"></param>
        /// <returns>void, wrapped in EMResponse object.</returns>
        public string deleteInvoice(int invoiceId, int propertyId)
        {
            EMResponse response = new EMResponse();
            try
            {
                invoiceMgr = new InvoiceManager();
                invoiceMgr.deleteInvoice(invoiceId, propertyId);
                response.status = ok;
            }
            catch (Exception e)
            {
                response.status = error;
                response.data = e.Message;
            }

            return EMConverter.fromObjectToJSON(response);
        }

        //* * * TARIFF MANAGER METHODS

        //// GET METHODS
        
        /// <summary>
        /// Retreives all Period objects in the database
        /// </summary>
        /// <returns>JSON representation of List[Period], wrapped in EMResponse object.</returns>
        public string getPeriods()
        {
            EMResponse response = new EMResponse();
            try
            {
                tariffMgr = new TariffManager();
                response.data = EMConverter.fromObjectToJSON(tariffMgr.getSCPeriods());
                response.status = ok;
            }
            catch (Exception e)
            {
                response.status = error;
                response.data = e.Message;
            }

            return EMConverter.fromObjectToJSON(response);
        }


        /// <summary>
        /// Retrieves a tariff object with specified id.  Child objects ARE included
        /// </summary>
        /// <param name="tariffId"></param>
        /// <returns>JSON representation of Tariff object, wrapped in EMResponse object.</returns>
        public string getTariff(int tariffId)
        {
            EMResponse response = new EMResponse();
            try
            {
                tariffMgr = new TariffManager();
                response.data = EMConverter.fromObjectToJSON(tariffMgr.getTariff(tariffId));
                response.status = ok;
            }
            catch (Exception e)
            {
                response.status = error;
                response.data = e.Message;
            }

            return EMConverter.fromObjectToJSON(response);
        }


        ///CREATE METHODS
        
        /// <summary>
        /// Creates a tariff object with the specifed attributes.  TariffBand objects must be created seperately using emAPI.createTariffBand.
        /// </summary>
        /// <param name="standingChargeValue">Value in £</param>
        /// <param name="startDate">shortdate string</param>
        /// <param name="standingChargePeriodId">id of Period for standing charge value (eg monthly, annually etc)</param>
        /// <param name="bandingPeriodId">id of Period for banding (currently unused)</param>
        /// <param name="meterId">id of meter to which tariff belongs</param>
        /// <returns>JSON representation of id of created tariff - int, wrapped in EMResponse</returns>
        public string createTariff(double standingChargeValue, string startDate, int standingChargePeriodId, int bandingPeriodId, int meterId)
        {
            EMResponse response = new EMResponse();
            try
            {
                tariffMgr = new TariffManager();
                response.data = EMConverter.fromObjectToJSON(tariffMgr.createTariff(standingChargeValue, startDate, standingChargePeriodId, bandingPeriodId, meterId));
                response.status = ok;
            }
            catch (Exception e)
            {
                response.status = error;
                response.data = e.Message;
            }

            return EMConverter.fromObjectToJSON(response);
        }


        /// <summary>
        /// Create a tariff band object and saves to tariff with provided id.  NB validity of limits must be checked by client before creation.
        /// </summary>
        /// <param name="upperLimit">the upper kWh of the band.  Use 0 for no upper limit on this band.</param>
        /// <param name="lowerLimit">the lower limit of this band.  Can be 0.</param>
        /// <param name="rate">The unit rate for each kWh that falls into this band, in pence.</param>
        /// <param name="tariffId">id of tariff to which the band belongs.</param>
        /// <returns>JSON representation of id of tariff band object, wrapped in EMResponse</returns>
        public string createTariffBand(int upperLimit, int lowerLimit, double rate, int tariffId)
        {
            EMResponse response = new EMResponse();
            try
            {
                tariffMgr = new TariffManager();
                response.data = EMConverter.fromObjectToJSON(tariffMgr.createTariffBand(upperLimit, lowerLimit, rate, tariffId));
                response.status = ok;
            }
            catch (Exception e)
            {
                response.status = error;
                response.data = e.Message;
            }

            return EMConverter.fromObjectToJSON(response);
        }

        /// <summary>
        /// Gets the date of the most recent tariff on the specified meter, used when creating a new tariff.
        /// </summary>
        /// <param name="meterId"></param>
        /// <returns>JSON representation of date string, wrapped in EMResponse</returns>
        public string getMinimumTariffDate(int meterId)
        {
            EMResponse response = new EMResponse();
            try
            {
                tariffMgr = new TariffManager();
                response.data = EMConverter.fromObjectToJSON(tariffMgr.getMinimumTariffDate(meterId));
                response.status = ok;
            }
            catch (Exception e)
            {
                response.status = error;
                response.data = e.Message;
            }

            return EMConverter.fromObjectToJSON(response);
		
		
        }


        /// <summary>
        /// Retrieves the date of the tariff before the most recent, used when editing the current tariff.
        /// </summary>
        /// <param name="meterId"></param>
        /// <returns>JSON representation of date string, wrapped in EMResponse</returns>
        public string getMinimumTariffDateForEdit(int meterId)
        {
            EMResponse response = new EMResponse();
            try
            {
                tariffMgr = new TariffManager();
                response.data = EMConverter.fromObjectToJSON(tariffMgr.getMinimumTariffDateForEdit(meterId));
                response.status = ok;
            }
            catch (Exception e)
            {
                response.status = error;
                response.data = e.Message;
            }

            return EMConverter.fromObjectToJSON(response);
        }

        
        ///EDIT METHODS
        
        /// <summary>
        /// Updates the tariff with the specified id.  A whole tariff object, including child tariffband objects should be provided, these will be updated to.
        /// NB you cannot included new tariffbands when updating a tariff - create a new band seperately first.
        /// </summary>
        /// <param name="tariffId">id of tariff object to update</param>
        /// <param name="newTariff">tariff object with updated details & tariffbands</param>
        /// <returns>JSON representation of updated Tariff object, including TariffBands, wrapped in EMResponse</returns>
        public string editTariff(int tariffId, string newTariff)
        {
            EMResponse response = new EMResponse();
            try
            {
                tariffMgr = new TariffManager();
                tariffMgr.editTariff(tariffId, newTariff);
                response.status = ok;
            }
            catch (Exception e)
            {
                response.status = error;
                response.data = e.Message;
            }

            return EMConverter.fromObjectToJSON(response);
        }

        ///DELETE METHODS
        
        /// <summary>
        /// Removes a tariff and associated tariff band objects from the database.
        /// </summary>
        /// <param name="tariffId">id of the tariff to delete</param>
        /// <returns>void, wrapped in EMResponse</returns>
        public string deleteTariff(int tariffId)
        {
            EMResponse response = new EMResponse();
            try
            {
                tariffMgr = new TariffManager();
                tariffMgr.deleteTariff(tariffId);
                response.status = ok;
            }
            catch (Exception e)
            {
                response.status = error;
                response.data = e.Message;
            }

            return EMConverter.fromObjectToJSON(response);
        }


        //VALIDATION MANAGER


        /// <summary>
        /// Validates the specified invoice, updating its isValid and isChecked attributes appropriately.
        /// </summary>
        /// <param name="invoiceId">id of invoice to validate</param>
        /// <param name="saveAfterValidation">bool representing whether the invoice validation details are saved post-validation</param>
        /// <returns>JSON representation of validated invoice, wrapped in EMResponse.</returns>
        public string validateInvoice(int invoiceId, bool saveAfterValidation)
        {
            EMResponse response = new EMResponse();
            try
            {
                validationMgr = new ValidationManager();
                response.data = EMConverter.fromObjectToJSON(validationMgr.validateInvoice(invoiceId, saveAfterValidation));
                response.status = ok;
            }
            catch (Exception e)
            {
                response.status = error;
                response.data = e.Message;
            }

            return EMConverter.fromObjectToJSON(response);
		
		
        }


        //FORECASTING MANAGER


        /// <summary>
        /// Creates an invoice object (not saved to the database) with forecasted cost and consumption figures.  Returns null if
        /// forecast not possible.
        /// </summary>
        /// <param name="meterId">id of meter for which to forecast invoice</param>
        /// <returns>JSON representation of Invoice (or null), wrapped in EMResponse</returns>
        public string forecastNextInvoice(int meterId)
        {
            EMResponse response = new EMResponse();
            try
            {
                forecastingMgr = new ForecastingManager();
                response.data = EMConverter.fromObjectToJSON(forecastingMgr.forecastNextInvoice(meterId));
                response.status = ok;
            }
            catch (Exception e)
            {
                response.status = error;
                response.data = e.Message;
            }

            return EMConverter.fromObjectToJSON(response);
        }
    }
}
