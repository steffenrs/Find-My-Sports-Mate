using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace PresentationLayer
    {
        [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
        public sealed class IntegerIsLessThanAttribute : ValidationAttribute
        {
            private const string DefaultErrorMessage = "{0} cannot be less than {1}.";

            public string OtherProperty { get; private set; }

            public IntegerIsLessThanAttribute(string otherProperty)
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
                //return string.Format(ErrorMessageString, name, OtherProperty);
                return "Maximum people joined cannot be less than minium people joined";
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

                    int intOne = (int)value;
                    int intTwo = (int)otherPropertyValue;
                    
                    if (intOne < intTwo)
                    {
                        return new ValidationResult(
                            FormatErrorMessage(validationContext.DisplayName));
                    }
                }

                return ValidationResult.Success;
            }
        }
}