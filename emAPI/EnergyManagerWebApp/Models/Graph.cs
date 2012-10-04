using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EnergyManagerWebApp.Models
{
    public class Graph
    {
        public string Title { get; set; }
        public string ChartType { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public string Data { get; set; }
        public string Legend { get; set; }
        public string hAxisTitle { get; set; }
        public string vAxisTitle { get; set; }
        public string hAxisFormat { get; set; }
        public string vAxisFormat { get; set; }
        public int Baseline { get; set; }
        public int chartAreaWidth { get; set; }
    }
}
