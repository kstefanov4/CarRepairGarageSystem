namespace CarRepairGarage.Data.Common.Helpers
{
    using System;
    using System.ComponentModel.DataAnnotations;

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class TruncateDecimalAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return true; // Null values are considered valid
            }

            if (value is not decimal decimalValue)
            {
                return false; // Non-decimal values are not valid
            }

            // Truncate the value to four decimal places
            decimal truncatedValue = Math.Truncate(decimalValue * 10000) / 10000;

            // Assign the truncated value back to the property
            value = truncatedValue;

            // All values are considered valid
            return true;
        }
    }

}
