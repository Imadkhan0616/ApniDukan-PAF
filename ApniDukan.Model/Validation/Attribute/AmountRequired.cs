using System.ComponentModel.DataAnnotations;

namespace ApniDukan.Validation.Attribute
{
    public class AmountRequired : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            decimal amount = (decimal)value;

            if (amount is 0 or < 0)
                return false; 

            return true;
        }
    }
}
