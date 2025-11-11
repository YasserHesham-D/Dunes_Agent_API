using Application.Services.PermissionService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionsController(IPermissionServices permissionServices) : ControllerBase
    {
    }
}
