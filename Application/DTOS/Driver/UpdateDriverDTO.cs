using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOS.Driver
{
    public class UpdateDriverDTO
    {
       
        [MinLength(2, ErrorMessage = "The Driver Name must be more than 2 letters.")]
        public string? Name { get; set; } = null!;

        
        [Phone(ErrorMessage = "Invalid phone number format.")]
        public string? PhoneNumber { get; set; } = null!;

        
        [MinLength(2, ErrorMessage = "The Car Number must be more than 2 letters.")]
        public string? CarNumber { get; set; } = null!;

       
        [MinLength(2, ErrorMessage = "The City of Work must be more than 2 letters.")]
        public string? PlaceOfWork { get; set; } = null!;
    }
}
