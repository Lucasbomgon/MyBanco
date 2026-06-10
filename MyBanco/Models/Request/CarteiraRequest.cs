using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using MyBanco.Models.Enum;
using MyBanco.Utils;

namespace MyBanco.Models.Request;

public class CarteiraRequest
{
    
    [Required(ErrorMessage = "O nomeCompleto é obrigatorio")]
    public string NomeCompleto { get; set; }
    
    [Required(ErrorMessage = "O CPF ou CNPJ é obrigatorio.")]
    [CpfCnpjValidation(ErrorMessage = "O CPF ou CNPJ informado é invalida.")]
    public string CPFCNPJ { get; set; }
    
    [Required(ErrorMessage = "O email é  obrigatorio.")]
    [EmailAddress(ErrorMessage = "O email deve ser valido.")]
    public string Email { get; set; }
    
    [Required(ErrorMessage = "A senha é obrigatorio.")]
    public string Senha { get; set; }
    
    [Required(ErrorMessage = "O tipo do usuario e obrigatorio.")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public UserType UserType { get; set; }

    [Required(ErrorMessage = "O Saldo em conta e obrigatorio.")]
    public decimal SaldoConta { get; set; }
}