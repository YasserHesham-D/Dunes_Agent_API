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
        public DateTime EntryDate { get; set; } = DateTime.Now;
        public Guid EmployeeAddedId {  get; set; }

    }
    public class HotelConfigration : IEntityTypeConfiguration<Hotel>
    {
        public void Configure(EntityTypeBuilder<Hotel> builder)
        {


        }
    }
}
