
/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
 * DAVID SAUNTSON 12993011                                               *
 * MSc Software Engineering - Final Year Project                         *
 *                                                                       *
 * Energy Management API & Software                                      *
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
 * 
 * PropertyManager.cs 
 * Code source: Handwritten
 */
		

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using emAPI.ClassLibrary;
using emAPI.Interfaces;

namespace emAPI.Controllers
{
    /// <summary>
    /// Controller object for Property model object related operations.
    /// </summary>
    internal class PropertyManager : IPropertyManager
    {
        internal EMMediator mediator;

        /// <summary>
        /// Constructor
        /// </summary>
        internal PropertyManager()
        {
            mediator = new EMMediator();
        }

        //* * * DATA ACCESS METHODS

        public List<PropertyType> getPropertyTypes()
        {
            List<PropertyType> propertyTypes = mediator.DataManager.getPropertyTypes();
            return propertyTypes;
        }

        public List<HeatingType> getHeatingTypes()
        {
            return mediator.DataManager.getHeatingTypes();
        }

        public List<BuildingType> getBuildingTypes()
        {
            return mediator.DataManager.getBuildingTypes();
        }

        public List<WallType> getWallTypes()
        {
            return mediator.DataManager.getWallTypes();
        }

        public int getPropertyTypeId(int heatingId, int buildingId, int wallId)
        {
            return mediator.DataManager.getPropertyTypeId(heatingId, buildingId, wallId);
        }

        public BenchmarkProperty getBenchmarkForProperty(int propertyId)
        {
            BenchmarkProperty benchmark = mediator.DataManager.getBenchmarkForProperty(propertyId);
            return benchmark;
        }

        public Property getProperty(int propertyId)
        {
            Property property = mediator.DataManager.getProperty(propertyId);
            return property;
        }

        public List<AnonymousProperty> getAnonymousProperties(string propertyIdsJSON)
        {
            List<int> propertyIds = EMConverter.fromJSONtoA<List<int>>(propertyIdsJSON);
            List<AnonymousProperty> anonProperties = mediator.DataManager.getAnonymousProperties(propertyIds);
            return anonProperties;
        }

        public BuildingType getBuildingType(int propertyTypeId)
        {
            BuildingType type = mediator.DataManager.getBuildingType(propertyTypeId);
            return type;
        }

        public HeatingType getHeatingType(int propertyTypeId)
        {
            HeatingType type = mediator.DataManager.getHeatingType(propertyTypeId);
            return type;
        }

        public WallType getWallType(int propertyTypeId)
        {
            WallType type = mediator.DataManager.getWallType(propertyTypeId);
            return type;
        }


        //* * * CREATION METHODS

        public int createProperty(string name, string postcode, int floorArea, int numbBedrooms, int typeId, int userId)
        {
            Property property = new Property
            {
                Name = name,
                Postcode = postcode,
                FloorArea = floorArea,
                NumbBedrooms = numbBedrooms
            };

            property.PropertyType = mediator.DataManager.getPropertyType(typeId);
            int propertyId = mediator.DataManager.saveProperty(property);
            mediator.addPropertyToUser(propertyId, userId);
            return propertyId;
        }


        //* * * UPDATE METHODS

        public Property editProperty(int propertyId, string propertyJSON)
        {
            Property updatedProperty = EMConverter.fromJSONtoA<Property>(propertyJSON);
            updatedProperty = mediator.DataManager.editProperty(propertyId, updatedProperty);

            ///need to update benchmark stats in case property type has been changed
            EMDatabaseStats.updateBenchmarkStats();

            return updatedProperty;
        }


        //* * * DELETE METHODS

        public void deleteProperty(int propertyId)
        {
            mediator.DataManager.deleteProperty(propertyId);
        }


        //* * * OTHER METHODS

        public void addMeterToProperty(int meterId, int propertyId)
        {
            Meter meter = mediator.DataManager.getMeter(meterId);
            Property property = mediator.DataManager.getProperty(propertyId);

            property.Meters.Add(meter);
        }

