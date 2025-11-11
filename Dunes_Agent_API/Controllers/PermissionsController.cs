using Application.Dtos.Permission;
using Application.Services.PermissionService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionsController(IPermissionServices permissionServices) : ControllerBase
    {
        [HttpPost("[Action]")]
        public async Task<IActionResult> AddPermission([FromBody] AddPermissionRequest request)
        {
            var result = await permissionServices.AddAsync(request);

            return Ok(result);
        }

        [HttpGet("[Action]")]
        public async Task<IActionResult> GetAllPermissions()
        {
            var result = await permissionServices.GetAllPermissions();
            

            return Ok(result);
        }

        [HttpGet("[Action]/{id}")]
        public async Task<IActionResult> GetEmployeesHasPermission(Guid id)
        {
            var result =  permissionServices.GetEmployeesWithPermissionAsync(id);

            return Ok(result);
        }

        [HttpPatch("[Action]/{id}")]
        public async Task<IActionResult> Patch(Guid id, [FromBody] UpdatePermissionRequest request)
        {
            var updated = await permissionServices.PatchAsync(id, request);

            return Ok(updated);
        }
    }
}
