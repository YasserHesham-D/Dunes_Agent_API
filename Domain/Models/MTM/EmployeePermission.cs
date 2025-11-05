using Domain.Models.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.MTM
{
    public class EmployeePermission
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid EmployeeId { get; set; }
        public Employee? Employee { get; set; }

        public string Module { get; set; } = string.Empty;  // e.g., "Bookings"
        public string Action { get; set; } = string.Empty;  // e.g., "Add", "Edit", "Delete"

        public bool IsGranted { get; set; }

    }
}
