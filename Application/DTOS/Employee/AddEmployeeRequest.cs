using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models.Accounts;
using Domain.Enums;

namespace Application.Dtos.Employee
{
    public class AddEmployeeRequest
    {
        public string Name { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; }
        public string Position { get; set; } = null!;

        public SalaryType SalaryType { get; set; } 

        public decimal CommissionRate { get; set; }

        public bool IsTheEmployeeEmirate { get; set; }

        public decimal StaffVisaCost { get; set; }
        public bool HasControlSystemAccess { get; set; }

        public Guid AreaOfLocationId { get; set; }
        public Guid HotelId { get; set; } 

        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        public ICollection<PermissionDto>? Permissions { get; set; } = new List<PermissionDto>();
    }
    public class PermissionDto
    {
        public string Module { get; set; } = string.Empty;
        public string Action { get; set; } = string.Empty;
        public bool IsGranted { get; set; }
    }
}
