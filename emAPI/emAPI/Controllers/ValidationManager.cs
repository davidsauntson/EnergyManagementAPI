
/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
 * DAVID SAUNTSON 12993011                                               *
 * MSc Software Engineering - Final Year Project                         *
 *                                                                       *
 * Energy Management API & Software                                      *
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
 * 
 * ValidationManager.cs
 * Code source: Handwritten
 */
		

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using emAPI.ClassLibrary;
using emAPI.Controllers.Validators;
using emAPI.Interfaces;

namespace emAPI.Controllers
{
    /// <summary>
    /// Controller object responsible for validating invoices.
    /// </summary>
    internal class ValidationManager : IValidationManager
    {
        internal EMMediator mediator = new EMMediator();
        private Validator validator;

        /// <summary>
        /// Validates invoice from invoice Id only.  Ensures Invoice.Meter object is complete with all required tariff information.
        /// Returns JSON string.
        /// </summary>
        /// <param name="invoiceId">id of invoice to validate</param>
        /// <param name="saveAfterValidation">whether the changes to the invoice should be saved after validation</param>
        /// <returns>JSON representation of the validated Invoice, wrapped in EMRepsonse.</returns>
        public string validateInvoice(int invoiceId, bool saveAfterValidation)
        {
            Invoice invoice = mediator.DataManager.getInvoice(invoiceId);
            invoice = validateInvoice(invoice, saveAfterValidation);
            return EMConverter.fromObjectToJSON(invoice);
        }

        /// <summary>
        /// Validates an invoice by updated the validation booleans and cost & consumption variances.
        /// </summary>
        /// <param name="invoice">Inovice object to validate</param>
        /// <param name="saveAfterValidation">whether the changes to the invoice should be saved after validation</param>
        /// <returns>The validated invoice object, regardless of whether it is saved to the database.</returns>
        public Invoice validateInvoice(Invoice invoice, bool saveAfterValidation)
        {
            ///check whether the invoice can be validated and update validation bools
            invoice.CostCanBeValidated = Validator.canValidateCost(invoice);
            invoice.ConsumptionCanBeValidated = Validator.canValidateConsumption(invoice);

            ///validate the consumption element of the invoice (same regardless of tariff type)
            invoice = Validator.validateConsumption(invoice);

            if (invoice.CostCanBeValidated.Value)
            {
                ///tariff is banded if the first upper limit is > 0
                bool tariffIsBanded = (invoice.Meter.Tariffs.ElementAt(0).Bands.ElementAt(0).UpperkWhLimit > 0);

                if (tariffIsBanded)
                {
                    ///use consumption banded validator
                    validator  = new ConsumptionBandedValidator();
                }
                else
                {
                    ///use regular validator
                    validator = new UnbandedValidator();
                    invoice = validator.validateCost(invoice);
                }

                invoice = validator.validateCost(invoice);

                ///update checked bool
                invoice.Checked = true;

            }

            ///save if required
            if (saveAfterValidation)
                mediator.DataManager.editInvoice(invoice.Id, invoice);

            return invoice;
        }

        
        //CALLS TO MEDIATOR
        public double getDataFromMeter(int meterId, string startDate, string endDate, int dataTypeId)
        {
            return mediator.getDataFromMeter(meterId, startDate, endDate, dataTypeId);
        }
    }
}