        public double getTotalData(int propertyId, string startDate, string endDate, int dataTypeId)
        {
            return getElecData(propertyId, startDate, endDate, dataTypeId) + getGasData(propertyId, startDate, endDate, dataTypeId);
        }


        public double getElecData(int propertyId, string startDate, string endDate, int dataTypeId)
        {
            Property property = mediator.DataManager.getProperty(propertyId);
            double cost = 0;
            foreach (Meter meter in property.Meters)
            {
                var elecMeter = meter as ElectricityMeter;
                if (elecMeter != null)
                {
                    cost += mediator.getDataFromMeter(meter.Id, startDate, endDate, dataTypeId);
                }
            }

            return cost;
        }


        public double getGasData(int propertyId, string startDate, string endDate, int dataTypeId)
        {
            Property property = mediator.DataManager.getProperty(propertyId);
            double cost = 0;
            foreach (Meter meter in property.Meters)
            {
                var gasMeter = meter as GasMeter;
                if (gasMeter != null)
                {
                    cost += mediator.getDataFromMeter(meter.Id, startDate, endDate, dataTypeId);
                }
            }

            return cost;
        }

        private double getTotalCO2(int propertyId, string startDate, string endDate)
        {
            Property property = mediator.DataManager.getProperty(propertyId);
            double CO2kg = 0;
            foreach (Meter meter in property.Meters)
            {
                CO2kg += mediator.getCO2FromMeter(meter.Id, startDate, endDate);
            }

            return CO2kg;
        }

        public double getFloorArea(int propertyId)
        {
            Property property = mediator.DataManager.getProperty(propertyId);
            double floorArea = 0;
            if (property.FloorArea == 0)
            {
                ///need to get standard floor area for this property type
                BenchmarkProperty benchmark = mediator.DataManager.getBenchmarkForProperty(propertyId);
                floorArea = benchmark.FloorArea;
            }
            else
            {
                floorArea = property.FloorArea;
            }

            return floorArea;
        }

        public double normalisedEleckWh(int propertyId, string startDate, string endDate)
        {
            double result;

            try
            {
                result = getElecData(propertyId, startDate, endDate, (int)DataType.Energy) / getFloorArea(propertyId);
            }
            catch
            {
                result = 0;
            }

            return result;
        }

        public double normalisedGaskWh(int propertyId, string startDate, string endDate)
        {
            double result;

            try
            {
                result = getGasData(propertyId, startDate, endDate, (int)DataType.Energy) / getFloorArea(propertyId);
            }
            catch
            {
                result = 0;
            }

            return result;
        }


        public DateTime getMostRecentDate(int propertyId, DataType dataType)
        {
            Property property = mediator.DataManager.getProperty(propertyId);

            DateTime maxDate = DateTime.MinValue;

            if (property.Meters != null)
            {
                foreach (Meter meter in property.Meters)
                {
                    switch ((int)dataType)
                    {
                        case (int)DataType.Energy:

                            if (meter.Register != null)
                            {
                                if (meter.Register.Count != 0)
                                {

                                    ///find max date on register
                                    DateTime maxDateOnThisMeter = meter.Register.Max(rdg => rdg.Date);

                                    if (maxDateOnThisMeter > maxDate)
                                    {
                                        maxDate = maxDateOnThisMeter;
                                    }
                                }
                            }

                            break;

                        case (int)DataType.Cost:

                            if (meter.Invoices != null)
                            {
                                if (meter.Invoices.Count != 0)
                                {
                                    ///find max date on list of invoices
                                    DateTime maxDateInInvoices = meter.Invoices.Max(inv => inv.EndDate);

                                    if (maxDateInInvoices > maxDate)
                                    {
                                        maxDate = maxDateInInvoices;
                                    }
                                }
                            }

                            break;
                    }
                }
            }
                      
            if (maxDate == DateTime.MinValue)
            {
                maxDate = DateTime.Now;
            }

            return maxDate;
        }


