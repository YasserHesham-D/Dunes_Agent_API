using Application.DTOS;
using Application.DTOS.Hotels;
using Domain.Interfaces.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        private readonly IHotelService hotelService;

        public HotelController(IHotelService hotelService)
        {
            this.hotelService = hotelService;
        }

        [HttpPost]
        [Route("[Action]")]
        //[Authorize(Roles="Admin,OpreationManager")]
        public async Task<IActionResult> AddNewHotel(AddNewHotelDTO newHotelDTO)
        {

            if (newHotelDTO == null)
            {
                return NotFound("Data Is Missed Creditionals");
            }
            var newhotel = await hotelService.AddNewHotel(newHotelDTO);

            if (newhotel == null) {
                return NotFound("Data Is Missed Creditionals");
            }


            if(newhotel != null && newhotel.Success == true)
            {
                return new JsonResult(newhotel.Message);
            }
            
            return BadRequest(newhotel.Message);
           
        }

        [HttpPatch]
        [Route("[Action]/{Id}")]
        //[Authorize(Roles="Admin,OpreationManager")]
        public async Task<IActionResult> UpdateHotel(Guid Id , UpdateHotelDTO UpdatedHotelDTO)
        {
            
            var updatedhotel = await hotelService.UpdateHotel(Id, UpdatedHotelDTO);

            if (updatedhotel == null || UpdatedHotelDTO == null)
            {
                return new JsonResult("No Changes Happened");
            }


            if (updatedhotel != null && updatedhotel.Success == true)
            {
                return new JsonResult(updatedhotel.Message);
            }

            return BadRequest(updatedhotel.Message);
        }

        [HttpDelete]
        [Route("[Action]/{Id}")]
        //[Authorize(Roles="Admin,OpreationManager")]
        public async Task<IActionResult> DeleteHotel(Guid Id)
        {
            var deletedhotel = await hotelService.DeleteHotel(Id);

            if (deletedhotel != null && deletedhotel.Success == true)
            {
                return new JsonResult(deletedhotel.Message);
            }

            return BadRequest(deletedhotel.Message);
        }

        [HttpGet]
        [Route("[Action]")]
        //[Authorize(Roles="Admin,OpreationManager")]

        public async Task<IActionResult> GetAllHotels()
        {
            var hotels = await hotelService.GetAllHotels();

            if(hotels == null || hotels.Count() == 0)
            {
                return new JsonResult("No Hotels Existed");
            }

            return new JsonResult(hotels);
        }

        [HttpGet]
        [Route("[Action]")]
        //[Authorize(Roles="Admin,OpreationManager")]
        public async Task<IActionResult> GetAllHotelsPaginated
        (string? Name, string? Place, string? Employee, DateTime? DateFrom, DateTime? DateTo, string SortColumn="EntryDate",bool IsAscending = false, int Page = 1, int PageSize = 20)
        {
            var hotels = await hotelService.GetAllHotelsPaginated(Name,Place,Employee,DateFrom,DateTo,SortColumn,IsAscending,Page,PageSize);

            if (hotels == null || hotels.GetCount() == 0)
            {
                return new JsonResult("No Hotels Existed");
            }

            return new JsonResult(hotels);
        }

        [HttpGet]
        [Route("[Action]/{Id}")]
        //[Authorize(Roles="Admin,OpreationManager")]

        public async Task<IActionResult> GetHotelDetails(Guid Id)
        {
            var existedhotel = await hotelService.GetHotelDetails(Id);

            if (existedhotel != null)
            {
                return new JsonResult(existedhotel);
            }

            return NotFound("Hotel Data Not Found");
        }

        [HttpGet]
        [Route("[Action]")]

        public async Task<IActionResult> GetHotelEmployeesCountPaginated(int Page = 1 ,int PageSize = 20)
        {
            var hotelemployees = await hotelService.GetHotelEmployeesCount(Page,PageSize);

            if (hotelemployees != null)
            {
                return new JsonResult(hotelemployees);
            }

            return NotFound("Hotel Employees Count Not Found");
        }





    }
}
