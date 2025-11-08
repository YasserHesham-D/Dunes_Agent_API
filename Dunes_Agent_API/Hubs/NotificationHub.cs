using Application.Services.NotificationService;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace Presentation.Hubs
{
    public class NotificationHub : Hub
    {
        public async Task SendNotification(string message)
        {
            await Clients.All.SendAsync("ReceiveNotification", message);
        }

        public override async Task OnConnectedAsync()
        {
            var employeeId = Context.User?.FindFirst("EmployeeId")?.Value;
            var roles = Context.User?.FindAll(ClaimTypes.Role).Select(c => c.Value).ToList() ?? new();

            if (!string.IsNullOrEmpty(employeeId))
            {
                // Join user’s personal group
                await Groups.AddToGroupAsync(Context.ConnectionId, employeeId);
            }

            if (roles.Any())
            {
                // Join each role group
                foreach (var role in roles)
                {
                    await Groups.AddToGroupAsync(Context.ConnectionId, role);
                }
            }

            await base.OnConnectedAsync();
        }

    }
    public class RealTimeNotificationService(IHubContext<NotificationHub> _hubContext) : IRealTimeNotificationService
    {


        public async Task SendNotificationToRolesAsync(IEnumerable<string> roles, object notificationPayload)
        {
            await _hubContext.Clients.Groups(roles)
            .SendAsync("ReceiveNotification", notificationPayload);
        }

        public async Task SendNotificationToUserAsync(string employeeId, object notification)
        {
            // We assume clients join groups using their EmployeeId
            await _hubContext.Clients.Group(employeeId).SendAsync("ReceiveNotification", notification);
        }
    }
}
