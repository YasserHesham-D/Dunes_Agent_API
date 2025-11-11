using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOS.Driver
{
    public class GetAllDriversDTO
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public string PhoneNumber { get; set; } = null!;

        public DateTime EntryDate { get; set; }

        public string CarNumber { get; set; } = null!;

        public string PlaceOfWork { get; set; } = null!;

        public string? EmployeeAdded { get; set; }
    }
}
