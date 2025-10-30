using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Hotel
    {
        public Guid Id { get; set; } =Guid.NewGuid();
        public string Name { get; set; } = null!;
        public string Place { get; set; } = null!;
        public DateTime EntryDate { get; set; } = DateTime.UtcNow;
        public Guid EmployeeAddedId {  get; set; }

        public virtual Employee Employee { get; set; } = null!;

        public ICollection<Booking> Bookings { get; set; } = null!;

        public ICollection<Employee>? EmployeesBelong {  get; set; } 

    }
    public class HotelConfigration : IEntityTypeConfiguration<Hotel>
    {
        public void Configure(EntityTypeBuilder<Hotel> builder)
        {


        }
    }
}
