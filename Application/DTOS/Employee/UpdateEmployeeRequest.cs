using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.Employee
{
    public class UpdateEmployeeRequest
    {

            public string? UserName { get; set; }
            public string? PhoneNumber { get; set; }
            public decimal? SalaryValue { get; set; }
            public decimal? CommissionRate { get; set; }
            public bool? IsFromUAE { get; set; }
            public int? StaffVisaCount { get; set; }
            public DateTime? JoinDate { get; set; }
            public Guid LocationId { get; set; }
    }
}
