
/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
 * DAVID SAUNTSON 12993011                                               *
 * MSc Software Engineering - Final Year Project                         *
 *                                                                       *
 * Energy Management API & Software                                      *
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
 * 
 * PropertiesPage.cs - LOGIC
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
using EnergyManagerMobile.Models;
using Newtonsoft.Json;

namespace EnergyManagerMobile.Pages
{
    public partial class PropertiesPage : PhoneApplicationPage
    {


        internal int UserId;
        emAPIMobileService.Service1Client emAPIMobile = new emAPIMobileService.Service1Client();

        public PropertiesPage()
        {
            InitializeComponent();
        }

        public PropertiesPage(int id)
        {
            InitializeComponent();
            UserId = id;
            emAPIMobile.getPropertiesForUserCompleted +=new EventHandler<emAPIMobileService.getPropertiesForUserCompletedEventArgs>(emAPIMobile_getPropertiesForUserCompleted);
            progBar.Visibility = System.Windows.Visibility.Visible;
            emAPIMobile.getPropertiesForUserAsync(UserId);
        }

        void emAPIMobile_getPropertiesForUserCompleted(object sender, emAPIMobileService.getPropertiesForUserCompletedEventArgs e)
        {
            string propertiesJSON = e.Result;
            if (propertiesJSON != ResponseCodes.Error.ToString())
            {
                List<PropertyMobile> properties = JsonConvert.DeserializeObject<List<PropertyMobile>>(propertiesJSON);
                lbxProperties.ItemsSource = properties;
                progBar.Visibility = System.Windows.Visibility.Collapsed;
            }
            else
            {
                MessageBox.Show("There was an error contacting the database");
                this.Content = new MainPage();
            }
        }

        private void GestureListener_Tap(object sender, Microsoft.Phone.Controls.GestureEventArgs e)
        {
            this.Content = new MetersPage((int)lbxProperties.SelectedValue);
        }



    }
}