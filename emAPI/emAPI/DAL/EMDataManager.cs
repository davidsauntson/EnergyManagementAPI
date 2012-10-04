/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
 * DAVID SAUNTSON 12993011                                               *
 * MSc Software Engineering - Final Year Project                         *
 *                                                                       *
 * Energy Management API & Software                                      *
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
 * 
 * EMDataManager.cs
 * Code source: Handwritten
 */
	
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using emAPI.ClassLibrary;
using System.Data.Entity;

namespace emAPI.DAL
{
    /// <summary>
    /// Entity Framework implementation of data storage.  Sub-class of DataAcccessObject.
    /// </summary>
    internal class EMDataManager : DataAccessObject
    {
        /// <summary>
        /// instance of the EMDatabase EF4.3 DbContext class
        /// </summary>
        internal EMDatabase emdb { get; set; }

        /// <summary>
        /// Ctor, initialises emdb to hold ref to the database
        /// </summary>
        public EMDataManager()
        {
            emdb = new EMDatabase();
        }

//* * * METHODS
//* * * GET METHODS BY ID
        
        
        /// single objects from object id
        
        /// <summary>
        /// Retreive invoice with specified id with associated tariff & meter
        /// </summary>
        /// <param name="invoiceId"></param>
        /// <returns></returns>
        public override Invoice getInvoice(int invoiceId)
        {
            return emdb.Invoices.Include(inv => inv.Meter)
                                .Include(inv => inv.Meter.Tariffs)              
                                .Single(inv => inv.Id == invoiceId);
        }


        /// <summary>
        /// Retreives the meter with specified id and associated meter readings
        /// </summary>
        /// <param name="meterId"></param>
        /// <returns></returns>
        public override Meter getMeter(int meterId)
        {
            return emdb.Meters.Include("Register").Single(m => m.Id == meterId);
        }


        /// <summary>
        /// Retreives the property with the specified property id and associated meters
        /// </summary>
        /// <param name="PropertyId"></param>
        /// <returns></returns>
        public override Property getProperty(int propertyId)
        {
            return emdb.Properties.Include("Meters").Include("PropertyType").Single(p => p.Id == propertyId);
        }

        /// <summary>
        /// Retreives the property type with the specified id
        /// </summary>
        /// <param name="typeId"></param>
        /// <returns></returns>
        public PropertyType getPropertyType(int typeId)
        {
            return emdb.PropertyTypes.Include("Walls").Include("Heating").Include("Building").Single(type => type.Id == typeId);
        }

        /// <summary>
        /// Retreive meter reading with specified id
        /// </summary>
        /// <param name="meterReadingId"></param>
        /// <returns></returns>
        public override MeterReading getReading(int meterReadingId)
        {
            return emdb.MeterReadings.Single(mr => mr.Id == meterReadingId);
        }

        /// <summary>
        /// Retreives the tariff with the specified id, along with associated tariff bands & standing charge period
        /// </summary>
        /// <param name="tariffId"></param>
        /// <returns></returns>
        public override Tariff getTariff(int tariffId)
        {
            Tariff tariff = emdb.Tariffs.Include("Bands")
                                        .Include("SCPeriod")
                                        .Include("BandPeriod")
                                        .Single(t => t.Id == tariffId);

            ///total disconnection from the EF4.1 proxy object is required
            Tariff trf = new Tariff();
            trf.Id = tariff.Id;
            trf.SCPeriod = tariff.SCPeriod;
            trf.SCValue = tariff.SCValue;
            trf.BandPeriod = tariff.BandPeriod;
            trf.StartDate = tariff.StartDate;
            trf.Bands = tariff.Bands.ToList();
            
            return trf;
        }


        public override TariffBand getBand(int bandId)
        {
            return emdb.TariffBands.Find(bandId);
        }

        /// <summary>
        /// Retreives the standing charge period with the specified id
        /// </summary>
        /// <param name="periodId"></param>
        /// <returns></returns>
        public override Period getPeriod(int periodId)
        {
            return emdb.Periods.Find(periodId);
        }

        /// <summary>
        /// Retrieves user with specified id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public override User getUser(int userId)
        {
            return emdb.Users.Find(userId);
        }


