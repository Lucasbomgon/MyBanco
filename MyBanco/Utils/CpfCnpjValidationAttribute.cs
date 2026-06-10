using System.ComponentModel.DataAnnotations;

namespace MyBanco.Utils;

public class CpfCnpjValidationAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(Object value, ValidationContext validationContext)
    {
        var cpfCnpj = value as string;

        if (string.IsNullOrEmpty(cpfCnpj) || !CPFCNPJValidator.IsValidCpfCnpj(cpfCnpj))
        {
            return new ValidationResult(ErrorMessage);
        }
        
        return  ValidationResult.Success;
    }
}