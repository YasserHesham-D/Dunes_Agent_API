using Application.Dtos.Service;
using Application.Services.ServicesService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using System.Security.Claims;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicesController(IServicesService servicesService) : ControllerBase
    {
        [HttpPost]
        [Route("[Action]")]
        public async Task<IActionResult> AddNewService([FromBody] AddNewServiceRequest request)
        {
            if(!ModelState.IsValid)
                return BadRequest("Invalid Request");
            var UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            
            
            var Result = await servicesService.AddNewServiceAsync(request,UserId);
            if (!Result)
                return StatusCode(500, "ServiceError");

            return Ok("New Service Added Succesfully");
        }

        [HttpGet]
        [Route("[Action]")]
        public async Task<IActionResult> GetAllServices
            (string? empname, string? serviceName, string? LocationName, int? Duration,
            string SortColumn = "EntryDate", bool IsAscending = false, int Page = 1, int PageSize = 6 )
        {
            var Services = await servicesService.GetAllServicesPaginated
                (empname, serviceName,LocationName,Duration,SortColumn,IsAscending,Page ,PageSize,"EntryDate");

            if (Services.GetCount() < 1)
                return NotFound("No Services Exist");

            return Ok(Services);
        }

        [HttpDelete]
        [Route("[Action]/{id}")]
        public async Task<IActionResult> DeleteService(Guid id)
        {
            var result = await servicesService.DeleteServiceAsync(id);
            
            if (!result)
                return StatusCode(500, "Service Error");


            return Ok("Service Deleted Succesfully");
        }

        [HttpPatch]
        [Route("[Action]/{id}")]
        public async Task<IActionResult> UpdateService(Guid id , UpdateServiceRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest("INvalid Request");

            var result = await servicesService.PatchService(id, request);

            if (!result)
                return StatusCode(500, "Service Error");

            return Ok("Service Updated Succesfully .");
        }
    }
}
