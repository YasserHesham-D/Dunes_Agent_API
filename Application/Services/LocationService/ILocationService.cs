
using Application.Dtos;
using Application.Dtos.HotelsandLocations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.LocationService
{
    public interface ILocationService
    {
        public Task<DataResponseDTO> AddNewLocation(AddNewDTO addNewLocationDTO);

        public Task<DataResponseDTO> UpdateLocation(Guid Id, UpdateDTO UpdatedLocationDto);

        public Task<DataResponseDTO> DeleteLocation(Guid Id);

        public Task<IQueryable<GetAllDTO>> GetAllLocations();

        public Task<Pagination<GetAllDTO>> GetAllLocationsPaginated(string? Name, string? Place, string? Employee, DateTime? DateFrom, DateTime? DateTo, string SortColumn = "EntryDate", bool IsAscending = false, int Page = 1, int PageSize = 20);

        public Task<GetDetailsDTO> GetLocationDetails(Guid Id);

        public Task<Dictionary<string, int>> GetEmployeesByLocationAsync();

        public Task<Pagination<GetEmployeesCountDTO>> GetLocationsEmployeesCount(int Page = 1, int PageSize = 20);
    }
}
