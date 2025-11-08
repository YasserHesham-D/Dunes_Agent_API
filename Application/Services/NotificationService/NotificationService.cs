using Domain.Interfaces.IModelsRepo;
using Domain.Interfaces.IRepository;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.NotificationService
{
    public class NotificationService(INotificationRepo notificationRepo, IRealTimeNotificationService realTimeNotification) : INotificationService
    {
        public async Task CreateAsync(Notification notification)
        {
            await notificationRepo.CreateAsync(notification);

            // Send it in real time
            var payload 
                = new
                {   
                    notification.Message,
                    notification.ProcessId,
                    notification.ProcessName
                };

            var roles = new[] { "Admin", "OperationManager" };
            await realTimeNotification.SendNotificationToRolesAsync(roles, payload);

        }

        public async Task MarkAsReadAsync(Guid notificationId)
        {
            await notificationRepo.MarkAsReadAsync(notificationId);
        }

        public async Task<IEnumerable<Notification>> GetForEmployeeAsync(string employeeId)
        {
            return await notificationRepo.GetForEmployeeAsync(employeeId);
        }
    }
}
