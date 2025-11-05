using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.Service
{
    public class AddNewServiceRequest
    {
        public int Duration { get; set; }
        public string ServiceName { get; set; } = null!;
        public string ServiceDescription { get; set; } = null!;
        public TimeDuration TimeDuration { get; set; }

        public ICollection<LocationServiceDTO> locationServices { get; set; }

    }
    public class LocationServiceDTO
    {
        public Guid LocationId { get; set; }

        public decimal KidPrice { get; set; }
        public decimal AdultPrice { get; set; }
        public decimal ChildPrice { get; set; }
    }
}
