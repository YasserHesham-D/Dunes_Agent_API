using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.Service
{
    public class UpdateServiceRequest
    {
        [AllowNull]
        public int Duration { get; set; }
        [AllowNull]
        public string ServiceName { get; set; } = string.Empty;
        [AllowNull]
        public string ServiceDescription { get; set; } = string.Empty;
        [AllowNull]
        public TimeDuration TimeDuration { get; set; }
        [AllowNull]
        public string type { get; set; } = string.Empty;

        [AllowNull]
        public Guid? LocationServiceDelete {  get; set; }
        [AllowNull]
        public ICollection<LocationServiceAddDTO>? locationServcieAdds { get; set; } = new List<LocationServiceAddDTO>();
        [AllowNull]
        public ICollection<LocationServiceUpdateDTO>? locationServiceUpdates { get; set; }
    }

    public class LocationServiceAddDTO
    {
        public Guid LocationId { get; set; }

        public decimal KidPrice { get; set; }
        public decimal AdultPrice { get; set; }
        public decimal ChildPrice { get; set; }
    }
    public class LocationServiceUpdateDTO
    {
        [AllowNull]
        public Guid Id { get; set; }    
        [AllowNull]
        public Guid LocationId { get; set; }
        [AllowNull]
        public decimal KidPrice { get; set; }
        [AllowNull]
        public decimal AdultPrice { get; set; }
        [AllowNull]
        public decimal ChildPrice { get; set; }
    }
}
