using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.HotelsandLocations
{
    public class AddNewDTO
    {

        [Required(ErrorMessage = "Name is Required.")]
        [MinLength(3,ErrorMessage ="The Name Must Be 3 Or More Letters")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Name is Required.")]
        [MinLength(3, ErrorMessage = "The  Place Must Be 3 Or More Letters")]
        public string Place { get; set; } = null!;

    }
}
