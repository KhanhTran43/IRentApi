using Data.Context;
using System.ComponentModel.DataAnnotations;

namespace iRentApi.Model.Validation
{
    public class UniqueEmailAttribute : ValidationAttribute
    {
        public UniqueEmailAttribute()
        {
        }

        protected override ValidationResult? IsValid(object value, ValidationContext validationContext)
        {
            var email = value as string;
            IRentContext _context = (IRentContext)validationContext
                         .GetService(typeof(IRentContext));

            if (_context.Users.FirstOrDefault(u => u.Email == email) != null) return new ValidationResult(GetErrorMessage());

            return ValidationResult.Success;
        }

        public string GetErrorMessage()
        {
            return $"Email existed";
        }
    }
}
