
/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
 * DAVID SAUNTSON 12993011                                               *
 * MSc Software Engineering - Final Year Project                         *
 *                                                                       *
 * Energy Management API & Software                                      *
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
 * 
 * IPropertyManager.cs 
 * Code source: Handwritten
 */
		

using System;
using emAPI.ClassLibrary;

namespace emAPI.Interfaces
{
    /// <summary>
    /// Interface for PropertyManager objects
    /// </summary>
    interface IPropertyManager
    {
        void addMeterToProperty(int meterId, int propertyId);
        int createProperty(string name, string postcode, int floorArea, int numbBedrooms, int typeId, int userId);
        void deleteProperty(int propertyId);
        Property editProperty(int propertyId, string propertyJSON);
        System.Collections.Generic.List<AnonymousProperty> getAnonymousProperties(string propertyIdsJSON);
        BenchmarkProperty getBenchmarkForProperty(int propertyId);
        BuildingType getBuildingType(int propertyTypeId);
        System.Collections.Generic.List<BuildingType> getBuildingTypes();
        System.Collections.Generic.List<Chunk> getDataAtProperty(int propertyId, string startDate, string endDate, int intervalId, int dataTypeId);
        double getElecData(int propertyId, string startDate, string endDate, int dataTypeId);
        double getFloorArea(int propertyId);
        double getGasData(int propertyId, string startDate, string endDate, int dataTypeId);
        HeatingType getHeatingType(int propertyTypeId);
        System.Collections.Generic.List<HeatingType> getHeatingTypes();
        DateTime getMostRecentDate(int propertyId, DataType dataType);
        Property getProperty(int propertyId);
        int getPropertyTypeId(int heatingId, int buildingId, int wallId);
        System.Collections.Generic.List<PropertyType> getPropertyTypes();
        double getTotalData(int propertyId, string startDate, string endDate, int dataTypeId);
        WallType getWallType(int propertyTypeId);
        System.Collections.Generic.List<WallType> getWallTypes();
        double normalisedEleckWh(int propertyId, string startDate, string endDate);
        double normalisedGaskWh(int propertyId, string startDate, string endDate);
        void updateAnnualCO2(int propertyId);
        void updateAnnualTotalCost(int propertyId);
        void updateAnnualTotalkWh(int propertyId);
    }
}
