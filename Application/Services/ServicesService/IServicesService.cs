using Application.Dtos;
using Application.Dtos.Service;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.ServicesService
{
    public interface IServicesService
    {
        Task<bool> AddNewServiceAsync(AddNewServiceRequest request, string UserId);
        Task<Pagination<GetAllServicesDTO>> GetAllServicesPaginated(string? empname, string? serviceName, string? locationname, int? duration, string sortColumn, bool isAscending, int page, int pageSize,string columnName);
        Task<bool> DeleteServiceAsync(Guid id);
        Task<bool> PatchService(Guid id, UpdateServiceRequest request);
    }
}
