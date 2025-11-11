using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.ReceiptVoucher
{

    public class GetAllReceiptVoucherDTO
    {
        public int Id { get; set; }
        public string GuestName { get; set; } = null!;
        public string AgentName { get; set; } = null!;
        public string NumOfRooms { get; set; } = null!;
        public DateTime pickupDate { get; set; }
        public string? Note { get; set; }
        public string CurrencyName { get; set; } = null!;
        public string PaymentMethodName { get; set; } = null!;
        public DateTime CreatedAt { get; set; }

        public ICollection<RvServicesDto> servicesDtos { get; set; } = new List<RvServicesDto>();

    }
    public class RvServicesDto
    {
        public string LocationName { get; set; }
        public string serviceName { get; set; }

        public int kidCount { get; set; }
        public int childCount { get; set; }
        public int AdultCount { get; set; }


    }
    
}
