using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.Permission
{
    public class GetAllPermissionsDTO
    {
        public string Module { get; set; } = null!;
        public string Action { get; set; } = null!;
    }
}
