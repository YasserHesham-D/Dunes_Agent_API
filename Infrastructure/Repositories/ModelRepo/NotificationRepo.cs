using Domain.Interfaces.IModelsRepo;
using Domain.Models;
using Infrastructure.DBContext;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.ModelRepo
{
    public class NotificationRepo : Repository<Notification>, INotificationRepo
    {
        private readonly AppDbContext context;
        private readonly ILogger<Notification> logger;

        public NotificationRepo(AppDbContext context, ILogger<Notification> logger) : base(context, logger)
        {
            this.context = context;
            this.logger = logger;
        }

        public async Task CreateAsync(Notification notification)
        {
            _context.Notifications.Add(notification);
        }

        public async Task MarkAsReadAsync(Guid notificationId)
        {
            var notification = await _context.Notifications.FindAsync(notificationId);
            if (notification is null)
                throw new Exception("Notification not found.");

            notification.IsRead = true;
        }

        public async Task<IEnumerable<Notification>> GetForEmployeeAsync(string employeeId)
        {
            return await _context.Notifications
                .Where(n => n.EmployeeId == employeeId)
                .OrderByDescending(n => n.CreatedAt)
                .ToListAsync();
        }

    }

}
