using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.Employee
{
    public class GetAllEmployees
    {
        public string Id { get; set; }
        
        public string? Image {  get; set; }
        public string FullName { get; set; } = null!;
        public string AddedBy { get; set; } = null!;
        public string Position { get; set; } = null!;
        
        public string Status { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public decimal Salary { get; set; }

        public decimal CommitionRate { get; set; }
        public bool EmployeeIsEmirates { get; set; }
        public decimal VisaCost { get; set; }

        public string AreaOfLocation { get; set; } = null!;
        public string Dues { get; set; } = null!;

        public DateTime JoiningDate { get; set; }
        public List<PermissionsDTO>? Permissions { get; set; } 
    }

    public class PermissionsDTO
    {
        public Guid Id { get; set; }
        public string Module { get; set; } = string.Empty;
        public string Action { get; set; } = string.Empty;
        public bool IsGranted { get; set; }
    }

}
