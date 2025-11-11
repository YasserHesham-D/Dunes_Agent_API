using Application.Dtos;
using Application.Dtos.Permission;
using Domain.Interfaces.IModelsRepo;
using Domain.Interfaces.IUnitOfWork;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Application.Services.PermissionService
{
    public class PermissionService(IPermissionRepo permissionRepo,IUnitOfWork unitOfWork) : IPermissionServices
    {
        public async Task<DataResponseDTO> AddAsync(AddPermissionRequest request)
        {
            var existM = await permissionRepo.AnyAsync(x => x.Module.Equals(request.Module));
            var existA = await permissionRepo.AnyAsync(x => x.Action.Equals(request.Action));

            if (existM && existA)
            {
                return new DataResponseDTO
                {
                    Success = false,
                    Message = "This Action Exist In This Module"
                };
            }

            var newpermission = new Permission
            {
                Action = request.Action,
                Module = request.Module,
            };

            await permissionRepo.AddAsync(newpermission);
            var result = await unitOfWork.SaveChangesAsync();

            if (result != 1)
            {
                return new DataResponseDTO
                {
                    Success = false,
                    Message = "Error on Saving To Db"
                };
            }

            return new DataResponseDTO
            {
                Success = true,
                Message = "Permission Added Succesfully ."
            };


        }

        public async Task<DataResponseDTO> DeleteAsync(Guid id)
        {
            var exist = await permissionRepo.GetByIdAsync(id);

            if (exist == null) 
            {
                return new DataResponseDTO
                {
                    Success = false,
                    Message = "Permission Not Found"
                };
            }

            exist.IsDeleted = true;
            var result = await unitOfWork.SaveChangesAsync();

            if (result != 1)
            {
                return new DataResponseDTO
                {
                    Success = false,
                    Message = "Error on Saving To Db"
                };
            }

            return new DataResponseDTO
            {
                Success = true,
                Message = "Permission Deleted Succesfully."
            };

        }

        public  async Task<IEnumerable<GetAllPermissionsDTO>> GetAllPermissions()
        {
            var permission = await permissionRepo.GetAllAsync();

            var all = permission.Select(x => new GetAllPermissionsDTO
            {
                Module = x.Module,
                Action = x.Action,
            });

            return all;
        }


        public IQueryable<IEnumerable<string>> GetEmployeesWithPermissionAsync(Guid permissionId)
        {
            var permissions = permissionRepo.GetAll()
                .Include(x => x.employees).Where(x => x.id == permissionId).Select(x => x.employees.Select(x => x.Employee.UserName));

            return permissions;

        }

        public async Task<DataResponseDTO> PatchAsync(Guid id, UpdatePermissionRequest request)
        {
            var Exists = await permissionRepo.GetByIdAsync(id);

            if(Exists == null)
            {
                return new DataResponseDTO
                {
                    Success = false,
                    Message = "Permission Not Found"
                };
            } 

            if(request == null)
            {
                return new DataResponseDTO
                {
                    Success = false,
                    Message = "Invalid Request"
                };
            }

            if(!string.IsNullOrEmpty(request.Module))
            {
                if ( await permissionRepo.AnyAsync(x => x.Module == request.Module))
                    return new DataResponseDTO
                    {
                        Success = false,
                        Message = "Module Already Exist"
                    };
                Exists.Module = request.Module;
            }

            if (!string.IsNullOrEmpty(request.Action))
            {
                if(await permissionRepo.AnyAsync(x => x.Action.Equals(request.Action) ) && await permissionRepo.AnyAsync(x => x.Module.Equals(request.Module)) )
                        return new DataResponseDTO
                        {
                            Success = false,
                            Message = "Action Already Exist With This Module"
                        };
                Exists.Action = request.Action;
            }

            var result = await unitOfWork.SaveChangesAsync();

            if (result == 1)
                return new DataResponseDTO
                {
                    Success = true,
                    Message = "Permission Updated Succesfully"
                };

            return new DataResponseDTO
            {
                Success = false,
                Message = "Error On Updating DB"
            };
        }

    }
}
