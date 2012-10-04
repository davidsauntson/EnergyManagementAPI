
/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
 * DAVID SAUNTSON 12993011                                               *
 * MSc Software Engineering - Final Year Project                         *
 *                                                                       *
 * Energy Management API & Software                                      *
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
 * 
 * EMDatabaseStats.cs
 * Code source: Handwritten
 */
		

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using emAPI.ClassLibrary;

namespace emAPI.Controllers
{
    /// <summary>
    /// Responsible for updating benchmark statistics and comparative performance operations.
    /// </summary>
    internal class EMDatabaseStats
    {
        /// <summary>
        /// Updates the average annual kWh and best annual kWh amounts for each benchmark type by assessing those
        /// attributes of each property in the database.
        /// </summary>
        internal static void updateBenchmarkStats()
        {
            EMMediator mediator = new EMMediator();

            List<BenchmarkProperty> benchmarks = mediator.DataManager.getBenchmarkProperties();
            foreach(BenchmarkProperty benchmark in benchmarks)
            {
                benchmark.typeAveragekWh = mediator.DataManager.getTypeAverageAnnualkWh(benchmark.Id);
                benchmark.typeBestkWh = mediator.DataManager.getTypeBestAnnualkWh(benchmark.Id);
                mediator.DataManager.editBenchmark(benchmark.Id, benchmark);
            }
        }

        /// <summary>
        /// Returns a list of anonymous properties with postcode and annual costs populated.
        /// </summary>
        /// <returns>List[AnonymousProperty] with postcode and annual cost for each property in the database</returns>
        internal static List<AnonymousProperty> getAllPropertyAnnualCosts()
        {
            EMMediator mediator = new EMMediator();
            List<Property> allProperties = mediator.DataManager.getAllProperties();
            List<AnonymousProperty> result = new List<AnonymousProperty>();

            foreach (Property property in allProperties)
            {
                AnonymousProperty anonProperty = new AnonymousProperty();
                anonProperty.Postcode = property.Postcode;
                anonProperty.AnnualCost = property.AnnualCost;
                anonProperty.Id = property.Id;
                
                result.Add(anonProperty);
            }

            return result;
        }
    }
}
