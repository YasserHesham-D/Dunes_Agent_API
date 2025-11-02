
using Application.Dtos;
using Application.Dtos.HotelsandLocations;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Application.Services.HotelService
{
    public interface IHotelService
    {
        public Task<DataResponseDTO> AddNewHotel(AddNewDTO addNewHotelDTO);

        public Task<DataResponseDTO> UpdateHotel(Guid Id, UpdateDTO UpdatedHotelDto);

        public Task<DataResponseDTO> DeleteHotel(Guid Id);

        public Task<IQueryable<GetAllDTO>> GetAllHotels();

        public Task<Pagination<GetAllDTO>> GetAllHotelsPaginated(string? Name, string? Place, string? Employee, DateTime? DateFrom, DateTime? DateTo, string SortColumn = "EntryDate", bool IsAscending = false, int Page = 1, int PageSize = 20);

        public Task<GetDetailsDTO> GetHotelDetails(Guid Id);

        public Task<Dictionary<string, int>> GetEmployeesByHotelAsync();

        public Task<Pagination<GetEmployeesCountDTO>> GetHotelEmployeesCount(int Page = 1, int PageSize = 20);
    }
}
