using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOS.Hotels
{
    public class GetHotelDetailsDTO
    {
        public string Name { get; set; } = null!;

        public string Place { get; set; } = null!;

        public DateTime? EntryDate { get; set; }

        public string? EmployeeAdded { get; set; }
    }
}
