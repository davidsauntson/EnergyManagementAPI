
/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
 * DAVID SAUNTSON 12993011                                               *
 * MSc Software Engineering - Final Year Project                         *
 *                                                                       *
 * Energy Management API & Software                                      *
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
 * 
 * MeterReadingViewModel.cs - 
 * Code source: Handwritten
 */
		

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using emAPI.ClassLibrary;
using EnergyManagerWebApp.CustomValidation;

namespace EnergyManagerWebApp.Models
{
    public class MeterReadingViewModel
    {
        public MeterReading MtrReading { get; set; }
        public DateTime MinimumDate { get; set; }
        public DateTime MinimumEditDate { get; set; }

        public int BelongsToMeter { get; set; }
        public int BelongsToProperty { get; set; }
        public string Fuel { get; set; }

        [DateGreaterThan("MinimumDate")]
        public DateTime Date { get; set; }

    }
}