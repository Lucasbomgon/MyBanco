namespace MyBanco.Utils;

public static class CPFCNPJValidator
{
    private static string RemoveFormatting(string value)
    {
        return new string(value.Where(char.IsDigit).ToArray());
    }
    
    private static int CalculateDigit(string value, int[] multipliers)
    {
        int sum = 0;

        for (int i = 0; i < multipliers.Length; i++)
        {
            // multiplica cada digito pela soma e peso
            sum += int.Parse(value[i].ToString()) * multipliers[i];
        }

        int remainder = sum % 11;
        
        return remainder < 2 ? 0 : 11 - remainder;
    }

    private static bool IsValid(string value, int expectedLength, int[] firstMultipliers, int[] secondMultipliers)
    {
        value = RemoveFormatting(value);

        if (value.Length != expectedLength)
            return false;

        if (value.Distinct().Count() == 1)
            return false;

        int firstDigit = CalculateDigit(value, firstMultipliers);
        int secondDigit = CalculateDigit(value, secondMultipliers);
        
        return value.Substring(value.Length - 2) == $"{firstDigit}{secondDigit}";
    }

    public static bool IsCpf(string cpf)
    {
        return IsValid(cpf,
            expectedLength: 11,
            firstMultipliers: new int[] { 10, 9, 8, 7, 6, 5, 4, 3, 2 },
            secondMultipliers: new int[] {11, 10, 9, 8, 7, 6, 5, 4, 3, 2});
    }

    public static bool IsCnpj(string cnpj)
    {
        return IsValid(cnpj,
            expectedLength: 14,
            firstMultipliers: new int[]{ 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2},
            secondMultipliers: new int[]{6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 });
            
    }

    public static bool IsValidCpfCnpj(string cpfCnpj)
    {
        var clean = RemoveFormatting(cpfCnpj);

        if (clean.Length == 11) return IsCpf(clean);
        if (clean.Length == 14) return IsCnpj(clean);
        return false;
    }
}