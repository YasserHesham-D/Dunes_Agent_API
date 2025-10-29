using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Employee
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string FullName { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;

        public string PhoneNumber { get; set; } = null!;
        public DateTime JoinDate { get; set; }

        // public Position PositionRole { get; set; } Enum ? 
        public decimal SalaryType { get; set; }
        public bool IsFromUAE { get; set; }
        public decimal? SalaryValue { get; set; } = 0;
        // public ????? StaffVisaCount { get; set; }
        public decimal? CommissionRate { get; set; } = 0;

        public Guid HotelId { get; set; }
        public Guid EmployeeAddedId { get; set; }
        public Guid LocationId { get; set; }


    }
    public class EmployeeConfigration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {


        }
    }
}
