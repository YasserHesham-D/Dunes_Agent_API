using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOS.Hotels
{
    public class AddNewHotelDTO
    {
        public string Name { get; set; } = null!;
        public string Place { get; set; } = null!;

    }
}
