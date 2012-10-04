
/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
 * DAVID SAUNTSON 12993011                                               *
 * MSc Software Engineering - Final Year Project                         *
 * 	                                                                     *
 * Energy Management API & Software	                                     *
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
 * 
 * IemAPI.cs - this is the service contract interface for the API facade 
 * Code source: Handwritten
 */

		
		

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;


//testing
using emAPI.ClassLibrary;

namespace emAPI
{
    /// <summary>
    /// Energy Manager API Interface.  For method documentation, see emAPI.cs.
    /// </summary>
    [ServiceContract]
    public interface IemAPI
    {


//* * * USER METHODS

        [OperationContract]
        string usernameIsUnique(string username);

        [OperationContract]
        string emailIsUnique(string email);

        [OperationContract]
        string createUser(string username, string forename, string surname, string password, string email);

        [OperationContract]
        string validateUser(string username, string password);

        [OperationContract]
        string getPropertiesForUser(int userId);

        [OperationContract]
        string getUser(int userId);

        [OperationContract]
        string getComparativeCostsForUser(int userId);

        ///EDIT METHODS
        ///

        [OperationContract]
        string editUser(int userId, string newUser);

        [OperationContract]
        string updatePassword(int userId, string newPassword);

//* * * PROPERTY METHODS

        ///GET METHODS
        
        [OperationContract]
        string getPropertyTypes();

        [OperationContract]
        string getProperty(int propertyId);

        ///CREATE METHODS

        [OperationContract]
        string createProperty(string name, string postcode, int floorArea, int numbBedrooms, int typeId, int userId);

        [OperationContract]
        string getPropertyTypeId(int heatingId, int buildingId, int wallId);

        [OperationContract]
        string getHeatingTypes();

        [OperationContract]
        string getBuildingTypes();

        [OperationContract]
        string getWallTypes();

        [OperationContract]
        string getTotalData(int propertyId, string startDate, string endDate, int dataTypeId);

        [OperationContract]
        string getElecData(int propertyId, string startDate, string endDate, int dataTypeId);

        [OperationContract]
        string getGasData(int propertyId, string startDate, string endDate, int dataTypeId);

        [OperationContract]
        string getBenchmarkForProperty(int propertyId);

        [OperationContract]
        string getAnonymousProperties(string propertyId);

        [OperationContract]
        string getDataAtProperty(int propertyId, string startDate, string endDate, int intervalId, int dataTypeId);

        [OperationContract]
        string getBuildingType(int propertyTypeId);

        [OperationContract]
        string getHeatingType(int propertyTypeId);

        [OperationContract]
        string getWallType(int propertyTypeId);

        [OperationContract]
        string getMostRecentDate(int propertyId, int dataTypeId);

        [OperationContract]
        string getFloorArea(int propertyId);

        ///EDIT METHODS
        ///

        [OperationContract]
        string editProperty(int propertyId, string newProperty);


        ///DELETE METHODS
        ///

        [OperationContract]
        string deleteProperty(int propertyId);


        ///OTHER METHODS
        ///

        [OperationContract]
        string updateBenchmarks();

//* * * METER METHODS

        ///GET METHODS
        ///

        [OperationContract]
        string getMeter(int meterId);

        [OperationContract]
        string getReading(int readingId);

        [OperationContract]
        string getMeterReadings(int meterId);

        [OperationContract]
        string getMinimumReadingDate(int meterId);

        [OperationContract]
        string getMinimumReadingDateForEdit(int meterId);

        [OperationContract]
        string getLastInvoiceDate(int meterId);

        [OperationContract]
        string getDetailsForMeter(int meterId);

        [OperationContract]
        string getDataFromMeter(int meterId, string startDate, string endDate, int dataTypeId);

        [OperationContract]
        string getDataFromMeterByInterval(int meterId, string startDate, string endDate, int intervalId, int dataTypeId);

        ///CREATE METHODS
        ///

        [OperationContract]
        string createElectricityMeter(string serialNo, double scalingFactor, int numbDigits, string startDate, int propertyId);

        [OperationContract]
        string createGasMeter(string serialNo, double meterCoefficient, int numbDigits, string startDate, int propertyId);

        [OperationContract]
        string createMeterReading(string date, int reading, int meterId, int propertyId);

        ///EDIT METHODS
        ///

        [OperationContract]
        string editMeter(int meterId, string newMeter);

        [OperationContract]
        string editMeterReading(int readingId, int meterId, string newMeter, int propertyId);

        ///DELETE METHODS
        ///

        [OperationContract]
        string deleteMeter(int meterId, int propertyId);

        [OperationContract]
        string deleteReading(int readingId, int meterId, int propertyId);


//* * * INVOICE METHODS

        ///GET METHODS
        ///

        [OperationContract]
        string getInvoicesForMeter(int meterId);

        [OperationContract]
        string getInvoice(int invoiceId);


        ///CREATE METHODS
        ///

        [OperationContract]
        string createInvoice(int meterId, string billDate, string startDate, string endDate, int kWh,
            double consumptionCharge, double standingCharge, double otherCharge, int propertyId);

        ///EDIT METHODS
        ///

        [OperationContract]
        string editInvoice(int invoiceId, string newInvoice, int propertyId);


        ///DELETE METHODS
        ///
        [OperationContract]
        string deleteInvoice(int invoiceId, int propertyId);

//* * * TARIFF MANAGER METHODS

        /// GET METHODS
        /// 

        [OperationContract]
        string getPeriods();

        [OperationContract]
        string getTariff(int tariffId);

        [OperationContract]
        string getMinimumTariffDate(int meterId);

        [OperationContract]
        string getMinimumTariffDateForEdit(int meterId);


        ///CREATE METHODS
        ///

        [OperationContract]
        string createTariff(double value, string startDate, int standingChargePeriodId, int bandingPeriodId, int meterId);

        [OperationContract]
        string createTariffBand(int upperLimit, int lowerLimit, double rate, int tariffId);

        ///EDIT METHODS
        ///

        [OperationContract]
        string editTariff(int tariffId, string newTariff);

        ///DELETE METHODS
        ///
        [OperationContract]
        string deleteTariff(int tariffId);


//* * * VALIDATION MANAGER METHODS

        [OperationContract]
        string validateInvoice(int invoiceId, bool saveAfterValidation);


//* * * FORECASTING MANAGER METHODS

        [OperationContract]
        string forecastNextInvoice(int meterId);
    }
}
