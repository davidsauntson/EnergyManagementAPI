using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using emAPI.ClassLibrary;

namespace EnergyManagerWebApp.Models
{
    public class InvoiceListViewModel
    {
        public List<Invoice> Invoices { get; set; }
        public Meter MeterDetails { get; set; }
        public int BelongsToProperty { get; set; }
    }
}