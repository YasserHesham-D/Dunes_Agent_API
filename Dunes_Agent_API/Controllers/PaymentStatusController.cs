using Application.Dtos.Payment_Methods_and_Status;
using Application.Services.PaymentMethodService;
using Application.Services.PaymentStatusService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentStatusController(IPaymentStatusService paymentStatusService) : ControllerBase
    {
        [HttpPost]
        [Route("[Action]")]
        //[Authorize(Roles="Admin,OpreationManager")]
        public async Task<IActionResult> AddNewStatus(AddNewMethodandStatusDTO statusDTO)
        {

            if (statusDTO == null)
            {
                return NotFound("Data Is Missed Creditionals");
            }

            var newstatus = await paymentStatusService.AddNewStatus(statusDTO);

            if (newstatus == null)
            {
                return NotFound("Data Is Missed Creditionals");
            }


            if (newstatus != null && newstatus.Success == true)
            {
                return new JsonResult(newstatus.Message);
            }

            return BadRequest(newstatus.Message);

        }

        [HttpPatch]
        [Route("[Action]/{Id}")]
        //[Authorize(Roles="Admin,OpreationManager")]
        public async Task<IActionResult> UpdateStatus(Guid Id, UpdateMethodandStatusDTO updatestatusDTO)
        {

            var updatedstatus = await paymentStatusService.UpdateStatus(Id, updatestatusDTO);

            if (updatedstatus == null || updatestatusDTO == null)
            {
                return new JsonResult("No Changes Happened");
            }


            if (updatedstatus != null && updatedstatus.Success == true)
            {
                return new JsonResult(updatedstatus.Message);
            }

            return BadRequest(updatedstatus.Message);
        }

        [HttpDelete]
        [Route("[Action]/{Id}")]
        //[Authorize(Roles="Admin,OpreationManager")]
        public async Task<IActionResult> DeleteStatus(Guid Id)
        {
            var deletedStatus = await paymentStatusService.DeleteStatus(Id);

            if (deletedStatus != null && deletedStatus.Success == true)
            {
                return new JsonResult(deletedStatus.Message);
            }

            return BadRequest(deletedStatus.Message);
        }

        [HttpGet]
        [Route("[Action]")]
        //[Authorize(Roles="Admin,OpreationManager")]

        public async Task<IActionResult> GetAllStatuses()
        {
            var statuses = await paymentStatusService.GetAllStatuses();

            if (statuses == null || statuses.Count() == 0)
            {
                return new JsonResult("No Statuses Existed");
            }

            return new JsonResult(statuses);
        }

        [HttpGet]
        [Route("[Action]")]
        //[Authorize(Roles="Admin,OpreationManager")]
        public async Task<IActionResult> GetAllStatusesPaginated
        (string? Name, string? Employee, string SortColumn = "Name", bool IsAscending = false, int Page = 1, int PageSize = 20)
        {
            var statuses = await paymentStatusService.GetAllStatusPaginated(Name, Employee, SortColumn, IsAscending, Page, PageSize);

            if (statuses == null || statuses.GetCount() == 0)
            {
                return new JsonResult("No Statuses Existed");
            }

            return new JsonResult(statuses);
        }

        [HttpGet]
        [Route("[Action]/{Id}")]
        //[Authorize(Roles="Admin,OpreationManager")]

        public async Task<IActionResult> GetStatusDetails(Guid Id)
        {
            var existedstatus = await paymentStatusService.GetStatusDetails(Id);

            if (existedstatus != null)
            {
                return new JsonResult(existedstatus);
            }

            return NotFound("Status Data Not Found");
        }

        [HttpGet]
        [Route("[Action]")]

        public async Task<IActionResult> GetStatusBookingsCountPaginated(int Page = 1, int PageSize = 20)
        {
            var StatusBookings = await paymentStatusService.GetStatusBookingsCount(Page, PageSize);

            if (StatusBookings != null)
            {
                return new JsonResult(StatusBookings);
            }

            return NotFound("Status Bookings Count Not Found");
        }
    }
}
