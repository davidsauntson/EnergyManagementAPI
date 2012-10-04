/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
 * DAVID SAUNTSON 12993011                                               *
 * MSc Software Engineering - Final Year Project                         *
 *                                                                       *
 * Energy Management API & Software                                      *
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
 * 
 * EMDatabase.cs 
 * Code source: Handwritten
 */
		

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using emAPI.ClassLibrary;
using emAPI.DAL;

namespace emAPI.DAL
{
    /// <summary>
    /// An entity framework 4.1 implementation of the databse
    /// </summary>
    class EMDatabase : DbContext
    {
        internal EMDatabase()
        {
            //Seed method to populate the database with sample data
            System.Data.Entity.Database.SetInitializer(new SampleData());
        }

        //database tables
        public DbSet<User> Users { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<Meter> Meters { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<MeterReading> MeterReadings { get; set; }
        public DbSet<Tariff> Tariffs { get; set; }
        public DbSet<TariffBand> TariffBands { get; set; }
        public DbSet<Annotation> Annotations { get; set; }
        public DbSet<BenchmarkProperty> Benchmarks { get; set; }
        public DbSet<PropertyType> PropertyTypes { get; set; }
        public DbSet<HeatingType> HeatingTypes { get; set; }
        public DbSet<WallType> WallTypes { get; set; }
        public DbSet<BuildingType> BuildingTypes { get; set; }
        public DbSet<Period> Periods { get; set; }
    }
}
