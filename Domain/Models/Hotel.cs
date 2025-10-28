using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Hotel
    {
        public Guid Id { get; set; } =Guid.NewGuid();
        public string Name { get; set; } = null!;
        public string Place { get; set; } = null!;

    }
}
