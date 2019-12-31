using System;
using System.ComponentModel.DataAnnotations;

namespace MPL.Common.ComponentModel.DataAnnotations
{
    /// <summary>
    /// A class that defines validation to ensure that a parameter is non-zero.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class NonZeroEnumValidationAttribute : ValidationAttribute
    {
        #region Constructors
        /// <summary>
        /// Creates a new instance of the class.
        /// </summary>
        public NonZeroEnumValidationAttribute()
            : base()
        { }

        #endregion

        #region Methods
        #region _Protected_
        /// <summary>
        /// Gets an indication of whether the specified value is valid.
        /// </summary>
        /// <param name="value">An object that is the value to validate.</param>
        /// <returns>A bool indicaating whether the value is valid.</returns>
        public override bool IsValid(object value)
        {
            bool ReturnValue = false;

            if (value != null && value.GetType().IsEnum)
                if ((int)value != 0)
                    ReturnValue = true;

            return ReturnValue;
        }
        /// <summary>
        /// Gets an indication of whether the specified value is valid.
        /// </summary>
        /// <param name="value">An object that is the value to validate.</param>
        /// <param name="validationContext">A ValidationContext containing the validation context for the operation.</param>
        /// <returns>A bool indicaating whether the value is valid.</returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            ValidationResult ReturnValue;

            if (IsValid(value))
                ReturnValue = ValidationResult.Success;
            else
                ReturnValue = new ValidationResult(FormatErrorMessage(validationContext.DisplayName), new string[] { validationContext.MemberName });

            return ReturnValue;
        }

        #endregion
        #endregion
    }
}