namespace MyBanco.Models;

public class TransferenciaEntity    
{
    public Guid IdTransferencia { get; set; }
    
    public int  SenderId { get; set; }
    public CarteiraEntity Sender { get; set; }
    
    public int ReceiverId { get; set; }
    public CarteiraEntity Receiver { get; set; }
    
    public decimal Valor  { get; set; } 
    
    public TransferenciaEntity(int  senderId, int receiverId, decimal valor)
    {   
        IdTransferencia = Guid.NewGuid();
        SenderId = senderId;
        ReceiverId = receiverId;
        Valor = valor;  
    }
    
}