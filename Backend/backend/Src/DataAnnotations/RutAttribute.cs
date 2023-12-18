using System.ComponentModel.DataAnnotations;
using backend.Src.Util;

namespace backend.Src.DataAnnotations
{
    public class RutAttribute : ValidationAttribute
    {
        public RutAttribute()
        {
            ErrorMessage = "The rut is not valid";
        }

        public RutAttribute(Func<string> errorMessageAccessor) : base(errorMessageAccessor)
        {
        }

        public RutAttribute(string errorMessage) : base(errorMessage)
        {
        }

        public override bool IsValid(object? value)
        {
            var rut = value?.ToString() ?? string.Empty;
            if (rut.Length < 10 || rut.Length > 12) return false;
            return RutValidator.CheckRut(rut);
        }
    }
}