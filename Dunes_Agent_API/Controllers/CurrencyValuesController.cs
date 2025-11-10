using Application.Dtos.Currency;
using Application.DTOS.Currency;
using Application.DTOS.Currency_Values;
using Application.Services.CurrencyService;
using Application.Services.CurrencyValuesService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyValuesController(ICurrencyValuesService currencyValuesService) : ControllerBase
    {
        [HttpPost]
        [Route("[Action]")]
        public async Task<IActionResult> AddNewCurrencyValue([FromBody] AddNewCurrencyValueDTO request)
        {
            //if (!ModelState.IsValid)
            //    return BadRequest("Invalid Request");

            //var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            //var result = await currencyService.AddNewCurrency(userId,request);

            //if (!result)
            //    return StatusCode(500, "Service Error");

            //return Ok("New Currency Added Succesfully .");


            if (request == null)
            {
                return NotFound("Data Is Missed Creditionals");
            }

            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var newCurrency = await currencyValuesService.AddNewCurrencyValue(request);

            if (newCurrency == null)
            {
                return NotFound("Data Is Missed Creditionals");
            }


            if (newCurrency != null && newCurrency.Success == true)
            {
                return new JsonResult(newCurrency.Message);
            }

            return BadRequest(newCurrency.Message);
        }


        [HttpPatch]
        [Route("[Action]/{Id}")]
        //[Authorize(Roles="Admin,OpreationManager")]
        public async Task<IActionResult> UpdateCurrencyValue(Guid Id, UpdateCurrencyValueDTO updateCurrencyDTO)
        {

            var updatedCurrency = await currencyValuesService.UpdateCurrencyValue(Id, updateCurrencyDTO);

            if (updatedCurrency == null || updateCurrencyDTO == null)
            {
                return new JsonResult("No Changes Happened");
            }


            if (updatedCurrency != null && updatedCurrency.Success == true)
            {
                return new JsonResult(updatedCurrency.Message);
            }

            return BadRequest(updatedCurrency.Message);
        }

        [HttpDelete]
        [Route("[Action]/{Id}")]
        //[Authorize(Roles="Admin,OpreationManager")]
        public async Task<IActionResult> DeleteCurrencyValue(Guid Id)
        {
            var deletedCurrency = await currencyValuesService.DeleteCurrencyValue(Id);

            if (deletedCurrency != null && deletedCurrency.Success == true)
            {
                return new JsonResult(deletedCurrency.Message);
            }

            return BadRequest(deletedCurrency.Message);
        }


        [HttpGet]
        [Route("[Action]")]
        //[Authorize(Roles="Admin,OpreationManager")]
        public async Task<IActionResult> GetAllCurrencyValuesPaginated
        (string? CurrencyFrom, string? CurrencyTo, string? Employee, string SortColumn = "Price", bool IsAscending = false, int Page = 1, int PageSize = 20)
        {
            var curruncices = await currencyValuesService.GetAllCurrencyValuesPaginated(CurrencyFrom, CurrencyTo,Employee, SortColumn, IsAscending, Page, PageSize);

            if (curruncices == null || curruncices.GetCount() == 0)
            {
                return new JsonResult("No Currincices Values Existed");
            }

            return new JsonResult(curruncices);
        }

        [HttpGet]
        [Route("[Action]/{Id}")]
        //[Authorize(Roles="Admin,OpreationManager")]

        public async Task<IActionResult> GetCurrencyValueDetails(Guid Id)
        {
            var existedCurrency = await currencyValuesService.GetCurrencyValueDetails(Id);

            if (existedCurrency != null)
            {
                return new JsonResult(existedCurrency);
            }

            return NotFound("Currency Data Not Found");
        }
    }
}
