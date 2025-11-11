using Domain.Models.MTM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.ReceiptVoucher
{
    public class UpdateReceiptVoucherRequest
    {
        public string? GuestName { get; set; }
        public string? AgentName { get; set; }
        public string? NumOfRooms { get; set; }
        public DateTime PickupDate {  get; set; }
        public string? Note { get; set; }
        public Guid? CurrencyId { get; set; }
        public Guid? PaymentMethodId { get; set; }

        public ICollection<ReciptVoucherServices> RVServices { get; set; } = new List<ReciptVoucherServices>();

    }
}
