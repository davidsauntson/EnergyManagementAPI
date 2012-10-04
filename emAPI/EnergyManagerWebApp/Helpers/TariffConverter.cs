
/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
 * DAVID SAUNTSON 12993011                                               *
 * MSc Software Engineering - Final Year Project                         *
 *                                                                       *
 * Energy Management API & Software                                      *
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
 * 
 * TariffConverter.cs - Converts from/to Tariff & TariffViewModel
 * Code source: 
 */
		

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using emAPI.ClassLibrary;
using EnergyManagerWebApp.Models;

namespace EnergyManagerWebApp.Helpers
{
    public class TariffConverter
    {
        public static Tariff createTariffFromViewModel(TariffViewModel tariffViewModel)
        {
            Tariff tariff = new Tariff();

            if (tariffViewModel.Id != 0)
            {
                tariff.Id = tariffViewModel.Id;
            }

            tariff.SCPeriod = new Period { Id = tariffViewModel.SCPeriodId };
            tariff.BandPeriod = new Period { Id = tariffViewModel.BandPeriodId };
            tariff.SCValue = tariffViewModel.SCValue;
            tariff.StartDate = tariffViewModel.StartDate;
            tariff.Bands = new List<TariffBand>();

            TariffBand newBand = new TariffBand();
            newBand.LowerkWhLimit = (int)tariffViewModel.LowerLimit1;
            newBand.UnitRate = (int)tariffViewModel.Rate1;
            newBand.Id = tariffViewModel.Band1Id;

            ///isBanded is determined during conversion to tariffViewModel when the tariff object is retreived in the HTTP GET Edit method
            ///of the TariffController.  It is preserved to HTTP POST Edit via hidden fields in the Edit.cshtml view.
            
            ///isbanded should be determined at each conversion since the user may want to edit an existing tariff to include additional / fewer bands
            ///

            tariffViewModel.IsBanded = !((tariffViewModel.LowerLimit1 == 0 | tariffViewModel.LowerLimit1 == null) &&                
                                        (tariffViewModel.UpperLimit1 == 0 | tariffViewModel.UpperLimit1 == null));

            if (!tariffViewModel.IsBanded)
            {
                ///no upper limit and therefore no subsequent bands
                
                newBand.UpperkWhLimit = 0;
                tariff.Bands.Add(newBand);
            }
            else
            {
                ///get upperLimit for first band & save to tariff model object
                newBand.UpperkWhLimit = (int)tariffViewModel.UpperLimit1;
                tariff.Bands.Add(newBand);
      
                ///follow same pattern for second band 
                TariffBand newBand2 = new TariffBand();
                newBand2.LowerkWhLimit = (int)tariffViewModel.LowerLimit2;
                newBand2.UnitRate = tariffViewModel.Rate2;
                newBand2.Id = tariffViewModel.Band2Id;

                if ((tariffViewModel.UpperLimit2 == null | tariffViewModel.UpperLimit2 == 0) && (tariffViewModel.LowerLimit2 == 0 | tariffViewModel.LowerLimit2 == null))
                {
                    ///there is no band if lower is null/zero AND upper is null/zero
                    newBand2.UpperkWhLimit = -1; ///no band
                }
                else
                {
                    ///there is a band...
                    if (tariffViewModel.UpperLimit2 != 0)
                    {
                        ///with an upper limit
                        newBand2.UpperkWhLimit = (int)tariffViewModel.UpperLimit2;
                    }
                    else
                    {
                        ///without an upper limit
                        newBand2.UpperkWhLimit = 0;
                    }
                }

                tariff.Bands.Add(newBand2);


                TariffBand newBand3 = new TariffBand();
                newBand3.LowerkWhLimit = (int)tariffViewModel.LowerLimit3;
                newBand3.UnitRate = tariffViewModel.Rate3;
                newBand3.Id = tariffViewModel.Band3Id;


                if ((tariffViewModel.UpperLimit3 == null | tariffViewModel.UpperLimit3 == 0) && (tariffViewModel.LowerLimit3 == 0 | tariffViewModel.LowerLimit3 == null))
                {
                    ///there is no band if lower is null/zero and upper is null/zero
                    newBand3.UpperkWhLimit = -1; ///no band
                }
                else
                {
                    ///there is a band...
                    if (tariffViewModel.UpperLimit3 != 0)
                    {
                        ///with an upper limit
                        newBand3.UpperkWhLimit = (int)tariffViewModel.UpperLimit3;
                    }
                    else
                    {
                        ///without an upper limit
                        newBand3.UpperkWhLimit = 0;
                    }
                }

                tariff.Bands.Add(newBand3);
            }

            return tariff;
        }

        public static TariffViewModel createTariffViewFromTariff(Tariff tariffModel, bool forEditing)
        {
            TariffViewModel tariffView = new TariffViewModel();

            tariffView.Id = tariffModel.Id;
            tariffView.StartDate = tariffModel.StartDate;
            tariffView.SCPeriodId = tariffModel.SCPeriod.Id;
            tariffView.SCValue = tariffModel.SCValue;
            tariffView.BandPeriodId = tariffModel.BandPeriod.Id;

            if (tariffModel.Bands.Count == 0)
            {
                ///there are no bands so we can just return now
                return tariffView;
            }

            else if (tariffModel.Bands.ElementAt(0) != null)
            {
                tariffView.IsBanded = (tariffModel.Bands.ElementAt(0).UpperkWhLimit > 0);
            }
            else
            {
                tariffView.IsBanded = false;
            }
             
            for (int i = 0; i < tariffModel.Bands.Count; i++)
            {
                TariffBand band = tariffModel.Bands.ElementAt(i);
                if (i == 0)
                {
                   
                    tariffView.LowerLimit1 = band.LowerkWhLimit;
                    tariffView.UpperLimit1 = band.UpperkWhLimit;
                    tariffView.Rate1 = band.UnitRate;
                    tariffView.Band1Id = band.Id;
                }

                if (i == 1)
                {
                    tariffView.LowerLimit2 = band.LowerkWhLimit;
                    tariffView.UpperLimit2 = band.UpperkWhLimit;
                    tariffView.Rate2 = band.UnitRate;
                    tariffView.Band2Id = band.Id;
                }

                if (i == 2)
                {
                    tariffView.LowerLimit3 = band.LowerkWhLimit;
                    tariffView.UpperLimit3 = band.UpperkWhLimit;
                    tariffView.Rate3 = band.UnitRate;
                    tariffView.Band3Id = band.Id;
                }
            }

            if (forEditing)
            {
                if (tariffView.UpperLimit2 == -1)
                    tariffView.UpperLimit2 = 0;

                if (tariffView.UpperLimit3 == -1)
                    tariffView.UpperLimit3 = 0;
            }

            return tariffView;
        }
    }
}
