using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PresentationLayer
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public sealed class DateIsEarlierAttribute : ValidationAttribute
    {
        private const string DefaultErrorMessage = "{0} cannot be earlier than Start date.";

        public string OtherProperty { get; private set; }

        public DateIsEarlierAttribute(string otherProperty)
            : base(DefaultErrorMessage)
        {
            if (string.IsNullOrEmpty(otherProperty))
            {
                throw new ArgumentNullException("otherProperty");
            }

            OtherProperty = otherProperty;
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format(ErrorMessageString, name, OtherProperty);
        }

        protected override ValidationResult IsValid(object value,
                                                    ValidationContext validationContext)
        {
            if (value != null)
            {
                var otherProperty = validationContext.ObjectInstance.GetType()
                                                     .GetProperty(OtherProperty);

                var otherPropertyValue = otherProperty
                                            .GetValue(validationContext.ObjectInstance, null);

                DateTime dateOne = (DateTime)otherPropertyValue;
                DateTime dateTwo = (DateTime)value;

                int dateComparison = dateTwo.CompareTo(dateOne);
                if (dateComparison < 0)
                {
                    return new ValidationResult(
                        FormatErrorMessage(validationContext.DisplayName));
                }
            }

            return ValidationResult.Success;
        }
    }
}