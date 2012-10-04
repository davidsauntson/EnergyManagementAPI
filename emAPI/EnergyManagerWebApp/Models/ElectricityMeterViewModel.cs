using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using emAPI.ClassLibrary;
using EnergyManagerWebApp.CustomValidation;

namespace EnergyManagerWebApp.Models
{
    public class ElectricityMeterViewModel
    {
        public ElectricityMeter Meter { get; set; }

        [DateNotInTheFuture]
        public DateTime StartDate { get; set; }
        public int BelongsToProperty { get; set; }

    }
}