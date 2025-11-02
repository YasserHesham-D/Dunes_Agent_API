using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOS.Hotels
{
    public class GetHotelEmployeesCountDTO
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public int? EmployeesCount { get; set; }
    }
}
