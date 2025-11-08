using Domain.Interfaces.IRepository;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.IModelsRepo
{
    public interface INotificationRepo : IRepo<Notification>
    {
        Task CreateAsync(Notification notification);
        Task MarkAsReadAsync(Guid notificationId);
        Task<IEnumerable<Notification>> GetForEmployeeAsync(string employeeId);
    }
}
