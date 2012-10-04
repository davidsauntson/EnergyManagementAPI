
/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
 * DAVID SAUNTSON 12993011                                               *
 * MSc Software Engineering - Final Year Project                         *
 *                                                                       *
 * Energy Management API & Software                                      *
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
 * 
 * ReadingPage.cs - LOGIC
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

namespace EnergyManagerMobile.Pages
{
    public partial class ReadingPage : PhoneApplicationPage
    {
        private MeterMobile Meter;
        private emAPIMobileService.Service1Client emAPIMobile = new emAPIMobileService.Service1Client();

        public ReadingPage()
        {
            InitializeComponent();
        }

        public ReadingPage(MeterMobile meter)
        {
            InitializeComponent();
            Meter = meter;
            txtSerialNo.Text = Meter.SerialNo;
            txtSerialNo.Visibility = System.Windows.Visibility.Visible;
            txtFuel.Text = Meter.Fuel;
            txtFuel.Visibility = System.Windows.Visibility.Visible;
            progBar.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            txtMessage.Visibility = System.Windows.Visibility.Collapsed;

            if (string.IsNullOrWhiteSpace(txtDate.Text) | string.IsNullOrWhiteSpace(txtReading.Text))
            {
                txtMessage.Text = "Please enter date and reading.";
                txtMessage.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                DateTime date;
                int reading;

                IFormatProvider UKculture = new System.Globalization.CultureInfo("en-GB");

                try
                {
                    date = Convert.ToDateTime(txtDate.Text, UKculture);
                    if (date < Convert.ToDateTime(Meter.MinimumDate, UKculture))
                    {
                        throw new Exception();
                    }
                }
                catch (Exception ex)
                {
                    txtMessage.Text = "Please enter a valid date, greater than " + Meter.MinimumDate;
                    txtMessage.Visibility = System.Windows.Visibility.Visible;
                    return;
                }

                try
                {
                    reading = int.Parse(txtReading.Text);
                    if (txtReading.Text.Length > Meter.NumbDigits)
                    {
                        throw new Exception();
                    }
                }
                catch (Exception)
                {
                    txtMessage.Text = "Please enter a valid reading, with no more than " + Meter.NumbDigits.ToString() + "digits";
                    txtMessage.Visibility = System.Windows.Visibility.Visible;
                    return;
                }

                emAPIMobile.createMeterReadingCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(emAPIMobile_createMeterReadingCompleted);

                try
                {
                    progBar.Visibility = System.Windows.Visibility.Visible;
                    emAPIMobile.createMeterReadingAsync(date.ToString("dd/MM/yyy"), reading, Meter.Id, Meter.BelongsToProperty);
                }
                catch
                {
                    progBar.Visibility = System.Windows.Visibility.Collapsed;
                    MessageBox.Show("There was a problem submitting the reading.  Please try again later.");
                }


            }
        }

        void emAPIMobile_createMeterReadingCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            progBar.Visibility = System.Windows.Visibility.Collapsed;
            MessageBox.Show("Reading submitted successfully");
            this.Content = new MetersPage(Meter.BelongsToProperty);
        }

        private void GestureListener_Tap(object sender, Microsoft.Phone.Controls.GestureEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            textBox.Text = "";
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Content = new MetersPage(Meter.BelongsToProperty);
        }
    }
}