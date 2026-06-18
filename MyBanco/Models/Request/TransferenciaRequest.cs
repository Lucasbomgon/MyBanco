using System.ComponentModel.DataAnnotations;

namespace MyBanco.Models.Request;

public class TransferenciaRequest
{
    [Required(ErrorMessage = "O campo valor é obrigatorio")]
    public decimal Valor { get; set; } 
    
    [Required(ErrorMessage = "O campo senderId é obrigatorio")]
    public int SenderId { get; set; }
    
    [Required(ErrorMessage = "O campo receiverId é obrigatorio")]
    public int ReceiverId { get; set; }
}