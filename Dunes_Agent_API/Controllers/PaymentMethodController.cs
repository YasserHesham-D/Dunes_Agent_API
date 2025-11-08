using Application.Dtos.HotelsandLocations;
using Application.Dtos.Payment_Methods_and_Status;
using Application.Services.HotelService;
using Application.Services.PaymentMethodService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentMethodController(IPaymentMethodService paymentMethodService) : ControllerBase
    {
        [HttpPost]
        [Route("[Action]")]
        //[Authorize(Roles="Admin,OpreationManager")]
        public async Task<IActionResult> AddNewMethod(AddNewMethodandStatusDTO methodDTO)
        {

            if (methodDTO == null)
            {
                return NotFound("Data Is Missed Creditionals");
            }

            var newmethod = await paymentMethodService.AddNewMethod(methodDTO);

            if (newmethod == null)
            {
                return NotFound("Data Is Missed Creditionals");
            }


            if (newmethod != null && newmethod.Success == true)
            {
                return new JsonResult(newmethod.Message);
            }

            return BadRequest(newmethod.Message);

        }

        [HttpPatch]
        [Route("[Action]/{Id}")]
        //[Authorize(Roles="Admin,OpreationManager")]
        public async Task<IActionResult> UpdateMethod(Guid Id, UpdateMethodandStatusDTO updateMethodDTO)
        {

            var updatedmethod = await paymentMethodService.UpdateMethod(Id, updateMethodDTO);

            if (updatedmethod == null || updateMethodDTO == null)
            {
                return new JsonResult("No Changes Happened");
            }


            if (updatedmethod != null && updatedmethod.Success == true)
            {
                return new JsonResult(updatedmethod.Message);
            }

            return BadRequest(updatedmethod.Message);
        }

        [HttpDelete]
        [Route("[Action]/{Id}")]
        //[Authorize(Roles="Admin,OpreationManager")]
        public async Task<IActionResult> DeleteMethod(Guid Id)
        {
            var deletedmethod = await paymentMethodService.DeleteMethod(Id);

            if (deletedmethod != null && deletedmethod.Success == true)
            {
                return new JsonResult(deletedmethod.Message);
            }

            return BadRequest(deletedmethod.Message);
        }

        [HttpGet]
        [Route("[Action]")]
        //[Authorize(Roles="Admin,OpreationManager")]

        public async Task<IActionResult> GetAllMethods()
        {
            var methods = await paymentMethodService.GetAllMethods();

            if (methods == null || methods.Count() == 0)
            {
                return new JsonResult("No Methods Existed");
            }

            return new JsonResult(methods);
        }

        [HttpGet]
        [Route("[Action]")]
        //[Authorize(Roles="Admin,OpreationManager")]
        public async Task<IActionResult> GetAllMethodsPaginated
        (string? Name, string? Employee,string SortColumn = "Name", bool IsAscending = false, int Page = 1, int PageSize = 20)
        {
            var methods = await paymentMethodService.GetAllMethodsPaginated(Name,Employee,SortColumn, IsAscending, Page, PageSize);

            if (methods == null || methods.GetCount() == 0)
            {
                return new JsonResult("No Methods Existed");
            }

            return new JsonResult(methods);
        }

        [HttpGet]
        [Route("[Action]/{Id}")]
        //[Authorize(Roles="Admin,OpreationManager")]

        public async Task<IActionResult> GetMethodDetails(Guid Id)
        {
            var existedmethod = await paymentMethodService.GetMethodDetails(Id);

            if (existedmethod != null)
            {
                return new JsonResult(existedmethod);
            }

            return NotFound("Method Data Not Found");
        }

        [HttpGet]
        [Route("[Action]")]

        public async Task<IActionResult> GetMethodBookingsCountPaginated(int Page = 1, int PageSize = 20)
        {
            var MethodBookings = await paymentMethodService.GetMethodBookingsCount(Page, PageSize);

            if (MethodBookings != null)
            {
                return new JsonResult(MethodBookings);
            }

            return NotFound("Method Bookings Count Not Found");
        }


        [HttpGet]
        [Route("[Action]")]

        public async Task<IActionResult> GetMethodVouchersCountPaginated(int Page = 1, int PageSize = 20)
        {
            var MethodVouchers = await paymentMethodService.GetMethodVouchersCount(Page, PageSize);

            if (MethodVouchers != null)
            {
                return new JsonResult(MethodVouchers);
            }

            return NotFound("Method Vouchers Count Not Found");
        }


        [HttpGet]
        [Route("[Action]")]

        public async Task<IActionResult> GetMethodOpreationsCountPaginated(int Page = 1, int PageSize = 20)
        {
            var MethodOpreations = await paymentMethodService.GetMethodOpreationsCount(Page, PageSize);

            if (MethodOpreations != null)
            {
                return new JsonResult(MethodOpreations);
            }

            return NotFound("Method Opreations Count Not Found");
        }



    }
}
