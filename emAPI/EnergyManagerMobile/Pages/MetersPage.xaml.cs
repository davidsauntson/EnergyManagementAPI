
/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
 * DAVID SAUNTSON 12993011                                               *
 * MSc Software Engineering - Final Year Project                         *
 *                                                                       *
 * Energy Management API & Software                                      *
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
 * 
 * MeterPage.cs - LOGIC
 * Code source: Handwritten
 */
		

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using Newtonsoft.Json;
using EnergyManagerMobile.Models;

namespace EnergyManagerMobile.Pages
{
    public partial class MetersPage : PhoneApplicationPage
    {
        emAPIMobileService.Service1Client emAPIMobile = new emAPIMobileService.Service1Client();

        public MetersPage()
        {
            InitializeComponent();
        }

        public MetersPage(int propertyId)
        {
            InitializeComponent();
            emAPIMobile.getMetersForPropertyCompleted += new EventHandler<emAPIMobileService.getMetersForPropertyCompletedEventArgs>(emAPIMobile_getMetersForPropertyCompleted);
            progBar.Visibility = System.Windows.Visibility.Visible;
            emAPIMobile.getMetersForPropertyAsync(propertyId);
        }

        void emAPIMobile_getMetersForPropertyCompleted(object sender, emAPIMobileService.getMetersForPropertyCompletedEventArgs e)
        {
            string metersJSON = e.Result;
            

            if (metersJSON != ResponseCodes.Error.ToString())
            {
                List<MeterMobile> meters = JsonConvert.DeserializeObject<List<MeterMobile>>(metersJSON);
                lbxMeters.ItemsSource = new List<MeterMobile>(meters.Count);
                lbxMeters.ItemsSource = meters.ToList();
                progBar.Visibility = System.Windows.Visibility.Collapsed;
            }
            else
            {
                MessageBox.Show("There was an error contact the database.");
                this.Content = new MainPage();
            }
        }

        private void GestureListener_Tap(object sender, Microsoft.Phone.Controls.GestureEventArgs e)
        {
            MeterMobile selectedMeter = lbxMeters.SelectedItem as MeterMobile;
            this.Content = new ReadingPage(selectedMeter);
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Content = new PropertiesPage(1);
        }

    }
}