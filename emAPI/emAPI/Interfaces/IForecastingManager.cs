
/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
 * DAVID SAUNTSON 12993011                                               *
 * MSc Software Engineering - Final Year Project                         *
 *                                                                       *
 * Energy Management API & Software                                      *
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
 * 
 * IForecastingManager.cs
 * Code source: Handwritten
 */
		

using System;
using emAPI.ClassLibrary;

namespace emAPI.Interfaces
{
    /// <summary>
    /// Interface for ForecastingManager objects
    /// </summary>
    interface IForecastingManager
    {
        Invoice forecastNextInvoice(int meterId);
    }
}
