
/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
 * DAVID SAUNTSON 12993011                                               *
 * MSc Software Engineering - Final Year Project                         *
 *                                                                       *
 * Energy Management API & Software                                      *
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
 * 
 * MeterManager.cs
 * Code source: Handwritten
 */
		

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using emAPI.ClassLibrary;
using emAPI.Interfaces;

namespace emAPI.Controllers
{
    /// <summary>
    /// Controller object for Meter model object related operations
    /// </summary>
    internal class MeterManager : IMeterManager
    {
        internal EMMediator mediator;

        internal MeterManager()
        {
            mediator = new EMMediator();
        }

        //* * * DATA ACCESS METHODS

        public Meter getMeter(int meterId)
        {
            Meter meter = mediator.DataManager.getMeter(meterId);
            ///reorder meter readings by date

            meter.Register = meter.Register.OrderByDescending(mr => mr.Date).ToList();
            return meter;
        }

        public MeterReading getReading(int readingId)
        {
            MeterReading reading = mediator.DataManager.getReading(readingId);
            return reading;
        }

        private MeterReading getNextReading(Meter meter, MeterReading reading)
        {
            ///deal with no meter readings
            if (meter.Register == null | meter.Register.Count == 0)
            {
                return null;
            }
            else
            {
                meter.Register = meter.Register.OrderBy(rdg => rdg.Date).ToList();
                foreach (MeterReading rdg in meter.Register)
                {
                    ///register is in ascending date order so the first reading with a date greater than the
                    ///submitted reading is the required reading
                    if (rdg.Date > reading.Date)
                        return rdg;
                }

                ///if all readings are younger than the submitted reading return null
                return null;
            }
        }


        public List<MeterReading> getMeterReadings(int meterId)
        {
            List<MeterReading> readings = mediator.DataManager.getMeterReadings(meterId);
            return readings;
        }


        public List<Meter> getMeters(int propertyId)
        {
            List<Meter> meters = mediator.DataManager.getMeters(propertyId);
            return meters;
        }


        public Meter getDetailsForMeter(int meterId)
        {
            Meter meter = mediator.DataManager.getDetailsForMeter(meterId);
            return meter;
        }

        //* * * CREATE METHODS

        public int createElectricityMeter(string serialNo, double scalingFactor, int numbDigits, string startDate, int propertyId)
        {
            ElectricityMeter newMeter = new ElectricityMeter
            {
                SerialNo = serialNo,
                ScalingFactor = scalingFactor,
                KWhtoCO2ConversionFactor = EMConverter.eleckWhFactor,
                NumbDigits = numbDigits,
                StartDate = Convert.ToDateTime(startDate)
            };

            ///add meter to property
            int meterId = mediator.DataManager.saveMeter(newMeter);
            mediator.DataManager.addMeterToProperty(meterId, propertyId);

            return meterId;
        }

        public int createGasMeter(string serialNo, double meterCoefficient, int numDigits, string startDate, int propertyId)
        {
            GasMeter newMeter = new GasMeter
            {
                SerialNo = serialNo,
                MeterCoefficient = meterCoefficient,    ///converts units of meter into m3 varies from meter to meter

                ///fixed conversion factors
                CorrectionFactor = EMConverter.gasCorrectionFactor,         ///corrects for temperature & pressure
                CalorificValue = EMConverter.gasCalorificValue,              ///standard value representing the energy in 1m3 of natural gas
                KWhtoCO2ConversionFactor = EMConverter.gaskWhFactor,  
                NumbDigits = numDigits,
                StartDate = Convert.ToDateTime(startDate)
            };

            ///add meter to property
            int meterId = mediator.DataManager.saveMeter(newMeter);
            mediator.DataManager.addMeterToProperty(meterId, propertyId);

            return meterId;
        }

        public int createMeterReading(string date, int reading, int meterId, int propertyId)
        {
            Meter meter = mediator.DataManager.getMeter(meterId);

            MeterReading newReading = new MeterReading
            {
                Date = Convert.ToDateTime(date),
                Reading = reading
            };

            ///check if this is the first reading for this meter
            if (meter.Register.Count == 0)
            {
                MeterReading firstReading = new MeterReading();
                firstReading.Date = newReading.Date;
                firstReading.Reading = newReading.Reading;
                firstReading.Consumption = 0;

                int firstReadingId = mediator.DataManager.saveMeterReading(firstReading);
                mediator.DataManager.addReadingToMeter(firstReadingId, meter.Id);
                return firstReadingId;
            }

            newReading.Consumption = calculatekWh(newReading, meter);
            int readingId = mediator.DataManager.saveMeterReading(newReading);
            mediator.DataManager.addReadingToMeter(readingId, meterId);

            ///update property's annual kWh total

            mediator.updatePropertyAnnualTotalkWh(propertyId);
            mediator.updateAnnualCO2(propertyId);

            return readingId;

        }