        public void updateAnnualTotalkWh(int propertyId)
        {
            Property property = mediator.DataManager.getProperty(propertyId);
            TimeSpan oneYear = new TimeSpan(365, 0, 0, 0);

            string start = (getMostRecentDate(propertyId, DataType.Energy) - oneYear).ToString();
            string end = getMostRecentDate(propertyId, DataType.Energy).ToString();

            property.AnnualkWh = getTotalData(propertyId, start, end, (int)DataType.Energy);
            property.AreaNormalisedAnnualkWh = Math.Round(property.AnnualkWh / getFloorArea(propertyId), 1);
            mediator.DataManager.editProperty(propertyId, property);
        }

        public void updateAnnualTotalCost(int propertyId)
        {
            Property property = mediator.DataManager.getProperty(propertyId);
            TimeSpan oneYear = new TimeSpan(365, 0, 0, 0);
            string start = (Convert.ToDateTime(getMostRecentDate(propertyId, DataType.Cost)) - oneYear).ToString();
            string end = Convert.ToDateTime(getMostRecentDate(propertyId, DataType.Cost)).ToString();

            property.AnnualCost = getTotalData(propertyId, start, end, (int)DataType.Cost);
            mediator.DataManager.editProperty(propertyId, property);
        }

        public void updateAnnualCO2(int propertyId)
        {
            Property property = mediator.DataManager.getProperty(propertyId);
            TimeSpan oneYear = new TimeSpan(365, 0, 0, 0);
            string start = (getMostRecentDate(propertyId, DataType.Energy) - oneYear).ToString();
            string end = getMostRecentDate(propertyId, DataType.Energy).ToString();

            property.AnnualCO2kg = getTotalCO2(propertyId, start, end);
            mediator.DataManager.editProperty(propertyId, property);
        }


        public List<Chunk> getDataAtProperty(int propertyId, string startDate, string endDate, int intervalId, int dataTypeId)
        {
            Property property = mediator.DataManager.getProperty(propertyId);

            ///setup vairable for apportionment manager
            DateTime start = Convert.ToDateTime(startDate);
            DateTime end = Convert.ToDateTime(endDate);
            AppotionmentPeriod interval = (AppotionmentPeriod)intervalId;

            ///property apportioned consumption requires a list of sub-lists, each sub-list the result of interval apportion on that
            ///meter.  the final list returned is the sum of each chunk on the sub list. SO...
            
            ///create reference list for dates
            List<Chunk> datedChunks = mediator.setupDatedChunksForApportionToPeriod(start, end, interval);

            ///List<Chunk> datedChunks = mediator.getDataFromMeterByInterval(property.Meters.ElementAt(0).Id, startDate, endDate, intervalId, dataTypeId);

            ///create the containing master list, which will have one list for each meter
            List<List<Chunk>> masterList = new List<List<Chunk>>();
            
            ///add a new list for each meter, where the list is the result of apportioning the meter by interval
            foreach (Meter meter in property.Meters)
            {
                List<Chunk> apportionedData = mediator.getDataFromMeterByInterval(meter.Id, startDate, endDate, intervalId, dataTypeId);

                if (apportionedData != null)
                {
                    masterList.Add(apportionedData);
                }
            }

            ///create result list that will have one list of chunks
            List<Chunk> resultsList = new List<Chunk>();

            foreach (Chunk chunk in datedChunks)
            {
                Chunk newChunk = new Chunk()
                {
                    StartDate = chunk.StartDate,
                    EndDate = chunk.EndDate
                };



                foreach (List<Chunk> subList in masterList)
                {
                    Chunk matchingChunk = subList.Find(chk => chk.StartDate == chunk.StartDate);

                    try
                    {
                        newChunk.Amount += matchingChunk.Amount;
                    }
                    catch
                    {
                        ///here there is no chunk on the subList at the required index
                        ///can proceed to the next chunk in datedChunks
                    }
                }

                resultsList.Add(newChunk);
            }

            return resultsList;
        }
    }
}