        /// <summary>
        /// Retrieves note with specified id
        /// </summary>
        /// <param name="noteId"></param>
        /// <returns></returns>
        public override Annotation getNote(int noteId)
        {
            return emdb.Annotations.Find(noteId);
        }
        

//* * * GET METHODS BY OTHER PARAMETER

        public override User getUserByUsername(string username)
        {
            return emdb.Users.Single(u => u.Username == username);
        }

        public override User getUserByEmail(string email)
        {
            return emdb.Users.Single(u => u.Email == email);
        }

        public override BenchmarkProperty getBenchmarkProperty(int benchmarkId)
        {
            return emdb.Benchmarks.Find(benchmarkId);
        }

        public override int getPropertyTypeId(int heatingId, int buildingId, int wallId)
        {
            PropertyType type = emdb.PropertyTypes.Single(pt => (pt.Heating.Id == heatingId && pt.Building.Id == buildingId && pt.Walls.Id == wallId));
            return type.Id;
        }

        public override List<AnonymousProperty> getAnonymousProperties(List<int> propertyIds)
        {
            List<AnonymousProperty> result = new List<AnonymousProperty>();
            
            ///include property type & all children
            foreach (Property property in emdb.Properties.Include(p => p.PropertyType).Include(p => p.PropertyType.Walls)
                                                                                      .Include(p => p.PropertyType.Heating)
                                                                                      .Include(p => p.PropertyType.Building))
                                                  
            {
                AnonymousProperty anonProperty = new AnonymousProperty
                {
                    Id = property.Id,
                    Postcode = property.Postcode,
                    AnnualCost = property.AnnualCost,
                    AnnualkWh = property.AnnualkWh,
                    AreaNormalisedAnnualkWh = property.AreaNormalisedAnnualkWh,
                    FloorArea = property.FloorArea,
                    NumbBedrooms = property.NumbBedrooms
                };

                anonProperty.isUsers = propertyIds.Contains(property.Id);
                anonProperty.Walls = property.PropertyType.Walls.Description;
                anonProperty.Buiding = property.PropertyType.Building.Description;
                anonProperty.Heating = property.PropertyType.Heating.Description;

                result.Add(anonProperty);
            }

            return result;
        }



//* * * GET METHODS BY PARENT ID

        ///nested objects from parent object id

        /// <summary>
        /// Retreive list of properties for user with specified id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public override List<Property> getProperties(int userId)
        {
            return emdb.Users.Include("Properties").Single(u => u.Id == userId).Properties.ToList<Property>();
        }


        /// <summary>
        /// Retrieve list of meters at property with specified id
        /// </summary>
        /// <param name="propertyId"></param>
        /// <returns></returns>
        public override List<Meter> getMeters(int propertyId)
        {
            return emdb.Properties.Include("Meters").Single(p => p.Id == propertyId).Meters.ToList<Meter>();
        }


        /// <summary>
        /// Retrieve list of meter readings for the meter with specified id
        /// </summary>
        /// <param name="meterId"></param>
        /// <returns></returns>
        public override List<MeterReading> getMeterReadings(int meterId)
        {
            return emdb.Meters.Include("Register").Single(m => m.Id == meterId).Register.ToList<MeterReading>();
        }


        /// <summary>
        /// Retireve list of invoices assigned to meter with specified id
        /// </summary>
        /// <param name="meterId"></param>
        /// <returns></returns>
        public override List<Invoice> getInvoicesForMeter(int meterId)
        {
            return emdb.Meters.Include("Invoices").Single(m => m.Id == meterId).Invoices.ToList<Invoice>();
        }


        /// <summary>
        /// Retreives list of notes on the meter with the specified id
        /// </summary>
        /// <param name="meterId"></param>
        /// <returns></returns>
        public override List<Annotation> getNotes(int meterId)
        {
            return emdb.Meters.Include("Annotations").Single(m => m.Id == meterId).Notes.ToList<Annotation>();
        }


        /// <summary>
        /// Return the meter associated with the specified invoice
        /// </summary>
        /// <param name="invoiceId"></param>
        /// <returns></returns>
        public override Meter getMeterForInvoice(int invoiceId)
        {
            Invoice invoice = emdb.Invoices.Find(invoiceId);
            return getMeter(invoice.Meter.Id);
        }

        public override BenchmarkProperty getBenchmarkForProperty(int propertyId)
        {
            Property property = getProperty(propertyId);
            return emdb.Benchmarks.Single(bmk => bmk.PropertyType.Id == property.PropertyType.Id);
        }