        //* * * UPDATE METHODS

        public Meter editMeter(int meterId, string meterJSON)
        {
            Meter updatedMeter = EMConverter.fromJSONtoA<Meter>(meterJSON);
            updatedMeter = mediator.DataManager.editMeter(meterId, updatedMeter);
            return updatedMeter;
        }

        public void editMeterReading(int readingId, int meterId, string readingJSON, int propertyId)
        {
            ///desrialise reading and update in the database
            MeterReading updatedReading = EMConverter.fromJSONtoA<MeterReading>(readingJSON);
            mediator.DataManager.editMeterReading(readingId, updatedReading);

            ///get the meter (now with updated reading) and recalc the consumption for each reading
            Meter meter = mediator.DataManager.getMeter(meterId);
            meter.Register = meter.Register.OrderBy(rdg => rdg.Date).ToList();
            foreach (MeterReading rdg in meter.Register)
            {
                if (rdg.Date >= updatedReading.Date)
                {
                    MeterReading revisedReading = new MeterReading 
                    { 
                        Date = rdg.Date,
                        Reading = rdg.Reading
                    };

                    revisedReading.Consumption = calculatekWh(revisedReading, meter);
                    mediator.DataManager.editMeterReading(rdg.Id, revisedReading);
                }
            }

            mediator.updateAnnualCO2(propertyId);
            mediator.updatePropertyAnnualTotalkWh(propertyId);
        }


        //* * * DELETE METHODS

        public void deleteMeter(int meterId, int propertyId)
        {
            mediator.DataManager.deleteMeter(meterId);

            mediator.updateAnnualCO2(propertyId);
            mediator.updatePropertyAnnualTotalkWh(propertyId);
        }

        public void deleteReading(int readingId, int meterId, int propertyId)
        {
            Meter meter = mediator.DataManager.getMeter(meterId);
            MeterReading deletedRead = mediator.DataManager.getReading(readingId);
            MeterReading nextRead = getNextReading(meter, deletedRead);
            
            ///check the deleted reading isn't the only or oldest reading in the list
            if(nextRead != null)
            {
                ///the consumption in the deleted reading needs to be added to the consumption
                ///of the next reading to maintain data integrity
                MeterReading updatedReading = new MeterReading
                {
                    Date = nextRead.Date,
                    Reading = nextRead.Reading,
                    Consumption = nextRead.Consumption + deletedRead.Consumption
                };

                mediator.DataManager.editMeterReading(nextRead.Id, updatedReading);
            }

            ///if the reading IS the only/oldest we can just delete it
            mediator.DataManager.deleteReading(readingId);


            mediator.updateAnnualCO2(propertyId);
            mediator.updatePropertyAnnualTotalkWh(propertyId);
        }


        //* * * OTHER METHODS

        public void addTariffToMeter(int meterId, int tariffId)
        {
            mediator.DataManager.addTariffToMeter(tariffId, meterId);
        }

        public void addInvoiceToMeter(int invoiceId, int meterId)
        {
            mediator.DataManager.addInvoiceToMeter(invoiceId, meterId);
        }

        

        /// <summary>
        /// Gets the minimum date for a reading to be entered for the specified meter
        /// </summary>
        /// <param name="meterId"></param>
        /// <returns></returns>
        public string getMinimumReadingDate(int meterId)
        {
            Meter meter = mediator.DataManager.getMeter(meterId);
            DateTime minimumDate = meter.StartDate;

            TimeSpan oneDay = new TimeSpan(1, 0, 0, 0);

            if (meter.Register.Count != 0)
            {
                ///there are readings for this meter so we need to get most recent reading date
                foreach (MeterReading rdg in meter.Register)
                {
                    if (rdg.Date > minimumDate)
                        minimumDate = rdg.Date;
                }

                minimumDate = minimumDate + oneDay;
            }

            return minimumDate.ToShortDateString();
        }

        

