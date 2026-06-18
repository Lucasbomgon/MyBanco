using Microsoft.EntityFrameworkCore;
using MyBanco.Models;

namespace MyBanco.Infra.Repository.Carteiras;

public class CarteiraRepository : ICarteiraRepository
{
    private readonly ApplicationDbContext _Context;
    
    public CarteiraRepository(ApplicationDbContext Context)
    {
        _Context = Context;
    }
    
    public async Task AddAsync(CarteiraEntity carteira) 
    {
        await  _Context.AddAsync(carteira);
    }

    public async Task UpdateAsync(CarteiraEntity carteira)
    {
        _Context.Update(carteira);
    }

    public async Task<CarteiraEntity?> GetByCpfCnpj(string cpfCnpj, string email)
    {
        return await _Context.Wallets.FirstOrDefaultAsync(wallet => 
            wallet.CPFCNPJ.Equals(cpfCnpj) || wallet.Email.Equals(email));
    }

    public async Task<CarteiraEntity?> GetById(int id) // 
    {
        return await _Context.Wallets.FindAsync(id);
    }

    public async Task CommitAsync()
    {
        await _Context.SaveChangesAsync();  
    }
}