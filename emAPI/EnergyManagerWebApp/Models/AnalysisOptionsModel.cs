using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using emAPI.ClassLibrary;

namespace EnergyManagerWebApp.Models
{
    public class AnalysisOptionsModel
    {
        public List<Period> Periods { get; set; }
        public int periodId { get; set; }

        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }

        public bool Success { get; set; }
        public int propertyId { get; set; }
        public PropertyViewModel property { get; set; }
        public List<Property> UsersProperties { get; set; }

        public AnalysisOptionsModel()
        {
            Periods = new List<Period>
            {
                new Period { Id = 5, Length = "Annually", NumbDays = 365 },
                new Period { Id = 4, Length = "Quarterly", NumbDays = 90 },
                new Period { Id = 3, Length = "Monthly", NumbDays = 30 },
                new Period { Id = 2, Length = "Weekly", NumbDays = 7 },
            };
        }
    }
}