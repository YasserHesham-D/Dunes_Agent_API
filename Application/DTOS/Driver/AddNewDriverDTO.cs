using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOS.Driver
{
    public class AddNewDriverDTO
    {
        [Required(ErrorMessage = "Driver Name is required.")]
        [MinLength(2, ErrorMessage = "The Driver Name must be more than 2 letters.")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Phone Number is required.")]
        [Phone(ErrorMessage = "Invalid phone number format.")]
        public string PhoneNumber { get; set; } = null!;

        [Required(ErrorMessage = "Car Number is required.")]
        [MinLength(2, ErrorMessage = "The Car Number must be more than 2 letters.")]
        public string CarNumber { get; set; } = null!;

        [Required(ErrorMessage = "City of Work is required.")]
        [MinLength(2, ErrorMessage = "The City of Work must be more than 2 letters.")]
        public string PlaceOfWork { get; set; } = null!;
    }
}
