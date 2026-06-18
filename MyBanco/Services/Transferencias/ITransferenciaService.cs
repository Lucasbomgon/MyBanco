using MyBanco.Models.DTOs;
using MyBanco.Models.Request;
using MyBanco.Models.Response;


namespace MyBanco.Services.Transferencias;

public interface ITransferenciaService
{
    Task<Result<TransferenciaDto>> ExecuteAsync(TransferenciaRequest request);
}