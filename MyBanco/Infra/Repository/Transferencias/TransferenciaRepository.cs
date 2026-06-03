using Microsoft.EntityFrameworkCore.Storage;
using MyBanco.Models;

namespace MyBanco.Infra.Repository.Transferencias;

public class TransferenciaRepository : ITransferenciaRepository
{
    private readonly ApplicationDbContext _Context;

    public TransferenciaRepository(ApplicationDbContext context)
    {
        _Context = context;
    }
    
    public async Task AddTransaction(TransferenciaEntity transferencia)
    {
        await _Context.Transfers.AddAsync(transferencia);
    }

    public async Task CommitAsync()
    {
        await  _Context.SaveChangesAsync();
    }

    public async Task<IDbContextTransaction> BeginTransactionAsync()
    {
        return await _Context.Database.BeginTransactionAsync();
    }
}