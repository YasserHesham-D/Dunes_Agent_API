using Application.Dtos;
using Application.Dtos.ReceiptVoucher;
using Application.Services.NotificationService;
using Domain.Interfaces.IModelsRepo;
using Domain.Interfaces.IRepository;
using Domain.Interfaces.IUnitOfWork;
using Domain.Models;
using Domain.Models.Accounts;
using Domain.Models.MTM;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.ReceiptVoucher
{
    //  total price calculation on adding new voucher not yet implemented
    //  MTM patch not yet implemented


    public class ReceiptVoucherService(IAccountsRepo accountsRepo,INotificationService notificationService,IReceiptVoucherRepo receiptVoucherRepo,IMTMRepo mTMRepo, IUnitOfWork unitOfWork, UserManager<Employee> userManager) : IReceiptVoucherService
    {
        public async Task<bool> CreateReceiptVoucherAsync(string userId, AddNewReceiptVoucherRequest request)
        {
            if (request == null)
                return false;

            

            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
                return false;

            var voucher = new ReciptVoucher
            {
                GuestName = request.GuestName,
                AgentName = request.AgentName,
                NumberOfRooms = request.NumOfRooms,
                Notes = request.Note,
                CurrencyId = request.Currency,
                PaymentMethodId = request.PaymentMethod,

                EmployeeAddedId = userId,


                Services = request.RVServices.Select(s => new ReciptVoucherServices
                {

                    LocationId = s.LocationId,
                    ServiceId = s.ServiceId,
                    KidsCount = s.KidsCount,
                    ChildsCount = s.ChildsCount,
                    AdultsCount = s.AdultsCount

                }).ToList()
            };


            var totalprice = 0;
            voucher.TotalPrice = totalprice;

            
            await unitOfWork.BeginTransactionAsync();
            try
            {

                await receiptVoucherRepo.AddAsync(voucher);

                var message = $"Employee {user.UserName} Made A New ReceiptVoucher " ;
                var pname =  "New ReceiptVoucher";

                var not = new Notification
                {
                    EmployeeId = userId,
                    ProcessId = voucher.Id,
                    Message = message,
                    ProcessName = pname,
                    

                };
                await notificationService.CreateAsync(not);

                await unitOfWork.CommitAsync();
                unitOfWork.Dispose();
                return true;

            }
            catch (Exception ex)
            {
                
                await unitOfWork.RollbackAsync();
                unitOfWork.Dispose();
                return false;

            }

        }

        public async Task<Pagination<GetAllReceiptVoucherDTO>> GetAllPaginatedAsync(
            string? guestName, string? agentName,string? LocationName,string?ServiceName, 
            string sortColumn = "CreatedAt", bool isAscending = false, int page = 1, int pageSize = 6)
        {
            var query = receiptVoucherRepo.GetAll();


            if (!string.IsNullOrEmpty(guestName))
                query = query.Where(x => x.GuestName.Contains(guestName));

            if (!string.IsNullOrEmpty(LocationName))
                query = query.Where(x => x.Services.Select(x => x.Location.Name).Contains(LocationName));

            if (!string.IsNullOrEmpty(ServiceName))
                query = query.Where(x => x.Services.Select(x => x.Service.ServiceName).Contains(ServiceName));

            if (!string.IsNullOrEmpty(agentName))
                query = query.Where(x => x.AgentName.Contains(agentName));

            query = sortColumn switch
            {
                "GuestName" => isAscending ? query.OrderBy(x => x.GuestName) : query.OrderByDescending(x => x.GuestName),
                "AgentName" => isAscending ? query.OrderBy(x => x.AgentName) : query.OrderByDescending(x => x.AgentName),
                "CreatedAt" => isAscending ? query.OrderBy(x => x.Date) : query.OrderByDescending(x => x.Date),
                _ => query.OrderByDescending(x => x.Date)
            };

            var dtoQuery = query.Select(x => new GetAllReceiptVoucherDTO
            {
                Id = x.Id,
                GuestName = x.GuestName,
                AgentName = x.AgentName,
                NumOfRooms = x.NumberOfRooms,
                Note = x.Notes,
                CurrencyName = x.Currency.Name,
                PaymentMethodName = x.PaymentMethod.Name,
                CreatedAt = x.Date
                ,servicesDtos = x.Services.Select(x => new RvServicesDto
                {
                    AdultCount = x.AdultsCount,
                    childCount = x.ChildsCount,
                    kidCount = x.KidsCount,
                    LocationName = x.Location.Name,
                    serviceName = x.Service.ServiceName
                   
                }).ToList()
            });

            var paginated = await Pagination<GetAllReceiptVoucherDTO>.CreateAsync(dtoQuery, page, pageSize);

            return paginated;
        }

        public async Task<GetAllReceiptVoucherDTO?> GetByIdAsync(Guid id)
        {
            var voucher = await receiptVoucherRepo.GetAll()
                .Include(x => x.Currency)
                .Include(x => x.PaymentMethod)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (voucher == null)
                return null;

            return new GetAllReceiptVoucherDTO
            {
                Id = voucher.Id,
                GuestName = voucher.GuestName,
                AgentName = voucher.AgentName,
                NumOfRooms = voucher.NumberOfRooms,
                Note = voucher.Notes,
                CurrencyName = voucher.Currency.Name,
                PaymentMethodName = voucher.PaymentMethod.Name,
                CreatedAt = voucher.Date

                ,servicesDtos= voucher.Services.Select(x => new RvServicesDto
                {
                    AdultCount = x.AdultsCount,
                    childCount = x.ChildsCount,
                    kidCount = x.KidsCount,
                    LocationName = x.Location.Name,
                    serviceName = x.Service.ServiceName

                }).ToList()
            };
            
        }

        public async Task<bool> PatchAsync(Guid id, UpdateReceiptVoucherRequest request)
        {
            var voucher = await receiptVoucherRepo.GetByIdAsync(id);
            if (voucher == null)
                return false;

            if (!string.IsNullOrEmpty(request.GuestName))
                voucher.GuestName = request.GuestName;

            if (!string.IsNullOrEmpty(request.AgentName))
                voucher.AgentName = request.AgentName;

            if (!string.IsNullOrEmpty(request.NumOfRooms))
                voucher.NumberOfRooms = request.NumOfRooms;

            if (!string.IsNullOrEmpty(request.Note))
                voucher.Notes = request.Note;

            if (request.CurrencyId.HasValue)
                voucher.CurrencyId = request.CurrencyId.Value;

            if (request.PaymentMethodId.HasValue)
                voucher.PaymentMethodId = request.PaymentMethodId.Value;

           

            await receiptVoucherRepo.UpdateAsync(voucher);
            await receiptVoucherRepo.SaveChangesAsync();

            return true;
        }
    }
}
