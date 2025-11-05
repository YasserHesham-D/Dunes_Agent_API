using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.Service
{
    public class UpdateServiceRequest
    {
        public int? Duration { get; set; }
        public string ServiceName { get; set; } = string.Empty;
        public string ServiceDescription { get; set; } =string.Empty;
        public TimeDuration? TimeDuration { get; set; }
        public Guid? LocationId { get; set; }
        public decimal? AdultsPrice { get; set; }
        public decimal? KidsPrice { get; set; }
        public decimal? ChildPrice { get; set; }
    }
}
