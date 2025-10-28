using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Service
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime Duration { get; set; }
        public DateTime EntryDate { get; set; }
        public string Name { get; set; } = null!;

        // On Relation Attributes

        public int KidsCount { get; set; }
        public decimal KidsTotalPrice { get; set; }
        public int ChildsCount { get; set; }
        public decimal ChildsTotalPrice { get; set; }
        public int AdultsCount { get; set; }
        public decimal AdultsTotalPrice { get; set; }

        public decimal TotalPrice { get; set; }
    }

}
