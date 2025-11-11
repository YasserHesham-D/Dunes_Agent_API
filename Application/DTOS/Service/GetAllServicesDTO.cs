using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.Service
{
    public class GetAllServicesDTO
    {
        public string EmployeeName  {  get; set; } = string.Empty;
        public string serviceName { get; set; } = string.Empty;
        public string ServiceDescription {  get; set; } = string.Empty;
        public int Duration { get; set; } 
        public string LocationName {  get; set; } = string.Empty;

        public ICollection<ServicelocationDTO> servicelocations { get; set; }

    }
    public class ServicelocationDTO
    {
        public decimal Adult { get; set; }
        public decimal Kid { get; set; }
        public decimal Child { get; set; }
    }
}
