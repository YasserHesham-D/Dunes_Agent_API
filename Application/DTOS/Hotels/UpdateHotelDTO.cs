using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOS.Hotels
{
    public class UpdateHotelDTO
    {
        
        [MinLength(3, ErrorMessage = "The Hotel Name Must Be 3 Or More Letters")]
        public string? Name { get; set; } 

        [MinLength(3, ErrorMessage = "The Hotel Place Must Be 3 Or More Letters")]
        public string? Place { get; set; }
    }
}
