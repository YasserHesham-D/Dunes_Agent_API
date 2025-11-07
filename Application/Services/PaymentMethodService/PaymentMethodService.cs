using Application.Dtos;
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

namespace Application.Services.PaymentMethodService
{
    public class PaymentMethodService : IPaymentMethodService
    {
        private readonly IPaymentMethodRepo PaymentMethodRepo;
        //private readonly IAccountsRepo accountsRepo;
        private readonly IUnitOfWork unitOfWork;

        public PaymentMethodService(IPaymentMethodRepo PaymentMethodRepo , IUnitOfWork unitOfWork)
        {
            this.PaymentMethodRepo = PaymentMethodRepo;
            this.unitOfWork = unitOfWork;
            //this.accountsRepo = accountsRepo;
        }

        public async Task<DataResponseDTO> AddNewMethod(AddNewMethodandStatusDTO methodDTO)
        {
            try
            {
                if (methodDTO == null)
                {
                    return new DataResponseDTO
                    {
                        Success = false,
                        Message = "Data Is Required"
                    };

                }

                if (methodDTO.Name.Length<=2)
                {
                    return new DataResponseDTO
                    {
                        Success = false,
                        Message = "Payment Method Name Must Be More Than 2 Letters"
                    };
                }



                bool methodexists = await PaymentMethodRepo.AnyAsync(x => x.Name ==  methodDTO.Name);

                if (methodexists)
                {
                    return new DataResponseDTO
                    {
                        Success = false,
                        Message = "Payment Method Name Exists"
                    };
                }

                var method = new PaymentMethod
                {
                    Name = methodDTO.Name,

                };


                await PaymentMethodRepo.AddAsync(method);
                await unitOfWork.SaveChangesAsync();

                return new DataResponseDTO
                {
                    Success = true,
                    Message = "Payment Method Added Successfully",
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

        public async Task<DataResponseDTO> DeleteMethod(Guid Id)
        {
            try
            {
                var existedmethod = await PaymentMethodRepo.GetByIdAsync(Id);
                if (existedmethod == null)
                {
                    return new DataResponseDTO
                    {
                        Success = false,
                        Message = "Method not found"
                    };
                }

                await PaymentMethodRepo.DeleteAsync(existedmethod);
                await unitOfWork.SaveChangesAsync();

                return new DataResponseDTO
                {
                    Success = true,
                    Message = "Method Deleted successfully"
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

        public async Task<IQueryable<GetAllPaymentMethodsandStatusesDTO>> GetAllMethods()
        {
            var Methods = PaymentMethodRepo.GetAll().AsNoTracking().Select(x => new GetAllPaymentMethodsandStatusesDTO
            {
                Id = x.Id,
                Name = x.Name,
                EmployeeAdded =x.Employee.FullName
            });



            return Methods;
        }

        public async Task<Pagination<GetAllPaymentMethodsandStatusesDTO>> GetAllMethodsPaginated(string? Name, string? Employee, string SortColumn = "Name", bool IsAscending = false, int Page = 1, int PageSize = 20)
        {
            var methods = PaymentMethodRepo.GetAll();

            if (!string.IsNullOrEmpty(Name))
            {
                methods = methods.Where(x => x.Name.Contains(Name));
            }



            if (!string.IsNullOrEmpty(Employee))
            {
                methods = methods.Where(x => x.Employee.FullName.Contains(Employee));
            }




            if (IsAscending == false)
            {
                methods = methods.OrderByDescending(x => x.Name); // FIXED: added assignment
            }

            var result = PaymentMethodRepo.Sort(methods, SortColumn, IsAscending);


            var Methods = result.Select(m => new GetAllPaymentMethodsandStatusesDTO
            {
                Id = m.Id,
                Name = m.Name,
                EmployeeAdded = m.Employee.FullName
            });

            //// Check if any products exist before pagination
            //var totalCount = await HotelsDto.CountAsync();


            var paginatedmethods = await Pagination<GetAllPaymentMethodsandStatusesDTO>.CreateAsync(Methods, Page, PageSize);

            return paginatedmethods;
        }

        public async Task<Pagination<GetStatusandMethodBookingsCountDTO>> GetMethodBookingsCount(int Page = 1, int PageSize = 20)
        {
            var methods = PaymentMethodRepo.GetAll();

            var BookingsDTO = methods.Select(status => new GetStatusandMethodBookingsCountDTO
            {
                Id = status.Id,
                Name = status.Name,
                BookingsCount = status.Bookings.Count()

            });

            var paginatedBookings = await Pagination<GetStatusandMethodBookingsCountDTO>.CreateAsync(BookingsDTO, Page, PageSize);

            return paginatedBookings;
        }

        public async Task<GetPaymentStatusandMethodDetailsDTO> GetMethodDetails(Guid Id)
        {
            var existedmethod = PaymentMethodRepo.GetAll().AsNoTracking().Where(x => x.Id==Id);

            var method = existedmethod.Select(x => new GetPaymentStatusandMethodDetailsDTO
            {
                Name = x.Name,
                EmployeeAdded = x.Employee.FullName

            }).FirstOrDefault();



            return method;
        }

        public async Task<DataResponseDTO> UpdateMethod(Guid Id, UpdateMethodandStatusDTO updatemethodDTO)
        {
            try
            {
                var existedmethod = await PaymentMethodRepo.GetByIdAsync(Id);
                if (existedmethod == null)
                {
                    return new DataResponseDTO
                    {
                        Success = false,
                        Message = "Method not found"
                    };
                }

                if (updatemethodDTO == null)
                {
                    return new DataResponseDTO
                    {
                        Success = false,
                        Message = "No data provided"
                    };
                }

                // Validate Name if present
                if (updatemethodDTO.Name != null)
                {
                    if (updatemethodDTO.Name.Length <= 2)
                    {
                        return new DataResponseDTO
                        {
                            Success = false,
                            Message = "Methpd Name must be more than 2 letters"
                        };
                    }

                    bool nameExists = await PaymentMethodRepo.AnyAsync(x => x.Name == updatemethodDTO.Name && x.Id != Id);
                    if (nameExists)
                    {
                        return new DataResponseDTO
                        {
                            Success = false,
                            Message = "Method name already exists"
                        };
                    }

                    existedmethod.Name = updatemethodDTO.Name;
                }

                // Validate Place if present


                // If nothing changed
                if (updatemethodDTO.Name == null)
                {
                    return new DataResponseDTO
                    {
                        Success = true,
                        Message = "No changes to update"
                    };
                }

                await PaymentMethodRepo.UpdateAsync(existedmethod);
                await unitOfWork.SaveChangesAsync();

                return new DataResponseDTO
                {
                    Success = true,
                    Message = "Method updated successfully"
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
    }
}