        public override BuildingType getBuildingType(int propertyTypeId)
        {
            PropertyType type = getPropertyType(propertyTypeId);
            return emdb.BuildingTypes.Find(type.Building.Id);
        }

        public override HeatingType getHeatingType(int propertyTypeId)
        {
            PropertyType type = getPropertyType(propertyTypeId);
            return emdb.HeatingTypes.Find(type.Heating.Id);
        }

        public override WallType getWallType(int propertyTypeId)
        {
            PropertyType type = getPropertyType(propertyTypeId);
            return emdb.WallTypes.Find(type.Walls.Id);
        }


        ///single attributes

        public string getUserPassword(int userId)
        {
            User user = emdb.Users.Find(userId);
            return user.Password;
        }


        ///whole tables

        public override List<PropertyType> getPropertyTypes()
        {
            return emdb.PropertyTypes.ToList<PropertyType>();
        }

        public override List<HeatingType> getHeatingTypes()
        {
            return emdb.HeatingTypes.ToList<HeatingType>();
        }

        public override List<BuildingType> getBuildingTypes()
        {
            return emdb.BuildingTypes.ToList<BuildingType>();
        }

        public override List<WallType> getWallTypes()
        {
            return emdb.WallTypes.ToList<WallType>();
        }

        public override List<Period> getPeriods()
        {
            List<Period> periods = new List<Period>();
            foreach (Period p in emdb.Periods)
            {
                Period period = new Period
                {
                    Id = p.Id,
                    NumbDays = p.NumbDays,
                    Length = p.Length
                };

                periods.Add(period);
            }

            return periods;
        }

        public override List<BenchmarkProperty> getBenchmarkProperties()
        {
            return emdb.Benchmarks.ToList<BenchmarkProperty>();
        }

        public override List<Property> getAllProperties()
        {
            return emdb.Properties.ToList<Property>();
        }


        ///other
        public override Meter getDetailsForMeter(int meterId)
        {
            return emdb.Meters.Find(meterId);
        }

//* * * STATS METHODS - for extracting statistics from the database

        public override double getTypeAverageAnnualkWh(int benchmarkId)
        {
            double averageAnnualkWh;
            BenchmarkProperty benchmark = emdb.Benchmarks.Find(benchmarkId);

            var annualTotals = from p in emdb.Properties
                             where p.PropertyType.Id == benchmark.PropertyType.Id && p.AnnualkWh != 0
                             select p.AnnualkWh;
            double totalKwh = 0;
            foreach (var kWh in annualTotals)
            {
                totalKwh += kWh;
            }

            averageAnnualkWh = totalKwh / annualTotals.Count();
            if (annualTotals.Count() == 0)
            {
                averageAnnualkWh = 0;
            }

            return averageAnnualkWh;
        }


        public override double getTypeBestAnnualkWh(int benchmarkId)
        {
            BenchmarkProperty benchmark = emdb.Benchmarks.Find(benchmarkId);

            var annualTotals = from p in emdb.Properties
                               where p.PropertyType.Id == benchmark.PropertyType.Id  && p.AnnualkWh != 0
                               select p.AnnualkWh;

            double lowestkWh = 0;
            foreach (var kWh in annualTotals)
            {
                ///prevent return of 0
                if (lowestkWh == 0)
                {
                    lowestkWh = kWh;
                }
                    
                if (kWh < lowestkWh)
                {
                        lowestkWh = kWh;
                }
            }

            return lowestkWh;
        }

//* * * EDIT METHODS - for amending existing data

        /// <summary>
        /// Updates meter with specified id.  Child objects ARE NOT affected.
        /// </summary>
        /// <param name="meterId">id of meter to update</param>
        /// <param name="newMeter">meter object with replacement data</param>
        public override Meter editMeter(int meterId, Meter newMeter)
        {
            Meter oldMeter = emdb.Meters.Single(m => m.Id == meterId);

            oldMeter.KWhtoCO2ConversionFactor = newMeter.KWhtoCO2ConversionFactor;
            oldMeter.SerialNo = newMeter.SerialNo;
            oldMeter.NumbDigits = newMeter.NumbDigits;

            emdb.SaveChanges();
            return oldMeter;
        }

