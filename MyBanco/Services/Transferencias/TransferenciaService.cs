using Microsoft.EntityFrameworkCore.Storage;
using MyBanco.Infra.Repository.Carteiras;
using MyBanco.Infra.Repository.Transferencias;
using MyBanco.Mappers;
using MyBanco.Models;
using MyBanco.Models.Response;
using MyBanco.Models.Request;
using MyBanco.Models.DTOs;
using MyBanco.Models.Enum;
using MyBanco.Services.Autorizador;
using MyBanco.Services.Notificacao;

namespace MyBanco.Services.Transferencias;

public class TransferenciaService : ITransferenciaService
{

    private readonly ITransferenciaRepository _transacaoRepository;
    private readonly ICarteiraRepository _carteiraRepository;
    private readonly IAutorizadorService _autorizadorService;
    private readonly INotificacaoService _notificacaoService;

    public TransferenciaService(ITransferenciaRepository transferenciaRepository,
        ICarteiraRepository carteiraRepository, IAutorizadorService autorizadorService, INotificacaoService notificacaoService)
    {
        _transacaoRepository = transferenciaRepository;
        _carteiraRepository = carteiraRepository;
        _autorizadorService = autorizadorService;
        _notificacaoService = notificacaoService;
    }
    
    
    public async Task<Result<TransferenciaDto>> ExecuteAsync(TransferenciaRequest request) 
    {
        if (!await _autorizadorService.AuthorizeAsync())
            return Result<TransferenciaDto>.Failure("Nao autorizado");

        var pagador = await _carteiraRepository.GetById(request.SenderId);
        var recebedor = await _carteiraRepository.GetById(request.ReceiverId);

        if (pagador is null || recebedor is null)
            return Result<TransferenciaDto>.Failure("Nenhuma carteira encontrada");

        if (pagador.SaldoConta < request.Valor || pagador.SaldoConta == 0)
            return Result<TransferenciaDto>.Failure("Saldo insuficiente");

        if (pagador.UserType == UserType.Lojista)
            return Result<TransferenciaDto>.Failure("Lojista nao pode efetuar a transferencia");
        
        pagador.DebitarSaldo(request.Valor);
        recebedor.CreditarSaldo(request.Valor);
        
        var transferencia = new TransferenciaEntity(pagador.Id, recebedor.Id, request.Valor);

        using (var transferenciaScope = await _transacaoRepository.BeginTransactionAsync())
        {
            try
            {
                var updateTasks = new List<Task>
                {
                    _carteiraRepository.UpdateAsync(pagador),
                    _carteiraRepository.UpdateAsync(recebedor),
                    _transacaoRepository.AddTransaction(transferencia),
                };
                
                await Task.WhenAll(updateTasks);
                
                await _carteiraRepository.CommitAsync();
                await _transacaoRepository.CommitAsync();

                await transferenciaScope.CommitAsync();

            }
            catch (Exception ex)
            {
                await transferenciaScope.RollbackAsync();
                return Result<TransferenciaDto>.Failure("Erro ao realizar transferencia: " + ex.Message);
            }
        }

        await _notificacaoService.SendNotification();
        return Result<TransferenciaDto>.Success(transferencia.ToTransferenciaDto());

    }
}