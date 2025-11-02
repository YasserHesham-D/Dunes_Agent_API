using Application.Dtos.HotelsandLocations;
using Application.Services.HotelService;
using Application.Services.LocationService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly ILocationService locationService;

        public LocationController(ILocationService locationService)
        {
            this.locationService = locationService;
        }


        [HttpPost]
        [Route("[Action]")]
        //[Authorize(Roles="Admin,OpreationManager")]
        public async Task<IActionResult> AddNewLocation(AddNewDTO newLocationDto)
        {

            if (newLocationDto == null)
            {
                return NotFound("Data Is Missed Creditionals");
            }
            var newlocation = await locationService.AddNewLocation(newLocationDto);

            if (newlocation == null)
            {
                return NotFound("Data Is Missed Creditionals");
            }


            if (newlocation != null && newlocation.Success == true)
            {
                return new JsonResult(newlocation.Message);
            }

            return BadRequest(newlocation.Message);

        }

        [HttpPatch]
        [Route("[Action]/{Id}")]
        //[Authorize(Roles="Admin,OpreationManager")]
        public async Task<IActionResult> UpdateLocation(Guid Id, UpdateDTO UpdatedLocationDTO)
        {

            var updatedlocation = await locationService.UpdateLocation(Id, UpdatedLocationDTO);

            if (updatedlocation == null || UpdatedLocationDTO == null)
            {
                return new JsonResult("No Changes Happened");
            }


            if (updatedlocation != null && updatedlocation.Success == true)
            {
                return new JsonResult(updatedlocation.Message);
            }

            return BadRequest(updatedlocation.Message);
        }

        [HttpDelete]
        [Route("[Action]/{Id}")]
        //[Authorize(Roles="Admin,OpreationManager")]
        public async Task<IActionResult> DeleteLocation(Guid Id)
        {
            var deletedLocation = await locationService.DeleteLocation(Id);

            if (deletedLocation != null && deletedLocation.Success == true)
            {
                return new JsonResult(deletedLocation.Message);
            }

            return BadRequest(deletedLocation.Message);
        }

        [HttpGet]
        [Route("[Action]")]
        //[Authorize(Roles="Admin,OpreationManager")]

        public async Task<IActionResult> GetAllLocations()
        {
            var locations = await locationService.GetAllLocations();

            if (locations == null || locations.Count() == 0)
            {
                return new JsonResult("No Locations Existed");
            }

            return new JsonResult(locations);
        }

        [HttpGet]
        [Route("[Action]")]
        //[Authorize(Roles="Admin,OpreationManager")]
        public async Task<IActionResult> GetAllLocationsPaginated
        (string? Name, string? Place, string? Employee, DateTime? DateFrom, DateTime? DateTo, string SortColumn = "EntryDate", bool IsAscending = false, int Page = 1, int PageSize = 20)
        {
            var locations = await locationService.GetAllLocationsPaginated(Name, Place, Employee, DateFrom, DateTo, SortColumn, IsAscending, Page, PageSize);

            if (locations == null || locations.GetCount() == 0)
            {
                return new JsonResult("No Locations Existed");
            }

            return new JsonResult(locations);
        }

        [HttpGet]
        [Route("[Action]/{Id}")]
        //[Authorize(Roles="Admin,OpreationManager")]

        public async Task<IActionResult> GetLocationDetails(Guid Id)
        {
            var existedlocation = await locationService.GetLocationDetails(Id);

            if (existedlocation != null)
            {
                return new JsonResult(existedlocation);
            }

            return NotFound("Location Data Not Found");
        }

        [HttpGet]
        [Route("[Action]")]
        //[Authorize(Roles="Admin,OpreationManager")]
        public async Task<IActionResult> GetLocationEmployeesCountPaginated(int Page = 1, int PageSize = 20)
        {
            var locationemployees = await locationService.GetLocationsEmployeesCount(Page, PageSize);

            if (locationemployees != null)
            {
                return new JsonResult(locationemployees);
            }

            return NotFound("Location Employees Count Not Found");
        }

        [HttpGet]
        [Route("[Action]")]
        //[Authorize(Roles="Admin,OpreationManager")]
        public async Task<IActionResult> GetLocationEmployeesCount()
        {
            var locationemployees = await locationService.GetEmployeesByLocationAsync();

            if (locationemployees != null)
            {
                return new JsonResult(locationemployees);
            }

            return NotFound("Location Employees Not Found");
        }
    }
}
