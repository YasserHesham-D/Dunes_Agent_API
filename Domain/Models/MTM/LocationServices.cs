using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.MTM
{
    public class LocationServices
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid ServiceId { get; set; }

        public virtual Service Service { get; set; } = null!;
        public Guid LocationId { get; set; }

        public virtual Location Location { get; set; } = null!;
        public decimal KidsPrice { get; set; }
        public decimal ChildsPrice { get; set; }
        public decimal AdultsPrice { get; set; }

    }
}
