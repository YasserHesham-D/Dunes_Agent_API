using Application.Dtos;
using Application.Dtos.HotelsandLocations;
using Application.DTOS.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.DriverService
{
    public interface IDriverService
    {
        public Task<DataResponseDTO> AddNewDriver(AddNewDriverDTO newDriverDTO);

        public Task<DataResponseDTO> UpdateDriver(Guid Id,UpdateDriverDTO updateDriverDTO);

        public Task<DataResponseDTO> DeleteDriver(Guid Id);

        public Task<IQueryable<GetAllDriversDTO>> GetAllDrivers();

        public Task<GetAllDriversDTO> GetDriverDetails(Guid Id);


        public Task<Pagination<GetDriverBookingsDTO>> GetDriverBookingsCount(int Page = 1, int PageSize = 20);


        public Task<Pagination<GetAllDriversDTO>> GetAllDriversPaginated
       (string? Name, string? CarNumber, string? PhoneNumber, string? Place, string? Employee, DateTime? DateFrom, DateTime? DateTo, string SortColumn = "DateOfAdd", bool IsAscending = false, int Page = 1, int PageSize = 20);
    }
}
