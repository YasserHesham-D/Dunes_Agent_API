using Application.Services.NotificationService;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController(INotificationService notificationService) : ControllerBase
    {


        [HttpPatch]
        [Route("[Action]/{id}")]
        public async Task<IActionResult> MarkAsRead(Guid id)
        {
            await notificationService.MarkAsReadAsync(id);
            return NoContent();
        }

        [HttpGet]
        [Route("[Action]")]
        public async Task<IActionResult> GetForEmployee()
        {
            // getting employee Id for the user of enpoint
            var employeeId = User.FindFirst("EmployeeId")?.Value;
            
            if (employeeId is null)
                return Unauthorized("Missing EmployeeId claim");

            var notifications = await notificationService.GetForEmployeeAsync(employeeId);
            return Ok(notifications);
        }
    }
}
