
/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
 * DAVID SAUNTSON 12993011                                               *
 * MSc Software Engineering - Final Year Project                         *
 *                                                                       *
 * Energy Management API & Software                                      *
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
 * 
 * PropertyViewModel.cs - 
 * Code source: Handwritten
 */
		

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using emAPI.ClassLibrary;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace EnergyManagerWebApp.Models
{
    public class PropertyViewModel
    {
        public Property Property { get; set; }
        public BenchmarkProperty Benchmark { get; set; }

        public int BelongsToUser { get; set; }

        public HeatingType HeatingType { get; set; }
        public BuildingType BuildingType { get; set; }
        public WallType WallType { get; set; }

        [Range(0, 99999999)]
        public int? NumbBedrooms { get; set; }

        [Range(0, 99999999)]
        public int? FloorArea { get; set; }

        public List<HeatingType> HeatingTypes { get; set; }
        public List<BuildingType> BuildingTypes { get; set; }
        public List<WallType> WallTypes { get; set; }

        public double BenchmarkkWhNormalisedGood { get; set; }
        public double BenchmarkkWhNormalisedTypical { get; set; }

    }
}