
/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
 * DAVID SAUNTSON 12993011                                               *
 * MSc Software Engineering - Final Year Project                         *
 *                                                                       *
 * Energy Management API & Software                                      *
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
 * 
 * PropertyMobile.cs - 
 * Code source: Handwritten
 */
		

using System;
using System.Collections.Generic;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace EnergyManagerMobile.Models
{
    public class PropertyMobile
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<MeterMobile> Meters {get; set;}
        public int BelongsToUser { get; set; }
    }
}
