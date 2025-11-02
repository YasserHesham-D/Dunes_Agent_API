using Application.DTOS;
using Application.DTOS.Hotels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Domain.Interfaces.IServices
{
    public interface IHotelService
    {
        public Task<DataResponseDTO> AddNewHotel(AddNewHotelDTO addNewHotelDTO);

        public Task<DataResponseDTO> UpdateHotel(Guid Id , UpdateHotelDTO UpdatedHotelDto);

        public Task<DataResponseDTO> DeleteHotel(Guid Id);

        public Task<IQueryable<GetAllHotelsDTO>> GetAllHotels();

        public Task<Pagination<GetAllHotelsDTO>> GetAllHotelsPaginated(string? Name, string? Place, string? Employee, DateTime? DateFrom, DateTime? DateTo, string SortColumn = "EntryDate", bool IsAscending = false, int Page = 1, int PageSize = 20);

        public Task<GetHotelDetailsDTO> GetHotelDetails(Guid Id);

        //public Task<Dictionary<string, int>> GetEmployeesByHotelAsync();

        public Task<Pagination<GetHotelEmployeesCountDTO>> GetHotelEmployeesCount(int Page = 1, int PageSize = 20);
    }
}
