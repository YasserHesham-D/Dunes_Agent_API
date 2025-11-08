using Domain.Models.Accounts;
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
        
        public DateTime CreatedAt { get; set; }= DateTime.Now;
        public bool IsRead { get; set; } 
        public string Message { get; set; } = null! ;

        public string EmployeeId { get; set; } = null!;
        public virtual Employee Employee { get; set; } 
    }
    public class NotificationConfigration : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> builder)
        {
            builder.ToTable("Notifications");

            builder.HasKey(x => x.Id);

            builder.Property(e => e.CreatedAt).HasColumnType("datetime2");
            //builder.Property(e => e.PickupDT).HasColumnType("datetime2");

            builder.Property(e => e.ProcessName).HasColumnType("NVARCHAR(100)").HasMaxLength(100);

            builder.Property(e => e.Message).HasColumnType("NVARCHAR(300)").HasMaxLength(300);

            builder.Property(e => e.IsRead)
              .HasDefaultValue(false);

            builder.HasOne(x => x.Employee)
                .WithMany(x => x.Notifications)
                .HasForeignKey(x => x.EmployeeId)
                .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
