using MyBanco.Models;
using MyBanco.Models.DTOs;

namespace MyBanco.Mappers;

public static class TransferenciaMapper
{
    public static TransferenciaDto ToTransferenciaDto(this TransferenciaEntity transaction)
    {
        return new TransferenciaDto(
            transaction.IdTransferencia,
            transaction.Sender,
            transaction.Receiver,
            transaction.Valor
        );
    }
}