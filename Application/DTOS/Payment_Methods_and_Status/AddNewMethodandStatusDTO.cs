using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.Payment_Methods_and_Status
{
    public class AddNewMethodandStatusDTO
    {
        [Required]
        [MinLength(3, ErrorMessage = "The Name Must Be 3 Or More Letters")]
        public string Name { get; set; } = null!;
    }
}
