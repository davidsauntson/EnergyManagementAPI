
/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
 * DAVID SAUNTSON 12993011                                               *
 * MSc Software Engineering - Final Year Project                         *
 *                                                                       *
 * Energy Management API & Software                                      *
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
 * 
 * InvoiceViewModel.cs - 
 * Code source: Handwritten
 */
		

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using emAPI.ClassLibrary;

namespace EnergyManagerWebApp.Models
{
    public class InvoiceViewModel
    {
        public Invoice Invoice { get; set; }
        public int MeterId { get; set; }
        public int BelongsToProperty { get; set; }
        public string Fuel { get; set; }
    }
}