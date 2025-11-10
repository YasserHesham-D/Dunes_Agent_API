using Domain.Models.Accounts;
using Domain.Models.MTM;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Permission
    {
        public Guid id { get; set; } = Guid.NewGuid();
        public bool IsDeleted { get; set; } = false;

        public string Module { get; set; } = null!;  // booking , employee , service , recipt voucher
        public string Action { get; set; } = null!; // show add , edit , delete
        public bool IsGranted { get; set; } = false;// true,false

        public ICollection<EmployeePermission>? employees { get; set; } = new List<EmployeePermission>();
    }
    public class EmployeeConfigration : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.HasKey(x =>x.id);

            builder.Property(x => x.IsDeleted).HasDefaultValue(false);

            builder.Property(x => x.Module)
                .IsRequired().HasMaxLength(50);

            builder.Property(x => x.Action)
                .IsRequired().HasMaxLength(80);

            builder.Property(x=> x.IsGranted)
                .IsRequired().HasDefaultValue(false);

            builder.HasMany(x => x.employees).WithOne(x => x.permission);
        }
    }
}
