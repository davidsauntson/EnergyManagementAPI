
/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
 * DAVID SAUNTSON 12993011                                               *
 * MSc Software Engineering - Final Year Project                         *
 *                                                                       *
 * Energy Management API & Software                                      *
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
 * 
 * MeterMobile.cs - 
 * Code source: Handwritten
 */
		

using System;

namespace EnergyManagerMobile.Models
{
    public class MeterMobile
    {
        public int Id { get; set; }
        public string SerialNo { get; set; }
        public string MinimumDate { get; set; }
        public string Fuel { get; set; }
        public int BelongsToProperty { get; set; }
        public int NumbDigits { get; set; }
    }
}
