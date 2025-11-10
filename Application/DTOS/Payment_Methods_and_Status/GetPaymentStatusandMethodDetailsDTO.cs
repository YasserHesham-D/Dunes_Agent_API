using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOS.Payment_Methods_and_Status
{
    public class GetPaymentStatusandMethodDetailsDTO
    {
       
        public string Name { get; set; } = null!;

        public string? EmployeeAdded { get; set; }
    }
}
