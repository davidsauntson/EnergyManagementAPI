/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
 * DAVID SAUNTSON 12993011                                               *
 * MSc Software Engineering - Final Year Project                         *
 *                                                                       *
 * Energy Management API & Software                                      *
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
 * 
 * DataAccessObject.cs
 * Code source: Handwritten
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using emAPI.Interfaces;
using emAPI.ClassLibrary;
using emAPI.Interfaces;

namespace emAPI.DAL
{
    /// <summary>
    /// Superclass for all Database access objects, deinfes the required functionality of all data storage & retrieval.
    /// </summary>
    internal abstract class DataAccessObject : IDataAccessObject
    {
        //GET METHODS - for data retreival
        public abstract Invoice getInvoice(int invoiceId);
        public abstract Meter getMeter(int meterId);
        public abstract Meter getMeterForInvoice(int invoiceId);
        public abstract Property getProperty(int PropertyId);
        public abstract MeterReading getReading(int meterReadingId);
        public abstract Tariff getTariff(int tariffId);
        public abstract TariffBand getBand(int bandId);
        public abstract User getUser(int userId);
        public abstract Annotation getNote(int noteId);
        public abstract Period getPeriod(int periodId);
        public abstract BenchmarkProperty getBenchmarkProperty(int benchmarkId);

        public abstract HeatingType getHeatingType(int propertyTypeId);
        public abstract WallType getWallType(int propertyTypeId);
        public abstract BuildingType getBuildingType(int propertyTypeId);

        public abstract BenchmarkProperty getBenchmarkForProperty(int propertyId);

        public abstract User getUserByUsername(string username);
        public abstract User getUserByEmail(string email);
        public abstract int getPropertyTypeId(int heatingId, int buildingId, int wallId);

        public abstract List<MeterReading> getMeterReadings(int meterId);
        public abstract List<Meter> getMeters(int propertyId);
        public abstract List<Annotation> getNotes(int meterId);
        public abstract List<Invoice> getInvoicesForMeter(int meterId);
        public abstract List<Property> getProperties(int userId);
        public abstract List<Property> getAllProperties();
        public abstract List<Period> getPeriods();
        public abstract List<PropertyType> getPropertyTypes();
        public abstract List<HeatingType> getHeatingTypes();
        public abstract List<BuildingType> getBuildingTypes();
        public abstract List<WallType> getWallTypes();
        public abstract List<BenchmarkProperty> getBenchmarkProperties();
        public abstract List<AnonymousProperty> getAnonymousProperties(List<int> propertyIds);

        public abstract Meter getDetailsForMeter(int meterId);

        //STATS METHODS - for extracting statistics from the database
        public abstract double getTypeAverageAnnualkWh(int benchmarkId);
        public abstract double getTypeBestAnnualkWh(int benchmarkId);


        //EDIT METHODS - for amending exisiting data
        public abstract Meter editMeter(int meterId, Meter newMeter);
        public abstract Invoice editInvoice(int invoiceId, Invoice newInvoice);
        public abstract Tariff editTariff(int tariffId, Tariff newTariff);
        public abstract TariffBand editTariffBand(int tariffBandId, TariffBand tariffBand);
        public abstract User editUser(int userId, User newUser);
        public abstract Property editProperty(int propertyId, Property newProperty);
        public abstract Annotation editNote(int noteId, Annotation newNote);
        public abstract MeterReading editMeterReading(int meterReadingId, MeterReading newReading);
        public abstract BenchmarkProperty editBenchmark(int benchmarkId, BenchmarkProperty newBenchmark);


        //SET METHODS - for data storage
        public abstract int saveUser(User user);
        public abstract int saveInvoice(Invoice invoice);
        public abstract int saveMeter(Meter meter);
        public abstract int saveNote(Annotation note);
        public abstract int saveProperty(Property property);
        public abstract int saveTariff(Tariff tariff);
        public abstract int saveTariffBand(TariffBand tariffBand);
        public abstract int saveMeterReading(MeterReading meterReading);


        
        //DELETE METHODS - for deleting existing data
        public abstract void deleteInvoice(int invoice);
        public abstract void deleteMeter(int meter);
        public abstract void deleteNote(int note);
        public abstract void deleteProperty(int property);
        public abstract void deleteReading(int reading);
        public abstract void deleteTariff(int tariff);
        public abstract void deleteTariffBand(int band);


        //LINKING METHODS - for creating relationships between parent & child objects
        public abstract void addPropertyToUser(int propertyId, int userId);
        public abstract void addMeterToProperty(int meterId, int propertyId);
        public abstract void addInvoiceToMeter(int invoiceId, int meterId);
        public abstract void addMeterToInvoice(int meterId, int invoiceId);
        public abstract void addTariffToMeter(int tariffId, int meterId);
        public abstract void addBandToTariff(int bandId, int tariffId);
        public abstract void addNoteToMeter(int noteId, int meterId);
    }
}
