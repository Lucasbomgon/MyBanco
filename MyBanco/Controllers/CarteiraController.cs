using Microsoft.AspNetCore.Mvc;
using MyBanco.Infra.Repository.Carteiras;
using MyBanco.Models.Request;
using MyBanco.Services.Carteiras;

namespace MyBanco.Controllers;


[ApiController]
[Route("[controller]")]
public class CarteiraController : ControllerBase
{
    private readonly ICarteiraService _CarteiraService;
    
    public CarteiraController(ICarteiraService CarteiraService)
        {
        _CarteiraService = CarteiraService;
        }

    [HttpPost]
    public async Task<IActionResult> PostCarteira(CarteiraRequest request)
    {
        var result = await _CarteiraService.ExecuteAsync(request);
        
        if(!result.IsSuccess)
            return BadRequest(result);

        return Created();
    }
}