        /// <summary>
        /// Updates invoice with specified id.  Child objects ARE NOT updated (except meter).
        /// </summary>
        /// <param name="invoiceId">id of invoice to update</param>
        /// <param name="newInvoice">invoice object with replacement data</param>
        public override Invoice editInvoice(int invoiceId, Invoice newInvoice)
        {
            Invoice oldInvoice = emdb.Invoices.Include("Meter").Single(inv => inv.Id == invoiceId);

            oldInvoice.Meter = emdb.Meters.Find(newInvoice.Meter.Id);

            oldInvoice.PresentRead = newInvoice.PresentRead;
            oldInvoice.PreviousRead = newInvoice.PreviousRead;
            oldInvoice.KWh = newInvoice.KWh;

            oldInvoice.ConsumptionCharge = newInvoice.ConsumptionCharge;
            oldInvoice.StandingCharge = newInvoice.StandingCharge;
            oldInvoice.OtherCharge = newInvoice.OtherCharge;

            oldInvoice.BillDate = newInvoice.BillDate;
            oldInvoice.StartDate = newInvoice.StartDate;
            oldInvoice.EndDate = newInvoice.EndDate;

            oldInvoice.Checked = newInvoice.Checked;
            oldInvoice.ConsumptionIsValid = newInvoice.ConsumptionIsValid;
            oldInvoice.CostIsValid = newInvoice.CostIsValid;

            emdb.SaveChanges();
            return oldInvoice;
        }

        /// <summary>
        /// Updates tariff with specified id.  Child TariffBand objects ARE updated, and their ids are required in the new tariff object
        /// </summary>
        /// <param name="tariffId">id of tariff to update</param>
        /// <param name="newTariff">tariff object with replacement data NLCUDING updated TariffBand objects with their ids as they exist in the database before change</param>
        public override Tariff editTariff(int tariffId, Tariff newTariff)
        {
            Tariff oldTariff = emdb.Tariffs.Find(tariffId);

            ///update each band item seperately
            foreach (TariffBand newBand in newTariff.Bands)
            {
                editTariffBand(newBand.Id, newBand);
            }


            oldTariff.SCPeriod = emdb.Periods.Find(newTariff.SCPeriod.Id);
            oldTariff.BandPeriod = emdb.Periods.Find(newTariff.BandPeriod.Id);

            oldTariff.SCValue = newTariff.SCValue;
            oldTariff.StartDate = newTariff.StartDate;

            emdb.SaveChanges();            
            return oldTariff;


        }

        /// <summary>
        /// Update tariffBand with specified id.
        /// </summary>
        /// <param name="tariffBandId">id of tariff band to update</param>
        /// <param name="tariffBand">replacement tariff band object</param>
        public override TariffBand editTariffBand(int tariffBandId, TariffBand newBand)
        {
            TariffBand oldBand = emdb.TariffBands.Find(tariffBandId);

            oldBand.UnitRate = newBand.UnitRate;
            oldBand.LowerkWhLimit = newBand.LowerkWhLimit;
            oldBand.UpperkWhLimit = newBand.UpperkWhLimit;

            return oldBand;
        }

        /// <summary>
        /// Update with specified id.  Child objects ARE NOT updated.
        /// </summary>
        /// <param name="userId">id of user to update</param>
        /// <param name="newUser">user object with replacement data</param>
        public override User editUser(int userId, User newUser)
        {
            User oldUser = emdb.Users.Find(userId);

            oldUser.Email = newUser.Email;
            oldUser.Forename = newUser.Forename;
            oldUser.Password = newUser.Password;
            oldUser.Surname = newUser.Surname;
            oldUser.Username = newUser.Username;

            emdb.SaveChanges();
            return oldUser;
        }

        /// <summary>
        /// Update property with specified id.  Child objects ARE NOT updated.
        /// </summary>
        /// <param name="propertyId">id of proeprty to update</param>
        /// <param name="newProperty">property object with replacement data</param>
        public override Property editProperty(int propertyId, Property newProperty)
        {
            Property oldProperty = emdb.Properties.Find(propertyId);

            oldProperty.FloorArea = newProperty.FloorArea;
            oldProperty.Name = newProperty.Name;
            oldProperty.NumbBedrooms = newProperty.NumbBedrooms;
            oldProperty.Postcode = newProperty.Postcode;
            oldProperty.PropertyType = emdb.PropertyTypes.Find(newProperty.PropertyType.Id);

            emdb.SaveChanges();
            return oldProperty;
        }

