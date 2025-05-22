using System.ComponentModel.DataAnnotations;

public class EnsureValidGuidsAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value is ICollection<Guid> guids)
        {
            foreach (var guid in guids)
            {
                if (guid == Guid.Empty)
                    return new ValidationResult(ErrorMessage);
            }
        }
        return ValidationResult.Success;
    }
}