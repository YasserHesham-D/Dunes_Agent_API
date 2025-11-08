using Application.Dtos;
using Application.Dtos.HotelsandLocations;
using Application.Dtos.Payment_Methods_and_Status;
using Application.DTOS.Payment_Methods_and_Status;
using Domain.Interfaces.IModelsRepo;
using Domain.Interfaces.IUnitOfWork;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Application.Services.PaymentStatusService
{
    public class PaymentStatusService : IPaymentStatusService
    {

        private readonly IPaymentStatusRepo PaymentStatusRepo;
        private readonly IAccountsRepo accountsRepo;
        private readonly IUnitOfWork unitOfWork;

        public PaymentStatusService(IPaymentStatusRepo PaymentStatusRepo, IUnitOfWork unitOfWork , IAccountsRepo accountsRepo)
        {
            this.PaymentStatusRepo = PaymentStatusRepo;
            this.unitOfWork = unitOfWork;
            this.accountsRepo = accountsRepo;
        }
        public async Task<DataResponseDTO> AddNewStatus(AddNewMethodandStatusDTO statusDTO)
        {
            try
            {
                if (statusDTO == null)
                {
                    return new DataResponseDTO
                    {
                        Success = false,
                        Message = "Data Is Required"
                    };

                }

                if (statusDTO.Name.Length<=2)
                {
                    return new DataResponseDTO
                    {
                        Success = false,
                        Message = "Payment Status Name Must Be More Than 2 Letters"
                    };
                }

               

                bool statusexists = await PaymentStatusRepo.AnyAsync(x => x.Name ==  statusDTO.Name);

                if (statusexists)
                {
                    return new DataResponseDTO
                    {
                        Success = false,
                        Message = "Payment Status Name Exists"
                    };
                }

                var status = new PaymentStatus
                {
                    Name = statusDTO.Name,
                   
                };


                await PaymentStatusRepo.AddAsync(status);
                await unitOfWork.SaveChangesAsync();

                return new DataResponseDTO
                {
                    Success = true,
                    Message = "Payment Status Added Successfully",
                };




            }
            catch (Exception ex)
            {
                return new DataResponseDTO
                {
                    Success = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<DataResponseDTO> DeleteStatus(Guid Id)
        {
            try
            {
                var existedstatus = await PaymentStatusRepo.GetByIdAsync(Id);
                if (existedstatus == null)
                {
                    return new DataResponseDTO
                    {
                        Success = false,
                        Message = "Status not found"
                    };
                }

                await PaymentStatusRepo.DeleteAsync(existedstatus);
                await unitOfWork.SaveChangesAsync();

                return new DataResponseDTO
                {
                    Success = true,
                    Message = "Status Deleted successfully"
                };
            }
            catch (Exception ex)
            {
                return new DataResponseDTO
                {
                    Success = false,
                    Message = ex.InnerException?.Message ?? ex.Message
                };
            }
        }

        public async Task<IQueryable<GetAllPaymentMethodsandStatusesDTO>> GetAllStatuses()
        {
            var statuses = PaymentStatusRepo.GetAll().AsNoTracking().Select(x => new GetAllPaymentMethodsandStatusesDTO
            {
                Id = x.Id,
                Name = x.Name,
                EmployeeAdded =x.Employee.UserName
            });



            return statuses;
        }

        public async Task<Pagination<GetAllPaymentMethodsandStatusesDTO>> GetAllStatusPaginated(string? Name, string? Employee, string SortColumn = "Name", bool IsAscending = false, int Page = 1, int PageSize = 20)
        {
            var statuses = PaymentStatusRepo.GetAll();

            if (!string.IsNullOrEmpty(Name))
            {
                statuses = statuses.Where(x => x.Name.Contains(Name));
            }

           

            if (!string.IsNullOrEmpty(Employee))
            {
                statuses = statuses.Where(x => x.Employee.UserName.Contains(Employee));
            }


           

            if (IsAscending == false)
            {
                statuses = statuses.OrderByDescending(x => x.Name); // FIXED: added assignment
            }

            var result = PaymentStatusRepo.Sort(statuses, SortColumn, IsAscending);


            var Statuses = result.Select(hotel => new GetAllPaymentMethodsandStatusesDTO
            {
                Id = hotel.Id,
                Name = hotel.Name,
                EmployeeAdded = hotel.Employee.UserName
            });

            //// Check if any products exist before pagination
            //var totalCount = await HotelsDto.CountAsync();


            var paginatedstatuses = await Pagination<GetAllPaymentMethodsandStatusesDTO>.CreateAsync(Statuses, Page, PageSize);

            return paginatedstatuses;
        }

        public async Task<DataResponseDTO> UpdateStatus(Guid Id, UpdateMethodandStatusDTO updatestatusDTO)
        {
            try
            {
                var existedstatus = await PaymentStatusRepo.GetByIdAsync(Id);
                if (existedstatus == null)
                {
                    return new DataResponseDTO
                    {
                        Success = false,
                        Message = "Status not found"
                    };
                }

                if (updatestatusDTO == null)
                {
                    return new DataResponseDTO
                    {
                        Success = false,
                        Message = "No data provided"
                    };
                }

                // Validate Name if present
                if (updatestatusDTO.Name != null)
                {
                    if (updatestatusDTO.Name.Length <= 2)
                    {
                        return new DataResponseDTO
                        {
                            Success = false,
                            Message = "Status Name must be more than 2 letters"
                        };
                    }

                    bool nameExists = await PaymentStatusRepo.AnyAsync(x => x.Name == updatestatusDTO.Name && x.Id != Id);
                    if (nameExists)
                    {
                        return new DataResponseDTO
                        {
                            Success = false,
                            Message = "Status name already exists"
                        };
                    }

                    existedstatus.Name = updatestatusDTO.Name;
                }

                // Validate Place if present
                

                // If nothing changed
                if (updatestatusDTO.Name == null)
                {
                    return new DataResponseDTO
                    {
                        Success = true,
                        Message = "No changes to update"
                    };
                }

                await PaymentStatusRepo.UpdateAsync(existedstatus);
                await unitOfWork.SaveChangesAsync();

                return new DataResponseDTO
                {
                    Success = true,
                    Message = "Status updated successfully"
                };
            }
            catch (Exception ex)
            {
                return new DataResponseDTO
                {
                    Success = false,
                    Message = ex.InnerException?.Message ?? ex.Message
                };
            }
        }

        public async Task<GetPaymentStatusandMethodDetailsDTO> GetStatusDetails(Guid Id)
        {
            var existedstatus = PaymentStatusRepo.GetAll().AsNoTracking().Where(x => x.Id==Id);

            var status = existedstatus.Select(x => new GetPaymentStatusandMethodDetailsDTO
            {
                Name = x.Name,
                EmployeeAdded = x.Employee.UserName

            }).FirstOrDefault();



            return status;



        }

        public async Task<Pagination<GetStatusandMethodBookingsCountDTO>> GetStatusBookingsCount(int Page = 1, int PageSize = 20)
        {
            var statuses = PaymentStatusRepo.GetAll();

            var BookingsDTO = statuses.Select(status => new GetStatusandMethodBookingsCountDTO
            {
                Id = status.Id,
                Name = status.Name,
                BookingsCount = status.Bookings.Count(),
                TotalMoney = status.Bookings.Sum(x => x.TotalPriceAfterDiscount)

            });

            var paginatedBookings = await Pagination<GetStatusandMethodBookingsCountDTO>.CreateAsync(BookingsDTO, Page, PageSize);

            return paginatedBookings;
        }
    }
}
