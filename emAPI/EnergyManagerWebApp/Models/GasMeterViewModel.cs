using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using emAPI.ClassLibrary;

namespace EnergyManagerWebApp.Models
{
    public class GasMeterViewModel
    {
        public GasMeter Meter { get; set; }
        public DateTime StartDate { get; set; }

        public int BelongsToProperty { get; set; }

        public Dictionary<string, double> meterCoefficients { get; set; }

        public GasMeterViewModel()
        {
            meterCoefficients = new Dictionary<string,double>();

            meterCoefficients.Add("cubic meters", 1);
            meterCoefficients.Add("cubic feet", 0.028316846);
            meterCoefficients.Add("10s of cubic feet", 0.28316846);
            meterCoefficients.Add("100s of cubic feet", 2.8316846);
        }
    }
}