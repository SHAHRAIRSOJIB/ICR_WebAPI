using System.ComponentModel.DataAnnotations;

namespace ICR_WEB_API.Service.Model.Attributes
{
    public class NumericAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null) return true;

            return double.TryParse(value.ToString(), out _);
        }
    }
}
