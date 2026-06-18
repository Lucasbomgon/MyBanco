namespace MyBanco.Services.Autorizador;

public interface IAutorizadorService
{
    Task<bool> AuthorizeAsync();
}