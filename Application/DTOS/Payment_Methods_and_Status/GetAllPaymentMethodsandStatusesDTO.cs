using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOS.Payment_Methods_and_Status
{
    public class GetAllPaymentMethodsandStatusesDTO
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public string? EmployeeAdded { get; set; }
    }
}
