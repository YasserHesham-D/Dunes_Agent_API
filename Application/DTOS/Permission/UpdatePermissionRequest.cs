using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.Permission
{
    public class UpdatePermissionRequest
    {
        public string? Module { get; set; } 
        public string? Action { get; set; } 
    }
}
