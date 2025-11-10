using Application.Dtos;
using Application.Dtos.Currency;
using Application.Dtos.HotelsandLocations;
using Application.DTOS.Currency;
using Application.DTOS.Payment_Methods_and_Status;
using Application.DTOS.Payment_Methods_and_Status.Status;
using Domain.Interfaces.IModelsRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.CurrencyService
{
    public interface ICurrencyService
    {
         public Task<DataResponseDTO> AddNewCurrency(string Id, AddNewCurrencyRequest request);

        public Task<DataResponseDTO> UpdateCurrency(Guid Id, UpdateCurrencyDTO updateCurrencyDTO);


        public Task<DataResponseDTO> DeleteCurrency(Guid Id);


        public Task<IQueryable<GetAllPaymentMethodsandStatusesDTO>> GetAllCurrencices();

        public Task<GetPaymentStatusandMethodDetailsDTO> GetCurrencyDetails(Guid Id);

        public  Task<Pagination<GetMethodOpreationsCountDTO>> GetCurrencyOpreationsCount(int Page = 1, int PageSize = 20);

        public Task<Pagination<GetCurrencyBookingsCountDTO>> GetCurrencyBookingsCount(int Page = 1, int PageSize = 20);

        public Task<Pagination<GetCurrencyVouchersCountDTO>> GetCurrencyVouchersCount(int Page = 1, int PageSize = 20);






        public Task<Pagination<GetAllPaymentMethodsandStatusesDTO>> GetAllCurrencicesPaginated(string? Name, string? Employee, string SortColumn = "Name", bool IsAscending = false, int Page = 1, int PageSize = 20);

    }
}
