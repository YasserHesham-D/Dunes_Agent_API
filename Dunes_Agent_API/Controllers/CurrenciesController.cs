using Application.Dtos.Currency;
using Application.Dtos.Payment_Methods_and_Status;
using Application.DTOS.Currency;
using Application.Services.CurrencyService;
using Application.Services.PaymentMethodService;
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

            if (request == null)
            {
                return NotFound("Data Is Missed Creditionals");
            }

            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var newCurrency = await currencyService.AddNewCurrency(userId, request);

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
        public async Task<IActionResult> UpdateCurrency(Guid Id, UpdateCurrencyDTO updateCurrencyDTO)
        {

            var updatedCurrency = await currencyService.UpdateCurrency(Id, updateCurrencyDTO);

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
        public async Task<IActionResult> DeleteCurrency(Guid Id)
        {
            var deletedCurrency = await currencyService.DeleteCurrency(Id);

            if (deletedCurrency != null && deletedCurrency.Success == true)
            {
                return new JsonResult(deletedCurrency.Message);
            }

            return BadRequest(deletedCurrency.Message);
        }

        [HttpGet]
        [Route("[Action]")]
        //[Authorize(Roles="Admin,OpreationManager")]

        public async Task<IActionResult> GetAllCurrencies()
        {
            var currencies = await currencyService.GetAllCurrencices();

            if (currencies == null || currencies.Count() == 0)
            {
                return new JsonResult("No Currencies Existed");
            }

            return new JsonResult(currencies);
        }

        [HttpGet]
        [Route("[Action]")]
        //[Authorize(Roles="Admin,OpreationManager")]
        public async Task<IActionResult> GetAllCurruncicesPaginated
        (string? Name, string? Employee, string SortColumn = "Name", bool IsAscending = false, int Page = 1, int PageSize = 20)
        {
            var curruncices = await currencyService.GetAllCurrencicesPaginated(Name, Employee, SortColumn, IsAscending, Page, PageSize);

            if (curruncices == null || curruncices.GetCount() == 0)
            {
                return new JsonResult("No Currincices Existed");
            }

            return new JsonResult(curruncices);
        }

        [HttpGet]
        [Route("[Action]/{Id}")]
        //[Authorize(Roles="Admin,OpreationManager")]

        public async Task<IActionResult> GetCurrencyDetails(Guid Id)
        {
            var existedCurrency = await currencyService.GetCurrencyDetails(Id);

            if (existedCurrency != null)
            {
                return new JsonResult(existedCurrency);
            }

            return NotFound("Currency Data Not Found");
        }

        [HttpGet]
        [Route("[Action]")]

        public async Task<IActionResult> GetCurrencyBookingsCountPaginated(int Page = 1, int PageSize = 20)
        {
            var CurrunceyBookings = await currencyService.GetCurrencyBookingsCount(Page, PageSize);

            if (CurrunceyBookings != null)
            {
                return new JsonResult(CurrunceyBookings);
            }

            return NotFound("Currency Bookings Count Not Found");
        }


        [HttpGet]
        [Route("[Action]")]
        public async Task<IActionResult> GetCurrencyVouchersCountPaginated(int Page = 1, int PageSize = 20)
        {
            var CurrencyVouchers = await currencyService.GetCurrencyVouchersCount(Page, PageSize);

            if (CurrencyVouchers != null)
            {
                return new JsonResult(CurrencyVouchers);
            }

            return NotFound("Currency Vouchers Count Not Found");
        }


        [HttpGet]
        [Route("[Action]")]
        public async Task<IActionResult> GetCurrencyOpreationsCountPaginated(int Page = 1, int PageSize = 20)
        {
            var CurrencyOpreations = await currencyService.GetCurrencyOpreationsCount(Page, PageSize);

            if (CurrencyOpreations != null)
            {
                return new JsonResult(CurrencyOpreations);
            }

            return NotFound("Currency Opreations Count Not Found");
        }
    }
}
