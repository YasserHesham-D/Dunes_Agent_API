using Application.Dtos;
using Application.Dtos.ReceiptVoucher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.ReceiptVoucher
{
    public interface IReceiptVoucherService
    {
        Task<bool> CreateReceiptVoucherAsync(string userId, AddNewReceiptVoucherRequest request);
        Task<Pagination<GetAllReceiptVoucherDTO>> GetAllPaginatedAsync(
            string? guestName, string? agentName, string? LocationName, string? ServiceName,
            string sortColumn = "CreatedAt", bool isAscending = false,
            int page = 1, int pageSize = 6);

        Task<GetAllReceiptVoucherDTO?> GetByIdAsync(Guid id);
        Task<bool> PatchAsync(Guid id, UpdateReceiptVoucherRequest request);
    }
}
