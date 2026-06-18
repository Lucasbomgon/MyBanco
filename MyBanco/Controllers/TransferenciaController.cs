using Microsoft.AspNetCore.Mvc;
using MyBanco.Models.DTOs;
using MyBanco.Models.Request;
using MyBanco.Services.Notificacao;
using MyBanco.Services.Transferencias;

namespace MyBanco.Controllers;


[ApiController]
[Route("transfer")]
public class TransferenciaController : ControllerBase
{
    private readonly ITransferenciaService _transferenciaService;

    public TransferenciaController(ITransferenciaService transferenciaService)
    {
        _transferenciaService = transferenciaService;   
    }

    [HttpPost]
    public async Task<IActionResult> PostUser(TransferenciaRequest request)
    {
        var result = await _transferenciaService.ExecuteAsync(request);
        if (!result.IsSuccess)
            return BadRequest(result);
        
        return Ok(result);
    } 
    
}