
/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
 * DAVID SAUNTSON 12993011                                               *
 * MSc Software Engineering - Final Year Project                         *
 *                                                                       *
 * Energy Management API & Software                                      *
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
 * 
 * IDataAccessObject.cs
 * Code source: Handwritten
 */
		

using System;
using emAPI.ClassLibrary;

namespace emAPI.DAL
{
    /// <summary>
    /// Interface for DataAccessObject objects
    /// </summary>
    interface IDataAccessObject
    {
        void addBandToTariff(int bandId, int tariffId);
        void addInvoiceToMeter(int invoiceId, int meterId);
        void addMeterToInvoice(int meterId, int invoiceId);
        void addMeterToProperty(int meterId, int propertyId);
        void addNoteToMeter(int noteId, int meterId);
        void addPropertyToUser(int propertyId, int userId);
        void addTariffToMeter(int tariffId, int meterId);
        void deleteInvoice(int invoice);
        void deleteMeter(int meter);
        void deleteNote(int note);
        void deleteProperty(int property);
        void deleteReading(int reading);
        void deleteTariff(int tariff);
        void deleteTariffBand(int band);
        BenchmarkProperty editBenchmark(int benchmarkId, BenchmarkProperty newBenchmark);
        Invoice editInvoice(int invoiceId, Invoice newInvoice);
        Meter editMeter(int meterId, Meter newMeter);
        MeterReading editMeterReading(int meterReadingId, MeterReading newReading);
        Annotation editNote(int noteId, Annotation newNote);
        Property editProperty(int propertyId, Property newProperty);
        Tariff editTariff(int tariffId, Tariff newTariff);
        TariffBand editTariffBand(int tariffBandId, TariffBand tariffBand);
        User editUser(int userId, User newUser);
        System.Collections.Generic.List<Property> getAllProperties();
        System.Collections.Generic.List<AnonymousProperty> getAnonymousProperties(System.Collections.Generic.List<int> propertyIds);
        TariffBand getBand(int bandId);
        BenchmarkProperty getBenchmarkForProperty(int propertyId);
        System.Collections.Generic.List<BenchmarkProperty> getBenchmarkProperties();
        BenchmarkProperty getBenchmarkProperty(int benchmarkId);
        BuildingType getBuildingType(int propertyTypeId);
        System.Collections.Generic.List<BuildingType> getBuildingTypes();
        Meter getDetailsForMeter(int meterId);
        HeatingType getHeatingType(int propertyTypeId);
        System.Collections.Generic.List<HeatingType> getHeatingTypes();
        Invoice getInvoice(int invoiceId);
        System.Collections.Generic.List<Invoice> getInvoicesForMeter(int meterId);
        Meter getMeter(int meterId);
        Meter getMeterForInvoice(int invoiceId);
        System.Collections.Generic.List<MeterReading> getMeterReadings(int meterId);
        System.Collections.Generic.List<Meter> getMeters(int propertyId);
        Annotation getNote(int noteId);
        System.Collections.Generic.List<Annotation> getNotes(int meterId);
        Period getPeriod(int periodId);
        System.Collections.Generic.List<Period> getPeriods();
        System.Collections.Generic.List<Property> getProperties(int userId);
        Property getProperty(int PropertyId);
        int getPropertyTypeId(int heatingId, int buildingId, int wallId);
        System.Collections.Generic.List<PropertyType> getPropertyTypes();
        MeterReading getReading(int meterReadingId);
        Tariff getTariff(int tariffId);
        double getTypeAverageAnnualkWh(int benchmarkId);
        double getTypeBestAnnualkWh(int benchmarkId);
        User getUser(int userId);
        User getUserByEmail(string email);
        User getUserByUsername(string username);
        WallType getWallType(int propertyTypeId);
        System.Collections.Generic.List<WallType> getWallTypes();
        int saveInvoice(Invoice invoice);
        int saveMeter(Meter meter);
        int saveMeterReading(MeterReading meterReading);
        int saveNote(Annotation note);
        int saveProperty(Property property);
        int saveTariff(Tariff tariff);
        int saveTariffBand(TariffBand tariffBand);
        int saveUser(User user);
    }
}
