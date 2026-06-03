using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Storage;
using MyBanco.Models;

namespace MyBanco.Infra.Repository.Transferencias;

public interface ITransferenciaRepository
{
   Task AddTransaction(TransferenciaEntity transferencia);
   
   Task CommitAsync();
   
   Task<IDbContextTransaction> BeginTransactionAsync();
}