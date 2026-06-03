using MyBanco.Models.Enum;  

namespace MyBanco.Models;


public class CarteiraEntity
{
    public int Id { get; set; }
    
    public string NomeCompleto { get; set; }
    
    public string CPFCNPJ { get; set; }
    
    public string Email { get; set; }
    
    public string Senha { get; set; }
    
    public decimal SaldoConta { get; set; }
    
    public UserType UserType { get; set; }

    private CarteiraEntity(){}

    public CarteiraEntity(String nomeCompleto, String cPFCBNPJ, String email, String senha, UserType userType, decimal saldoConta)
    {
        NomeCompleto = nomeCompleto;
        CPFCNPJ = cPFCBNPJ;
        Email = email;
        Senha = senha;
        UserType = userType;
        SaldoConta = saldoConta;
    }

    public void DebitarSaldo(decimal valor)
    {
        SaldoConta -= valor;
    }

    public void CreditarSaldo(decimal valor)
    {
        SaldoConta += valor;
    }
    
}