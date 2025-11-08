using Application.Services.NotificationService;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> GetForEmployee(string employeeId)
        {
            var notifications = await notificationService.GetForEmployeeAsync(employeeId);
            return Ok(notifications);
        }
    }
}
