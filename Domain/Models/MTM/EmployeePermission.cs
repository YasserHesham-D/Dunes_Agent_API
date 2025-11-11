using Domain.Models.Accounts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.MTM
{
    public class EmployeePermission
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public bool IsDeleted { get; set; } = false;
        public bool IsGranted { get; set; } = false; // true,false

        public string? EmployeeId { get; set; }
        public virtual Employee? Employee { get; set; }

        public Guid PermissionId { get; set; }
        public virtual Permission? permission {get;set;}
    }
    public class EmployeePermissionConfiguration : IEntityTypeConfiguration<EmployeePermission>
    {
        public void Configure(EntityTypeBuilder<EmployeePermission> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.IsDeleted).HasDefaultValue(false);
            builder.Property(x => x.IsGranted).HasDefaultValue(false);

            builder.HasOne(x =>x.Employee)
                .WithMany(x => x.Permissions)
                .HasForeignKey(x => x.EmployeeId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.permission)
                .WithMany(x => x.employees)
                .HasForeignKey(x => x.PermissionId)
                .OnDelete(DeleteBehavior.NoAction);

        }
    }

}
