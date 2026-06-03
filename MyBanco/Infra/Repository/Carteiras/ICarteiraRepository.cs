using MyBanco.Models;

namespace MyBanco.Infra.Repository.Carteiras;

public interface ICarteiraRepository
{
    Task AddAsync(CarteiraEntity carteira);
    
    Task UpdateAsync(CarteiraEntity carteira); 
    
    Task<CarteiraEntity?> GetByCpfCnpj(string cpfCnjp, string email);

    Task<CarteiraEntity?> GetById(int id);

    Task CommitAsync();

}