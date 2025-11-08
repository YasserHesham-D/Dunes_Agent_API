using Application.Dtos.Currency;
using Application.Services.CurrencyService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using System.Security.Claims;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrenciesController(ICurrencyService currencyService) : ControllerBase
    {
        [HttpPost]
        [Route("[Action]")]
        public async Task<IActionResult> AddNewCurrency([FromBody] AddNewCurrencyRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid Request");

            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            
            var result = await currencyService.AddNewCurrency(userId,request);

            if (!result)
                return StatusCode(500, "Service Error");

            return Ok("New Currency Added Succesfully .");
        }
    }
}
