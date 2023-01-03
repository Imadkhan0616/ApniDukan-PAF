using System.ComponentModel.DataAnnotations;

namespace ApniDukan.Validation.Attribute
{
    public class DateRequired : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTime date = (DateTime)value;

            if (date == DateTime.MinValue || date <= new DateTime(1923, 1, 1))
                return false;

            return true;
        }
    }
}
