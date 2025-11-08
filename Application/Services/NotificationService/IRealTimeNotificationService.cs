using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.NotificationService
{
    public interface IRealTimeNotificationService
    {
        Task SendNotificationToUserAsync(string employeeId, object notification);
        Task SendNotificationToRolesAsync(IEnumerable<string> roles, object notificationPayload);
    }
}
