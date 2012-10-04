/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
 * DAVID SAUNTSON 12993011                                               *
 * MSc Software Engineering - Final Year Project                         *
 *                                                                       *
 * Energy Management API & Software                                      *
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
 * 
 * ApportionmentManager.cs
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
    /// Controller object responsible for apportioning data into regularly sized Chunk objects.
    /// </summary>
    public class ApportionmentManager : IApportionmentManager
    {
        private EMMediator mediator = new EMMediator();


        /// <summary>
        /// Calculates total amount between start and end date based on provided data
        /// </summary>
        /// <param name="dataIn">List[Chunk] of base data</param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns>Double representing total amount between start and end date, apportioned between start and end dates</returns>
        public double apportionToDates(List<Chunk>dataIn, string startDate, string endDate)
        {
            DateTime start = Convert.ToDateTime(startDate);
            DateTime end = Convert.ToDateTime(endDate);

            if(!canApportion(dataIn, start, end))
            {
                return 0;
            }
            else
            {
                List<Chunk> datedChunks = setupDatedChunksForApportionToDates(start, end);
                List<Chunk> result = apportion(datedChunks, dataIn);

                if (result == null | result.Count == 0)
                {
                    return 0;
                }
                else
                {
                    return result.ElementAt(0).Amount;
                }
            }

        }


        /// <summary>
        /// Converts meter readings / invoice costs into consumption between regular intervals.
        /// </summary>
        /// <param name="dataIn">List of date/double pairs that represent raw meter readings or invoices</param>
        /// <param name="startDate">start date of required apportioned consumption</param>
        /// <param name="endDate">end date of required apportioned consumption</param>
        /// <param name="interval">interval between required apportioned date/double pairs</param>
        /// <returns>Returns null if apportionment cannot be undertaken (not enough readings/invoices / no readings/invoices in specified time period)</returns>
        public List<Chunk> apportionToPeriod(List<Chunk> dataIn, DateTime startDate, DateTime endDate, AppotionmentPeriod interval)
        {
            if (!canApportion(dataIn, startDate, endDate))
            {
                return null;
            }

            else
            {   
                ///create list of dated chunks with no consumptions, used to create the final results list
                List<Chunk> datedChunks = setupDatedChunksForApportionToPeriod(startDate, endDate, interval);

                ///do the actual apportioning of data
                return apportion(datedChunks, dataIn);
            }
        }


        //SETUP METHODS


        /// <summary>
        /// Creates List[Chunk] with required start and end date.  List[Chunk].Count = 1.
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns>List[Chunk] where .Count = 1</returns>
        private List<Chunk> setupDatedChunksForApportionToDates(DateTime start, DateTime end)
        {
            List<Chunk> datedChunks = new List<Chunk>
            {
                new Chunk { StartDate = start, EndDate = end }
            };

            return datedChunks;
        }


        /// <summary>
        /// Creates List[Chunk], with regular start and end dates for required intervals.  Amount = 0 for all Chunks.
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="interval"></param>
        /// <returns>List[Chunk] for required duration, each Chunk covering one interval.</returns>
        public List<Chunk> setupDatedChunksForApportionToPeriod(DateTime startDate, DateTime endDate, AppotionmentPeriod interval)
        {
            ///calendar object used for adding days/months/years to start date
            System.Globalization.Calendar calendar = System.Globalization.CultureInfo.CurrentCulture.Calendar;

            ///set up apportionment requirements
            List<Chunk> datedChunks = new List<Chunk>();
            int periodType = (int)interval;

            ///i used to keep track of how many Chunks have already been created & adds appropriate no. of intervals to start date.
            int i = 0;

            ///create list of chunks with correct required start dates
            do
            {
                Chunk chunk = new Chunk();
                TimeSpan oneDay = new TimeSpan(1, 0, 0); ///one day timespan needed to calculate end dates

                switch (periodType)
                {

                    case 1: ///interval is daily
                        ///

                        chunk.StartDate = calendar.AddDays(startDate, i);
                        chunk.EndDate = calendar.AddDays(chunk.StartDate, 1);
                        break;

                    case 2: ///interval is weekly
                        ///
                        chunk.StartDate = calendar.AddWeeks(startDate, i);
                        chunk.EndDate = calendar.AddWeeks(chunk.StartDate, 1);
                        break;

                    case 3: ///interval is monthly
                        ///
                        chunk.StartDate = calendar.AddMonths(startDate, i);
                        chunk.EndDate = calendar.AddMonths(chunk.StartDate, 1);
                        break;

                    case 4: ///interval is quarterly
                        ///
                        chunk.StartDate = calendar.AddMonths(startDate, 3 * i);
                        chunk.EndDate = calendar.AddMonths(chunk.StartDate, 3 * 1);

                        break;

                    case 5: ///interval is annually
                        ///
                        chunk.StartDate = calendar.AddYears(startDate, i);
                        chunk.EndDate = calendar.AddYears(chunk.StartDate, 1);
                        break;

                }

                ///take on day off each date date to avoid overlapping date ranges
                chunk.EndDate = chunk.EndDate.Subtract(oneDay);
                datedChunks.Add(chunk);
                i++;

            } while (datedChunks.ElementAt(i - 1).EndDate <= endDate);

            return datedChunks;

        }

        //APPORTIONMENT METHODS

        /// <summary>
        /// Takes a list of Chunks with irregular (but consecutive) start and end dates and creates another List[Chunk] with regular start
        /// and end dates, the consumption of each caclulated from available data.  
        /// Backwards apportionment (where the requested interval is smaller than the interval between readings/invoices) will result in the
        /// average over the whole period being applied to that chunk.
        /// Interval, start and end dates for the whole period are determined from the list of Chunks provided.
        /// </summary>
        /// <param name="datedChunks">
        /// List[Chunk] containing Chunks with regular start/end date for the duration of the period required.
        /// Create using setupDatedChunksForApportionToPeriod(DateTime startDate, DateTime endDate, AppotionmentPeriod interval)
        /// </param>
        /// <param name="dataIn">
        /// List[Chunk] containing source data.
        /// Create using convertReadingsToChunks(List[MeterReading] readings) OR convertInvoicesToChunks(List[Invoices] invoices)
        /// </param>
        /// <returns>List[Chunk] of apportioned data.</returns>
        public List<Chunk> apportion(List<Chunk> datedChunks, List<Chunk> dataIn)
        {
            ///create list to store resulting chunks
            List<Chunk> resultChunks = new List<Chunk>();
            
            ///ensure provided list is in ascending date order
            dataIn = dataIn.OrderBy(chunk => chunk.StartDate).ToList();

            ///determine max start and end dates from dataIn
            DateTime maxStart = dataIn.First().StartDate;
            DateTime maxEnd = dataIn.Last().EndDate;

            ///do the apportioning
            foreach (Chunk datedChunk in datedChunks)
            {
                ///list for storing the chunks from dataIn that are relevant to this results chunk
                List<Chunk> relevantChunks = new List<Chunk>();

                double averageDailyRateOverPeriod = 0;
                double totalAmountOverPeriod = 0;
                double periodLength = 0;

                ///get all relevant chunks in the dataIn list that lie within the date boundaries of this chunk of the results list
                foreach (Chunk chunk in dataIn)
                {

                    ///track average daily amount to fill in gaps in 'backwards' apportionment (ie where interval < time between reads)
                    TimeSpan tsInChunk = chunk.EndDate - chunk.StartDate;
                    periodLength += tsInChunk.Days;
                    totalAmountOverPeriod += chunk.Amount;
                    averageDailyRateOverPeriod = totalAmountOverPeriod / periodLength;

                    ///check whether this chunk falls in the chunk on datedChunks & is therefore relevant to calculating
                    ///the total for this datedChunk
                    if ((chunk.StartDate >= datedChunk.StartDate && chunk.StartDate <= datedChunk.EndDate) |
                          (chunk.EndDate >= datedChunk.StartDate && chunk.EndDate <= datedChunk.EndDate))
                    {
                        relevantChunks.Add(chunk);
                    }
                }

                double knownAmount = 0;
                double estimatedAmount = 0;

                ///calculate the known amount for this datedChunk using all the relevant chunks gathered in relevantChunks above
                foreach (Chunk relevantChunk in relevantChunks)
                {
                    knownAmount += relevantChunk.Amount;
                }


                ///determine number of days required by this results chunk
                TimeSpan ts = datedChunk.EndDate - datedChunk.StartDate;
                double numbDaysRequired = ts.TotalDays;


                double dailyRate = 0;

                ///determine number of days covered by the chunks in this result chunk
                double numbDaysCovered = new double();
                if (relevantChunks.Count != 0)
                {
                    ts = relevantChunks.ElementAt(relevantChunks.Count - 1).EndDate - relevantChunks.ElementAt(0).StartDate;
                    numbDaysCovered = ts.TotalDays;
                }
                else
                {
                    ///in this case we have to use the average of the whole period calculated above
                    numbDaysCovered = 0;
                    dailyRate = averageDailyRateOverPeriod;
                }


                ///determine number of missing days that require estimation
                double missingDays = numbDaysRequired - numbDaysCovered;

                ///determine average daily amount from known amount
                if (numbDaysCovered != 0)
                {
                    dailyRate = knownAmount / numbDaysCovered;
                }

                ///calculate estimated amount
                estimatedAmount = missingDays * dailyRate;

                ///create new, final result chunk and add to list
                Chunk resultChunk = new Chunk
                {
                    StartDate = datedChunk.StartDate,
                    EndDate = datedChunk.EndDate,
                    Amount = knownAmount + estimatedAmount
                    ///NB if the length of the chunk is greater than required, missingDays will be negative and the total amount
                    ///adjusted downward as necessary
                };

                ///check if chunk lies inside the maxStart and maxEnd range (ie not outide all data available)
                if ((resultChunk.StartDate >= maxStart && resultChunk.StartDate <= maxEnd) |
                      (resultChunk.EndDate >= maxStart && resultChunk.EndDate <= maxEnd))
                {
                    resultChunks.Add(resultChunk);
                }
            }

            return resultChunks;
        }

        
        //CHECKING METHODS
        
        private enum DateCoverage
        {
            bothDatesInRange = 1,
            startDateInRange = 2,
            endDateInRange = 3,
            neitherDateInRange = 4,
            invalidRange = 5,
            invalidDates = 6
        }


        /// <summary>
        /// Check the date coverage of two provided start and end dates compared to a range's start and end date
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="rangeStart"></param>
        /// <param name="rangeEnd"></param>
        /// <returns>DateCoverage enum representing the date coverage</returns>
        private DateCoverage getDateCoverage(DateTime startDate, DateTime endDate, DateTime rangeStart, DateTime rangeEnd)
        {
            if (startDate > endDate)
            {
                return DateCoverage.invalidDates;
            }

            if (rangeStart > rangeEnd)
            {
                return DateCoverage.invalidRange;
            }

            if ((startDate >= rangeStart) && (endDate <= rangeEnd))
            {
                return DateCoverage.bothDatesInRange;
            }

            if ((endDate >= rangeStart) | (endDate <= rangeEnd))
            {
                return DateCoverage.endDateInRange;
            }

            if ((startDate >= rangeStart) | (startDate <= rangeEnd))
            {
                return DateCoverage.startDateInRange;
            }

            return DateCoverage.neitherDateInRange;
        }


        /// <summary>
        /// Determines whether the date coverage of a range of dates is suitable to allow apportionment from that range to between two provided dates
        /// </summary>
        /// <param name="coverage"></param>
        /// <returns>true if date coverage is suitable</returns>
        private bool checkDateCoverage(DateCoverage coverage)
        {
            return (int)coverage < (int)DateCoverage.neitherDateInRange;
        }

        /// <summary>
        /// Determines whether or not apportionment between provided dates can be accomplished using provided List[Chunk]
        /// </summary>
        /// <param name="dataIn"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns>true if apportionement can be accomplished</returns>
        private bool canApportion(List<Chunk> dataIn, DateTime startDate, DateTime endDate)
        {
            ///check if there are appropriate number of chunks to complete apportionment
            ///
            if (dataIn.Count < 2)
            {
                ///need at least two chunks to do any apportionment
                ///
                return false;
            }

            ///get dateCoverate enum that tells us where the start and end dates of the requested data range fall in relation to the data in the dataIn list
            DateCoverage dateCoverage = getDateCoverage(startDate, endDate, dataIn.ElementAt(0).StartDate, dataIn.ElementAt(dataIn.Count - 1).EndDate);

            return checkDateCoverage(dateCoverage);
        }

        //CONVERSION METHODS

        //CONVERTING READINGS & INVOICES INTO CHUNKS

      
        /// <summary>
        /// Converts a list of meter readings into a list of chunks suitable for apportionment
        /// </summary>
        /// <param name="readings">meter readings to convert</param>
        /// <returns>List[Chunk] from meter readings</returns>
        public List<Chunk> convertReadingsToChunks(List<MeterReading> readings)
        {
            List<Chunk> result = new List<Chunk>();
            TimeSpan oneDay = new TimeSpan(1, 0, 0, 0);

            readings = readings.OrderBy(rdg => rdg.Date).ToList();

            foreach (MeterReading rdg in readings)
            {
                Chunk chunk = new Chunk();
                try
                {
                    ///start date calculated from date of reading + one day
                    chunk.StartDate = readings.First(r => r.Date == rdg.Date).Date + oneDay;

                    ///end date calulated from date of next reading
                    chunk.EndDate = readings.First(r => r.Date > rdg.Date).Date;

                    ///consumption calculated from consumption of next reading
                    chunk.Amount = readings.First(r => r.Date > rdg.Date).Consumption;
                }
                catch
                {
                    ///no chunk added if there is an error (ie are outside boundaries of list)
                    break;
                }

                result.Add(chunk);
            }

            return result;
        }


        /// <summary>
        /// Converts a list of invoices into a list of Chunks suitable for apportionment
        /// </summary>
        /// <param name="invoices"></param>
        /// <returns></returns>
        public List<Chunk> convertInvoicesToChunks(List<Invoice> invoices)
        {
            List<Chunk> result = new List<Chunk>();
            TimeSpan oneDay = new TimeSpan(1, 0, 0, 0);

            invoices = invoices.OrderBy(inv => inv.StartDate).ToList();

            foreach (Invoice inv in invoices)
            {
                Chunk chunk = new Chunk();

                chunk.StartDate = inv.StartDate;
                chunk.EndDate = inv.EndDate;
                chunk.Amount = inv.ConsumptionCharge + inv.StandingCharge + inv.OtherCharge;

                result.Add(chunk);
            }

            return result;
        }


        /// <summary>
        /// Determines the best type of apportionment interval for two dates.  This is acheived by calculating the modulus of the
        /// number of periods between two dates for each period type in the database.  The answer with the lowest modulus
        /// (ie the lowest number of remainder days) is the best match.
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns>Apportionment period that best matches the two dates.</returns>
        public AppotionmentPeriod getBestApportionmentPeriod(DateTime startDate, DateTime endDate)
        {
            TimeSpan timespan = endDate - startDate;
            int lengthInDays = (int)timespan.TotalDays;

            ///Create a list of dictionary objects that hold a string for the duration and an int for the modulo of that duration in days
            Dictionary<string, int> remainders = new Dictionary<string, int>();

            ///populate the dictionary with each of the Period types and their length in days
            List<Period> periods = mediator.DataManager.getPeriods();
            foreach (Period p in periods)
            {
                remainders.Add(p.Length, (lengthInDays % p.NumbDays));
            }

            ///order the dictionary so the lowest remainder is at the top
            remainders.OrderBy(d => d.Value);

            ///extract the entry at the top of the dictionary - this is the best match
            string matchingPeriod = remainders.ElementAt(0).Key;

            ///convert string to enum and return
            return (AppotionmentPeriod)Enum.Parse(typeof(AppotionmentPeriod), matchingPeriod);
        }
    }
}
