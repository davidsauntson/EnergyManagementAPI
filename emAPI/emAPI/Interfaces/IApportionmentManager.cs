
/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
 * DAVID SAUNTSON 12993011                                               *
 * MSc Software Engineering - Final Year Project                         *
 *                                                                       *
 * Energy Management API & Software                                      *
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
 * 
 * IApportionmentManager.cs 
 * Code source: Handwritten
 */
		

using System;
using emAPI.ClassLibrary;

namespace emAPI.Interfaces
{
    /// <summary>
    /// Interface for AnnotationManager objects
    /// </summary>
    internal interface IApportionmentManager
    {
        double apportionToDates(System.Collections.Generic.List<Chunk> dataIn, string startDate, string endDate);
        System.Collections.Generic.List<Chunk> apportionToPeriod(System.Collections.Generic.List<Chunk> dataIn, DateTime startDate, DateTime endDate, AppotionmentPeriod interval);
        System.Collections.Generic.List<Chunk> convertInvoicesToChunks(System.Collections.Generic.List<Invoice> invoices);
        System.Collections.Generic.List<Chunk> convertReadingsToChunks(System.Collections.Generic.List<MeterReading> readings);
        AppotionmentPeriod getBestApportionmentPeriod(DateTime startDate, DateTime endDate);
        System.Collections.Generic.List<Chunk> setupDatedChunksForApportionToPeriod(DateTime startDate, DateTime endDate, AppotionmentPeriod interval);
    }
}
