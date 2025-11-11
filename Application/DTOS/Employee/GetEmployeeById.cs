using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.Employee
{
    public class GetEmployeeById
    {
        public string Id { get; set; }
        public string userName { get; set; }
        public string? phoneNumber { get; set; }
        public string? image {  get; set; }
        public string Email {  get; set; }
        public string position { get; set; }
        public decimal sallery {  get; set; }
        public decimal commissionRate { get; set; }
        public DateTime JoiningDate { get; set; }
        public string AreaOfLocation { get; set; }
        public string HotelName { get; set; }
        public bool  IsEmirate { get; set; }
        public bool HasControlOverSystem { get; set; }
        public decimal staffVisaCount { get; set; }

       // public ICollection<PermissionDto>? Permissions { get; set; } = new List<PermissionDto>();


    }
}
