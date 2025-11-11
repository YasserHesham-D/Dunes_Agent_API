using Application.Dtos;
using Application.Dtos.Permission;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.PermissionService
{
    public interface IPermissionServices
    {
        Task<DataResponseDTO> AddAsync(AddPermissionRequest request);
        Task<DataResponseDTO> DeleteAsync(Guid id);
        Task<IEnumerable<GetAllPermissionsDTO>> GetAllPermissions();
        Task<DataResponseDTO> PatchAsync(Guid id, UpdatePermissionRequest request);
        IQueryable<IEnumerable<string>> GetEmployeesWithPermissionAsync(Guid permissionId);

    }
}
