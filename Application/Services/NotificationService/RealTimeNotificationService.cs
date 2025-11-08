using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.NotificationService
{
    public class RealTimeNotificationService : IRealTimeNotificationService
    {
        public Task SendNotificationToRolesAsync(IEnumerable<string> roles, object notificationPayload)
        {
            throw new NotImplementedException();
        }

        public Task SendNotificationToUserAsync(string employeeId, object notification)
        {
            throw new NotImplementedException();
        }
    }
}