        /// <summary>
        /// Gets the previous reading on this meter, using the date of the reading object passed in
        /// </summary>
        /// <param name="currentReading"></param>
        /// <param name="meter"></param>
        /// <returns></returns>
        private MeterReading getPreviousReadingOnMeter(MeterReading currentReading, Meter meter)
        {
            MeterReading previousReading = new MeterReading();
            meter.Register = meter.Register.OrderByDescending(rdg => rdg.Date).ToList();
            
            foreach (MeterReading rdg in meter.Register)
            {
                ///where there is only one other reading, the other reading is the start date of the meter
                ///it is acceptable to have a reading on the start date so currentReading date >= rdg date
                if (meter.Register.Count == 1)
                {
                    if (rdg.Date <= currentReading.Date)
                        return rdg;
                }
                else
                {
                    ///there are other non-zero readings, so currentReading date > rdg date
                    if (rdg.Date < currentReading.Date)
                        return rdg;
                }
            }

            ///
            return null;
        }

        /// <summary>
        /// Calculates the energy in kWh for a given meter reading.  Previous reading is retrieved from the Register of the meter and
        /// used in the calculation.  NB kWh calculated to nearest whole number, rounding away from zero.
        /// </summary>
        /// <param name="reading">the MeterReading object of the current reading</param>
        /// <param name="meter">the Id of the meter the reading belongs to</param>
        /// <returns>kWh consumed as integer</returns>
        public int calculatekWh(MeterReading reading, Meter meter)
        {
            int kWh = 0;
            ///find the most recent reading before this one
            ///caculate the units consumed
            ///calculate the energy consumed --> need to know which meter this reading belongs to

            MeterReading previousReading = getPreviousReadingOnMeter(reading, meter);
            int unitsConsumed = calculateUnits(previousReading, reading, meter);

            ///to determine kWh need to know type of meter
            var elecMeter = meter as ElectricityMeter;
            if (elecMeter != null)
            {
                ///must be an electricity meter
                ///only coeff is scaling factor
                ///
                kWh = (int)Math.Round((double)unitsConsumed * elecMeter.ScalingFactor, MidpointRounding.AwayFromZero);
            }
            else
            {
                ///must be a gas meter
                var gasMeter = meter as GasMeter;
                kWh = (int)Math.Round(
                    (double)unitsConsumed 
                    * gasMeter.MeterCoefficient     ///converts from units on meter to m3 (defined in constructor)
                    * gasMeter.CalorificValue       ///converts from m3 into joules (defined in constructor)
                    * gasMeter.CorrectionFactor     ///corrects joules for standard room temp and pressure
                    / EMConverter.fromJoulesTokWh,  ///converts from joules to kWh, static property of EMConverter class
                    MidpointRounding.AwayFromZero);
            }

            return kWh;
        }

        private int calculateUnits(MeterReading previousReading, MeterReading rdg, Meter meter)
        {
            int unitsConsumed = 0;
            string readingAsString = rdg.Reading.ToString();
            ///truncate reading length if greater than no digits on meter
            if (readingAsString.Length > meter.NumbDigits)
            {
                readingAsString = readingAsString.Substring(0, meter.NumbDigits);
                rdg.Reading = int.Parse(readingAsString);
            }

            ///get max possible reading on this meter
            StringBuilder maxRead = new StringBuilder();
            maxRead.Append('9', meter.NumbDigits);
            int maxPossibleReading = int.Parse(maxRead.ToString());

            ///check if the meter has 'ticked over', ie gone past max reading
            if (previousReading.Reading > rdg.Reading)
            {
                ///meter has gone past the max read and started again
                ///therefore need units from previous --> max
                ///and from 0 --> current reading
                ///+1 for max --> 0 (eg 999999 + 1 will take the meter back to 0)
                unitsConsumed = maxPossibleReading - previousReading.Reading + rdg.Reading + 1;
            }
            else
            {
                ///units consumed is just the difference between prev and present
                unitsConsumed = rdg.Reading - previousReading.Reading;
            }

            return unitsConsumed;
        }

        public string getMinimumReadingDateForEdit(int meterId)
        {
            Meter meter = mediator.DataManager.getMeter(meterId);
            DateTime minimumDate = meter.StartDate;

            if (meter.Register.Count > 1)
            {
                ///there is more than one reading on the list and therefore the reading being edited is not the only reading on the meter
                ///therefore minimum date for editing the reading is the start date of the second reading on the list
                meter.Register = meter.Register.OrderByDescending(rdg => rdg.Date).ToList();
                minimumDate = meter.Register.ElementAt(1).Date;
            }

            ///otherwise the minimum start date is the start date of the meter, assigned above
            ///

            return minimumDate.ToString();
        }

