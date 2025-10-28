using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class ReciptVoucher
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string? Notes { get; set; }
        public bool IsConfirmed { get; set; }
        // public ???? Room { get; set; }
        public string GuestName { get; set; } = null!;
        public DateTime Date { get; set; }
    }
}
