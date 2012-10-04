
/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
 * DAVID SAUNTSON 12993011                                               *
 * MSc Software Engineering - Final Year Project                         *
 *                                                                       *
 * Energy Management API & Software                                      *
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
 * 
 * TariffViewModel.cs
 * Code source: Handwritten
 */
		
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using emAPI.ClassLibrary;
using System.ComponentModel.DataAnnotations;
using EnergyManagerWebApp.CustomValidation;

namespace EnergyManagerWebApp.Models
{
    public class TariffViewModel
    {
        [DateGreaterThan("MinimumStartDate", ErrorMessage="Must be greater than minimum start date")]
        public DateTime StartDate { get; set; }
        public DateTime MinimumStartDate { get; set; }

        public int Id { get; set; }

        public double SCValue { get; set; }
        public int SCPeriodId { get; set; }

        public bool IsBanded { get; set; }
        public int BandPeriodId { get; set; }

        [IntGreaterThanAttribute("LowerLimit1", ErrorMessage="Must be greater than 1st lower limit")]
        [Range(0,9999999, ErrorMessage="Enter valid limit in kWh")]
        public int? UpperLimit1 { get; set; }

        [ZeroOrGreaterThanAttribute("UpperLimit1", ErrorMessage="Must be greater than 1st upper limit")]
        [Range(0, 9999999, ErrorMessage="Enter valid limit in kWh")]
        public int? UpperLimit2 { get; set; }


        [Range(0, 0)]
        public int? UpperLimit3 { get; set; }

        public int meterId { get; set; }
        public string fuel { get; set;}
        public int BelongsToProperty { get; set; }


        [Range(0, 0)]
        public int? LowerLimit1 { get; set; }

        [IntGreaterThanAttribute("UpperLimit1")]
        [Range(0, 9999999, ErrorMessage="Enter valid limit in kWh")]
        public int? LowerLimit2 { get; set; }

        [IntGreaterThanAttribute("UpperLimit2")]
        [Range(0, 9999999, ErrorMessage="Enter valid limit in kWh")]
        public int? LowerLimit3 { get; set; }


        [Range(0.0001, 9999999, ErrorMessage="Enter valid rate in pence")]
        public double Rate1 { get; set; }

        [Range(0, 9999999, ErrorMessage="Enter valid rate in pence")]
        public double Rate2 { get; set; }

        [Range(0, 9999999, ErrorMessage="Enter valid rate in pence")]
        public double Rate3 { get; set; }


        public List<Period> Periods { get; set; }

        public int Band1Id { get; set; }
        public int Band2Id { get; set; }
        public int Band3Id { get; set; }

        public TariffViewModel()
        {
            Periods = new List<Period>
            {
                new Period { Id = 1, Length = "Annually", NumbDays = 365 },
                new Period { Id = 2, Length = "Quarterly", NumbDays = 90 },
                new Period { Id = 3, Length = "Monthly", NumbDays = 30 },
                new Period { Id = 4, Length = "Weekly", NumbDays = 7 },
                new Period { Id = 5, Length = "Daily", NumbDays = 1 }
            };

        }
    }
}