        public double getDataFromMeter(int meterId, string startDate, string endDate, int dataTypeId)
        {
            Meter meter = mediator.DataManager.getMeter(meterId);
            List<Chunk> dataIn = new List<Chunk>();

            switch (dataTypeId)
            {
                case 1:     ///energy data
                    dataIn = mediator.convertReadingsToChunks(meter.Register.ToList());
                    break;

                case 2:     ///cost data
                    dataIn = mediator.convertInvoicesToChunks(meter.Invoices.ToList());
                    break;
            }
            
            double result = mediator.apportionToDates(dataIn, startDate, endDate);
            return result;
        }




        //public double getkWhFromMeter(int meterId, string startDate, string endDate)
        //{
        //    Meter meter = mediator.DataManager.getMeter(meterId);
        //    List<Chunk> dataIn = mediator.convertReadingsToChunks(meter.Register.ToList());

        //    double kWh = mediator.apportionToDates(dataIn, startDate, endDate);
        //    return kWh;
        //}


        public List<Chunk> getDataFromMeterByInterval(int meterId, string startDate, string endDate, int intervalId, int dataTypeId)
        {
            Meter meter = mediator.DataManager.getMeter(meterId);
            List<Chunk> dataIn = new List<Chunk>();

            switch (dataTypeId)
            {
                case 1:     ///energy data
                    dataIn = mediator.convertReadingsToChunks(meter.Register.ToList());
                    break;

                case 2:     ///cost data
                    dataIn = mediator.convertInvoicesToChunks(meter.Invoices.ToList());
                    break;
            }

            DateTime start = Convert.ToDateTime(startDate);
            DateTime end = Convert.ToDateTime(endDate);

            AppotionmentPeriod interval = (AppotionmentPeriod)intervalId;

            List<Chunk> result = mediator.apportionToPeriod(dataIn, start, end, interval);

            return result;
        }


        //public string getkWhFromMeterByInterval(int meterId, string startDate, string endDate, int intervalId)
        //{
        //    Meter meter = mediator.DataManager.getMeter(meterId);
        //    List<Chunk> dataIn = mediator.convertReadingsToChunks(meter.Register.ToList());

        //    DateTime start = Convert.ToDateTime(startDate);
        //    DateTime end = Convert.ToDateTime(endDate);

        //    AppotionmentPeriod interval = (AppotionmentPeriod)intervalId;

        //    List<Chunk> result = mediator.apportionToPeriod(dataIn, start, end, interval);

        //    return EMConverter.fromObjectToJSON(result);
        //}

        //public double getCostFromMeter(int meterId, string startDate, string endDate)
        //{
        //    Meter meter = mediator.DataManager.getMeter(meterId);
        //    List<Chunk> dataIn = mediator.convertInvoicesToChunks(meter.Invoices.ToList());

        //    double cost = mediator.apportionToDates(dataIn, startDate, endDate);
        //    return cost;
        //}

        public double getCO2FromMeter(int meterId, string startDate, string endDate)
        {
            Meter meter = mediator.DataManager.getMeter(meterId);
            double kWh = getDataFromMeter(meterId, startDate, endDate, (int)DataType.Energy);

            double CO2kg = kWh * meter.KWhtoCO2ConversionFactor;

            return CO2kg;
        }

        //public List<Chunk> getCostFromMeterByInterval(int meterId, string startDate, string endDate, int intervalId)
        //{
        //    Meter meter = mediator.DataManager.getMeter(meterId);
        //    List<Chunk> dataIn = mediator.convertInvoicesToChunks(meter.Invoices.ToList());

        //    DateTime start = Convert.ToDateTime(startDate);
        //    DateTime end = Convert.ToDateTime(endDate);

        //    AppotionmentPeriod interval = (AppotionmentPeriod)intervalId;

        //    List<Chunk> result = mediator.apportionToPeriod(dataIn, start, end, interval);
        //    return result;
        //}


        public string getLastInvoiceDate(int meterId)
        {
            Meter meter = mediator.DataManager.getMeter(meterId);
            Invoice lastInvoice = new Invoice();
            
            ///check for presence of invoices
            if (meter.Invoices == null | meter.Invoices.Count == 0)
            {
                return null;
            }
            else
            {
                meter.Invoices = meter.Invoices.OrderBy(inv => inv.StartDate).ToList();
                lastInvoice = meter.Invoices.ElementAt(0);
            }

            return lastInvoice.EndDate.ToShortDateString();
        }


    }
}