        /// <summary>
        /// Update note with specified id.  Child objects ARE NOT updated.
        /// </summary>
        /// <param name="noteId"></param>
        /// <param name="newNote"></param>
        /// <returns></returns>
        public override Annotation editNote(int noteId, Annotation newNote)
        {
            Annotation oldNote = emdb.Annotations.Find(noteId);

            oldNote.Date = newNote.Date;
            oldNote.Note = newNote.Note;

            emdb.SaveChanges();
            return oldNote;
        }

        public override MeterReading editMeterReading(int meterReadingId, MeterReading newReading)
        {
            MeterReading oldReading = emdb.MeterReadings.Find(meterReadingId);

            oldReading.Consumption = newReading.Consumption;
            oldReading.Date = newReading.Date;
            oldReading.Reading = newReading.Reading;

            emdb.SaveChanges();
            return oldReading;
        }

        public override BenchmarkProperty editBenchmark(int benchmarkId, BenchmarkProperty newBenchmark)
        {
            BenchmarkProperty oldBenchmark = emdb.Benchmarks.Find(benchmarkId);

            oldBenchmark.BenchmarkCO2Good = newBenchmark.BenchmarkCO2Good;
            oldBenchmark.BenchmarkCO2Typical = newBenchmark.BenchmarkCO2Typical;
            oldBenchmark.BenchmarkkWhGood = newBenchmark.BenchmarkkWhGood;
            oldBenchmark.BenchmarkkWhTypical = newBenchmark.BenchmarkkWhTypical;
            oldBenchmark.FloorArea = newBenchmark.FloorArea;
            oldBenchmark.typeAveragekWh = newBenchmark.typeAveragekWh;
            oldBenchmark.typeBestkWh = newBenchmark.typeBestkWh;

            emdb.SaveChanges();
            return oldBenchmark;
        }
        

//* * * SET METHODS - all data storage methods

        /// <summary>
        /// Store the user object in the database
        /// </summary>
        /// <param name="user"></param>
        /// <returns>id of created user</returns>
        public override int saveUser(User user)
        {
            emdb.Users.Add(user);
            emdb.SaveChanges();
            return user.Id;
        }


        /// <summary>
        /// Store the property object in the database
        /// </summary>
        /// <param name="property"></param>
        /// <returns>id of the created property</returns>
        public override int saveProperty(Property property)
        {
            emdb.Properties.Add(property);
            emdb.SaveChanges();
            return property.Id;
        }


        /// <summary>
        /// Store the meter object in the database
        /// </summary>
        /// <param name="meter"></param>
        public override int saveMeter(Meter meter)
        {
            emdb.Meters.Add(meter);
            emdb.SaveChanges();
            return meter.Id;
        }

        /// <summary>
        /// Saves the meter reading object to the database
        /// </summary>
        /// <param name="meterReading"></param>
        public override int saveMeterReading(MeterReading meterReading)
        {
            emdb.MeterReadings.Add(meterReading);
            emdb.SaveChanges();
            return meterReading.Id;
        }


        /// <summary>
        /// Saves the invoice object to the databse
        /// </summary>
        /// <param name="invoice"></param>
        public override int saveInvoice(Invoice invoice)
        {
            emdb.Invoices.Add(invoice);
            emdb.SaveChanges();
            return invoice.Id;
        }

        /// <summary>
        /// Saves the tariff object to the databse
        /// </summary>
        /// <param name="tariff"></param>
        public override int saveTariff(Tariff tariff)
        {
            tariff.Bands = new List<TariffBand>();
            emdb.Tariffs.Add(tariff);
            emdb.SaveChanges();
            return tariff.Id;
        }


        /// <summary>
        /// Saves a tariff band object to the database
        /// </summary>
        /// <param name="tariffBand"></param>
        public override int saveTariffBand(TariffBand tariffBand)
        {
            emdb.TariffBands.Add(tariffBand);
            emdb.SaveChanges();
            return tariffBand.Id;
        }

        /// <summary>
        /// Saves the annotation object to the database
        /// </summary>
        /// <param name="note"></param>
        public override int saveNote(Annotation note)
        {
            emdb.Annotations.Add(note);
            emdb.SaveChanges();
            return note.Id;
        }



//* * * DELETE METHODS - for removing objects from the database
        
