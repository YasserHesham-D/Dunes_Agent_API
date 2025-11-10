using Application.Dtos;
using Application.Dtos.HotelsandLocations;
using Application.DTOS.Currency_Values;
using Domain.Interfaces.IModelsRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.CurrencyValuesService
{
    public interface ICurrencyValuesService
    {
        public Task<DataResponseDTO> AddNewCurrencyValue(AddNewCurrencyValueDTO currencyValueDTO);

        public Task<DataResponseDTO> UpdateCurrencyValue(Guid Id, UpdateCurrencyValueDTO updateCurrencyValueDTO);

        public Task<DataResponseDTO> DeleteCurrencyValue(Guid Id);

        public Task<Pagination<GetAllCurrencyValuesDTO>> GetAllCurrencyValuesPaginated(string? CurrencyFrom, string? CurrencyTo, string? Employee, string SortColumn = "Price", bool IsAscending = false, int Page = 1, int PageSize = 20);

        public Task<GetAllCurrencyValuesDTO> GetCurrencyValueDetails(Guid Id);
    }
}
