
/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
 * DAVID SAUNTSON 12993011                                               *
 * MSc Software Engineering - Final Year Project                         *
 *                                                                       *
 * Energy Management API & Software                                      *
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
 * 
 * MeterReadingListViewModel.cs - 
 * Code source: Handwritten
 */
		

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using emAPI.ClassLibrary;

namespace EnergyManagerWebApp.Models
{
    public class MeterReadingListViewModel
    {
        public List<MeterReading> Readings { get; set; }
        public MeterViewModel MeterDetails { get; set; }
    }
}