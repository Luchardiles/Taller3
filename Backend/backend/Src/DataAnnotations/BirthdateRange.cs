using System.ComponentModel.DataAnnotations;

namespace backend.Src.DataAnnotations
{
    public class BirthdateRange : ValidationAttribute
    {
        public BirthdateRange()
        {
            ErrorMessage = "The age birthday is not valid";
        }

        public BirthdateRange(string errorMessage) : base(errorMessage)
        {
        }

        public BirthdateRange(Func<string> errorMessageAccessor) : base(errorMessageAccessor)
        {
        }

        public override bool IsValid(object? value)
        {
            var birthday = (int?)value ?? 0;
            if (birthday < 1900 || birthday > DateTime.Now.Year) return false;
            return true;
        }
    }
}