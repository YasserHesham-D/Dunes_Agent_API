using Application.Dtos;
using Application.Dtos.HotelsandLocations;
using Application.Dtos.Payment_Methods_and_Status;
using Application.DTOS.Payment_Methods_and_Status;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.PaymentStatusService
{
    public interface IPaymentStatusService
    {
        public Task<DataResponseDTO> AddNewStatus(AddNewMethodandStatusDTO statusDTO);

        public Task<DataResponseDTO> UpdateStatus(Guid Id, UpdateMethodandStatusDTO updatestatusDTO);

        public Task<DataResponseDTO> DeleteStatus(Guid Id);

        public  Task<IQueryable<GetAllPaymentMethodsandStatusesDTO>> GetAllStatuses();

        public Task<Pagination<GetAllPaymentMethodsandStatusesDTO>> GetAllStatusPaginated
       (string? Name, string? Employee,string SortColumn = "Name", bool IsAscending = false, int Page = 1, int PageSize = 20);


        public Task<GetPaymentStatusandMethodDetailsDTO> GetStatusDetails(Guid Id);

        public Task<Pagination<GetStatusandMethodBookingsCountDTO>> GetStatusBookingsCount(int Page = 1, int PageSize = 20);
    }
}
