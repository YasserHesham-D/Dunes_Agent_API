using Application.Dtos.Payment_Methods_and_Status;
using Application.DTOS.Payment_Methods_and_Status;
using Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.PaymentMethodService
{
    public interface IPaymentMethodService
    {
        public Task<DataResponseDTO> AddNewMethod(AddNewMethodandStatusDTO methodDTO);

        public Task<DataResponseDTO> UpdateMethod(Guid Id, UpdateMethodandStatusDTO updatemethodDTO);

        public Task<DataResponseDTO> DeleteMethod(Guid Id);

        public Task<IQueryable<GetAllPaymentMethodsandStatusesDTO>> GetAllMethods();

        public Task<Pagination<GetAllPaymentMethodsandStatusesDTO>> GetAllMethodsPaginated
       (string? Name, string? Employee, string SortColumn = "Name", bool IsAscending = false, int Page = 1, int PageSize = 20);


        public Task<GetPaymentStatusandMethodDetailsDTO> GetMethodDetails(Guid Id);

        public Task<Pagination<GetStatusandMethodBookingsCountDTO>> GetMethodBookingsCount(int Page = 1, int PageSize = 20);
    }
}
