using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.HotelsandLocations
{
    public class GetAllDTO
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public string Place { get; set; } = null!;

        public DateTime EntryDate { get; set; } 

        public string? EmployeeAdded { get; set; }
    }
}
