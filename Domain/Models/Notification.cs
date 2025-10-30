using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Notification
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string ProcessName { get; set; } = null!;
        public Guid ProcessId { get; set; }
        
        public DateTime CreatedAt { get; set; }
        public bool IsRead { get; set; }

        public string Message { get; set; } = null! ;

        public Guid EmployeeId { get; set; }
        public virtual Employee Employee { get; set; } = null!;
    }
    public class NotificationConfigration : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> builder)
        {


        }
    }
}
