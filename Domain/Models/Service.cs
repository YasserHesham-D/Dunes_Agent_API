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
    public class Service
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public DateTime Duration { get; set; }
        public DateTime EntryDate { get; set; } = DateTime.UtcNow;
        
        public string Name { get; set; } = null!;
        public string? Description { get; set; } = null;
        public string? Type { get; set; } = null;

        public Guid EmployeeAddedId { get; set; }
        public virtual Employee Employee { get; set; } = null!;

        public ICollection<LocationServices> Services { get; set; } = null!;

        public ICollection<BookingServices> BookingServices { get; set; } = null!;

        public ICollection<ReciptVoucherServices> VoucherServices { get; set; } = null!;



    }
    public class ServiceConfigration : IEntityTypeConfiguration<Service>
    {
        public void Configure(EntityTypeBuilder<Service> builder)
        {


        }
    }

}
