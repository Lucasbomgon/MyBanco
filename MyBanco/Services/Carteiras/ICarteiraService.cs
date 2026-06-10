using MyBanco.Models.Request;
using MyBanco.Models.Response;

namespace MyBanco.Services.Carteiras;

public interface ICarteiraService
{
    Task<Result<bool>> ExecuteAsync(CarteiraRequest request);
}