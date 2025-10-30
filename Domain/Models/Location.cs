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
    public class Location
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = null!;
        public string Place {  get; set; } = null!;
        public Guid EmployeeAddedId {  get; set; }

        public virtual Employee Employee { get; set; } = null!;

        public ICollection<LocationServices> Services { get; set; } = null!;

        public ICollection<BookingServices> BookingServices { get; set; } = null!;

        public ICollection<ReciptVoucherServices> VoucherServices { get; set; } = null!;

        public ICollection<Employee>? EmployeesBelong { get; set; }


    }
    public class LocationConfigration : IEntityTypeConfiguration<Location>
    {
        public void Configure(EntityTypeBuilder<Location> builder)
        {


        }
    }
}
