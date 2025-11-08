using Domain.Models;
using Domain.Models.MTM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.ReceiptVoucher
{
    public class AddNewReceiptVoucherRequest
    {
        public string GuestName { get; set; } = null!;
        public string AgentName { get; set; } = null!;
        public string NumOfRooms { get; set; } = null!;
        public string? Note { get; set; }
        public Guid Currency {  get; set; }
        public Guid PaymentMethod { get; set; } 

        public ICollection<ReciptVoucherServicesDTO> RVServices { get; set; } = new List<ReciptVoucherServicesDTO>();
        
    }
    public class ReciptVoucherServicesDTO
    {
        public Guid LocationId {  get; set; }
        public Guid ServiceID { get; set; }
        public int KidCount { get; set;}
        public int ChildCount { get; set; }
        public int AdultCount { get; set; }

    }
}
