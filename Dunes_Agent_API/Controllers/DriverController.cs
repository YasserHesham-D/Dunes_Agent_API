using Application.Dtos.HotelsandLocations;
using Application.DTOS.Driver;
using Application.Services.DriverService;
using Application.Services.HotelService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DriverController(IDriverService driverService) : ControllerBase
    {
        [HttpPost]
        [Route("[Action]")]
        //[Authorize(Roles="Admin,OpreationManager")]
        public async Task<IActionResult> AddNewDriver(AddNewDriverDTO newDriverDTO)
        {

            if (newDriverDTO == null)
            {
                return NotFound("Data Is Missed Creditionals");
            }

            var newdriver = await driverService.AddNewDriver(newDriverDTO);

            if (newdriver == null)
            {
                return NotFound("Data Is Missed Creditionals");
            }


            if (newdriver != null && newdriver.Success == true)
            {
                return new JsonResult(newdriver.Message);
            }

            return BadRequest(newdriver.Message);

        }

        [HttpPatch]
        [Route("[Action]/{Id}")]
        //[Authorize(Roles="Admin,OpreationManager")]
        public async Task<IActionResult> UpdateDriver(Guid Id, UpdateDriverDTO updateDriverDTO)
        {

            var updateddriver = await driverService.UpdateDriver(Id, updateDriverDTO);

            if (updateddriver == null || updateDriverDTO == null)
            {
                return new JsonResult("No Changes Happened");
            }


            if (updateddriver != null && updateddriver.Success == true)
            {
                return new JsonResult(updateddriver.Message);
            }

            return BadRequest(updateddriver.Message);
        }

        [HttpDelete]
        [Route("[Action]/{Id}")]
        //[Authorize(Roles="Admin,OpreationManager")]
        public async Task<IActionResult> DeleteDriver(Guid Id)
        {
            var deleteddriver = await driverService.DeleteDriver(Id);

            if (deleteddriver != null && deleteddriver.Success == true)
            {
                return new JsonResult(deleteddriver.Message);
            }

            return BadRequest(deleteddriver.Message);
        }

        [HttpGet]
        [Route("[Action]")]
        //[Authorize(Roles="Admin,OpreationManager")]

        public async Task<IActionResult> GetAllDrivers()
        {
            var drivers = await driverService.GetAllDrivers();

            if (drivers == null || drivers.Count() == 0)
            {
                return new JsonResult("No Drivers Existed");
            }

            return new JsonResult(drivers);
        }

        [HttpGet]
        [Route("[Action]")]
        //[Authorize(Roles="Admin,OpreationManager")]
        public async Task<IActionResult> GetAllDriversPaginated
        (string? Name, string? CarNumber, string? PhoneNumber, string? Place, string? Employee, DateTime? DateFrom, DateTime? DateTo, string SortColumn = "DateOfAdd", bool IsAscending = false, int Page = 1, int PageSize = 20)
        {
            var drivers = await driverService.GetAllDriversPaginated(Name, CarNumber,PhoneNumber,Place,Employee, DateFrom, DateTo, SortColumn, IsAscending, Page, PageSize);

            if (drivers == null || drivers.GetCount() == 0)
            {
                return new JsonResult("No Drivers Existed");
            }

            return new JsonResult(drivers);
        }

        [HttpGet]
        [Route("[Action]/{Id}")]
        //[Authorize(Roles="Admin,OpreationManager")]

        public async Task<IActionResult> GetDriverDetails(Guid Id)
        {
            var existeddriver = await driverService.GetDriverDetails(Id);

            if (existeddriver != null)
            {
                return new JsonResult(existeddriver);
            }

            return NotFound("Driver Data Not Found");
        }

        [HttpGet]
        [Route("[Action]")]

        public async Task<IActionResult> GetDriverBookingsCountPaginated(int Page = 1, int PageSize = 20)
        {
            var driverbookings = await driverService.GetDriverBookingsCount(Page, PageSize);

            if (driverbookings != null)
            {
                return new JsonResult(driverbookings);
            }

            return NotFound("Driver Bookings Count Not Found");
        }

    }
}