        public override void deleteInvoice(int invoiceId)
        {
            emdb.Invoices.Remove(emdb.Invoices.Find(invoiceId));
            emdb.SaveChanges();
        }

        public override void deleteMeter(int meterId)
        {
            Meter meter = emdb.Meters.Find(meterId);

            ///remove child objects
            meter.Invoices.ToList().ForEach(inv => deleteInvoice(inv.Id));
            meter.Register.ToList().ForEach(rdg => deleteReading(rdg.Id));
            meter.Tariffs.ToList().ForEach(tariff => deleteTariff(tariff.Id));
            meter.Notes.ToList().ForEach(note => deleteNote(note.Id));

            emdb.Meters.Remove(emdb.Meters.Find(meterId));
            emdb.SaveChanges();
        }

        public override void deleteNote(int noteId)
        {
            emdb.Annotations.Remove(emdb.Annotations.Find(noteId));
            emdb.SaveChanges();
        }

        public override void deleteProperty(int propertyId)
        {
            Property property = emdb.Properties.Find(propertyId);
            property.Meters.ToList().ForEach(meter => deleteMeter(meter.Id));
            emdb.Properties.Remove(emdb.Properties.Find(propertyId));
            emdb.SaveChanges();
        }

        public override void deleteReading(int readingId)
        {
            emdb.MeterReadings.Remove(emdb.MeterReadings.Find(readingId));
            emdb.SaveChanges();
        }

        public override void deleteTariff(int tariffId)
        {
            Tariff tariff = emdb.Tariffs.Find(tariffId);
            tariff.Bands.ToList().ForEach(tb => deleteTariffBand(tb.Id));
            emdb.Tariffs.Remove(emdb.Tariffs.Find(tariffId));
            emdb.SaveChanges();
        }

        public override void deleteTariffBand(int bandId)
        {
            emdb.TariffBands.Remove(emdb.TariffBands.Find(bandId));
            emdb.SaveChanges();
        }

//* * * LINKING METHODS

        public override void addPropertyToUser(int propertyId, int userId)
        {
            User user = emdb.Users.Find(userId);
            Property property = emdb.Properties.Find(propertyId);

            user.Properties.Add(property);
            emdb.SaveChanges();
        }

        public override void addMeterToProperty(int meterId, int propertyId)
        {
            Meter meter = emdb.Meters.Find(meterId);
            Property property = emdb.Properties.Find(propertyId);

            property.Meters.Add(meter);
            emdb.SaveChanges();
        }

        public override void addInvoiceToMeter(int invoiceId, int meterId)
        {
            Invoice invoice = emdb.Invoices.Find(invoiceId);
            Meter meter = emdb.Meters.Find(meterId);

            meter.Invoices.Add(invoice);
            addMeterToInvoice(meterId, invoiceId);
            emdb.SaveChanges();
        }

        public override void addMeterToInvoice(int meterId, int invoiceId)
        {
            Invoice invoice = emdb.Invoices.Find(invoiceId);
            Meter meter = emdb.Meters.Find(meterId);

            invoice.Meter = meter;
            emdb.SaveChanges();
        }

        public override void addTariffToMeter(int tariffId, int meterId)
        {
            Tariff tariff = emdb.Tariffs.Find(tariffId);
            Meter meter = emdb.Meters.Find(meterId);

            meter.Tariffs.Add(tariff);
            emdb.SaveChanges();
        }

        public override void addBandToTariff(int bandId, int tariffId)
        {
            Tariff tariff = emdb.Tariffs.Find(tariffId);
            TariffBand band = emdb.TariffBands.Find(bandId);

            tariff.Bands.Add(band);
            emdb.SaveChanges();
        }

        public override void addNoteToMeter(int noteId, int meterId)
        {
            Annotation note = emdb.Annotations.Find(noteId);
            Meter meter = emdb.Meters.Find(meterId);

            meter.Notes.Add(note);
            emdb.SaveChanges();
        }

        public void addReadingToMeter(int readingId, int meterId)
        {
            MeterReading reading = emdb.MeterReadings.Find(readingId);
            Meter meter = emdb.Meters.Find(meterId);

            meter.Register.Add(reading);
            emdb.SaveChanges();
        }
    }
}
