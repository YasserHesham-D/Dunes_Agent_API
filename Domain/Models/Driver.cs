using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Driver
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = null!;
        public string phoneNumber { get; set; } = null!;
        public string CarNumber { get; set; } = null!;
        public string PlaceOfWork { get; set; } = null!;
        public DateTime DateOfAdd { get; set; }= DateTime.UtcNow;
        public Guid EmployeeAddedId { get; set; }

        public virtual Employee Employee { get; set; } = null!;

        public ICollection<Booking>? Bookings { get; set; }

    }
    public class DriverConfigration : IEntityTypeConfiguration<Driver>
    {
        public void Configure(EntityTypeBuilder<Driver> builder)
        {


        }
    }
}
