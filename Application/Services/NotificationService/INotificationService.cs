using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.NotificationService
{
    public interface INotificationService
    {



        Task CreateAsync(Notification notification);
        Task MarkAsReadAsync(Guid notificationId);
        Task<IEnumerable<Notification>> GetForEmployeeAsync(string employeeId);
    }
}
