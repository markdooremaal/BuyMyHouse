using BuyMyHouse.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace BuyMyHouse.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MortgageController : ControllerBase
{
    private readonly IMortgageService _mortgageService;

    public MortgageController(IMortgageService mortgageService)
    {
        _mortgageService = mortgageService;
    }

    [HttpGet("GetMortgageById")]
    public async Task<IActionResult> GetMortGageById(int id)
    {
        var mortgage = await _mortgageService.GetById(id);

        if ((mortgage.DateTime.AddHours(24) - DateTime.Now).TotalHours > 24)
        {
            return NotFound("Mortgage offer expired");
        }
        
        return Ok(mortgage);
    }
}