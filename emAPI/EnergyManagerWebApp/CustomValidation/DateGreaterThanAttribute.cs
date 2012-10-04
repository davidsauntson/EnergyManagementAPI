
/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
 * DAVID SAUNTSON 12993011                                               *
 * MSc Software Engineering - Final Year Project                         *
 *                                                                       *
 * Energy Management API & Software                                      *
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
 * 
 * .cs - 
 * Code source: http://www.a2zdotnet.com/View.aspx?Id=182#.UBQuhsjC6Go
 */


using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace EnergyManagerWebApp.CustomValidation
{
    public sealed class DateGreaterThanAttribute : ValidationAttribute
    {
        private const string _defaultErrorMessage = "Date must be later than that";
        private string _basePropertyName;

        public DateGreaterThanAttribute(string basePropertyName)
            : base(_defaultErrorMessage)
        {
            _basePropertyName = basePropertyName;
        }

        //Override default FormatErrorMessage Method  
        public override string FormatErrorMessage(string name)
        {
            return string.Format(_defaultErrorMessage, name, _basePropertyName);
        }

        //Override IsValid  
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            //Get PropertyInfo Object  
            var basePropertyInfo = validationContext.ObjectType.GetProperty(_basePropertyName);

            //Get Value of the property  
            var startDate = (DateTime)basePropertyInfo.GetValue(validationContext.ObjectInstance, null);


            var thisDate = (DateTime)value;

            //Actual comparision  
            if (thisDate < startDate)
            {
                var message = FormatErrorMessage(validationContext.DisplayName);
                return new ValidationResult(message);
            }

            //Default return - This means there were no validation error  
            return null;
        }

    }

    public sealed class DateNotInTheFuture : ValidationAttribute
    {
        private const string _defaultErrorMessage = "'{0}' must not be in the future";
        private string _basePropertyName;

        public DateNotInTheFuture()
            : base(_defaultErrorMessage)
        {
            
        }

        //Override default FormatErrorMessage Method  
        public override string FormatErrorMessage(string name)
        {
            return string.Format(_defaultErrorMessage, name);
        }

        //Override IsValid  
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var thisDate = (DateTime)value;

            //Actual comparision  
            if (thisDate > System.DateTime.Now)
            {
                var message = FormatErrorMessage(validationContext.DisplayName);
                return new ValidationResult(message);
            }

            //Default return - This means there were no validation error  
            return null;
        }

    }

    public sealed class IntGreaterThanAttribute : ValidationAttribute
    {
        private const string _defaultErrorMessage = "Must be greater than previous limit";
        private string _basePropertyName;

        public IntGreaterThanAttribute(string basePropertyName)
            : base(_defaultErrorMessage)
        {
            _basePropertyName = basePropertyName;
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format(_defaultErrorMessage, name, _basePropertyName);
        }

        //Override IsValid  
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            //Get PropertyInfo Object  
            var basePropertyInfo = validationContext.ObjectType.GetProperty(_basePropertyName);

            //Get Value of the property  
            var baseInt = (int?)basePropertyInfo.GetValue(validationContext.ObjectInstance, null);


            var thisInt = (int?)value;

            //Actual comparision  
            if (baseInt != 0)
            {
                if (thisInt < baseInt)
                {
                    var message = FormatErrorMessage(validationContext.DisplayName);
                    return new ValidationResult(message);
                }
            }

            //Default return - This means there were no validation error  
            return null;
        }
    }


    public sealed class ZeroOrGreaterThanAttribute : ValidationAttribute
    {
        private const string _defaultErrorMessage = "2nd upper limit must be higher than 1st upper limit";
        private string _basePropertyName;

        public ZeroOrGreaterThanAttribute(string basePropertyName)
            : base(_defaultErrorMessage)
        {
            _basePropertyName = basePropertyName;
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format(_defaultErrorMessage, name, _basePropertyName);
        }

        //Override IsValid  
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            //Get PropertyInfo Object  
            var basePropertyInfo = validationContext.ObjectType.GetProperty(_basePropertyName);

            //Get Value of the property  
            var baseInt = (int?)basePropertyInfo.GetValue(validationContext.ObjectInstance, null);


            var thisInt = (int?)value;

            //Actual comparision  
            if (thisInt != 0)
            {
                if (thisInt < baseInt)
                {
                    var message = FormatErrorMessage(validationContext.DisplayName);
                    return new ValidationResult(message);
                }
            }

            //Default return - This means there were no validation error  
            return null;
        }


    }
}
