using Application.Dtos;
using Application.Dtos.Currency;
using Application.DTOS.Currency;
using Application.DTOS.Payment_Methods_and_Status;
using Application.DTOS.Payment_Methods_and_Status.Status;
using Domain.Interfaces.IModelsRepo;
using Domain.Interfaces.IUnitOfWork;
using Domain.Models;
using Domain.Models.Accounts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.CurrencyService
{
    public class CurrencyService(UserManager<Employee> userManager, ICurrencyRepo currencyRepo,IUnitOfWork unitOfWork) : ICurrencyService
    {
        public async Task<DataResponseDTO> AddNewCurrency(string Id ,AddNewCurrencyRequest request)
        {

            var user = await userManager.FindByIdAsync(Id);
            if (user == null)
            {
                return new DataResponseDTO
                {
                    Success = false,
                    Message = "User Not Found"
                };
            }

            try
            {
                if (request == null)
                {
                    return new DataResponseDTO
                    {
                        Success = false,
                        Message = "Data Is Required"
                    };

                }

                if (request.Name.Length<=2)
                {
                    return new DataResponseDTO
                    {
                        Success = false,
                        Message = "Currency Name Must Be More Than 2 Letters"
                    };
                }

                

                bool currencyexists = await currencyRepo.AnyAsync(x => x.Name ==  request.Name);

                if (currencyexists)
                {
                    return new DataResponseDTO
                    {
                        Success = false,
                        Message = "Currency Name Exists"
                    };
                }

                var currency = new Currency
                {
                    Name = request.Name,
                    EmployeeAddedId = Id,
                };


                await currencyRepo.AddAsync(currency);
                await unitOfWork.SaveChangesAsync();

                return new DataResponseDTO
                {
                    Success = true,
                    Message = "Currency Added Successfully",
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

        public async Task<DataResponseDTO> UpdateCurrency(Guid Id, UpdateCurrencyDTO updateCurrencyDTO)
        {
            try
            {
                var existedcurrency = await currencyRepo.GetByIdAsync(Id);
                if (existedcurrency == null)
                {
                    return new DataResponseDTO
                    {
                        Success = false,
                        Message = "Currency not found"
                    };
                }

                if (updateCurrencyDTO == null)
                {
                    return new DataResponseDTO
                    {
                        Success = false,
                        Message = "No data provided"
                    };
                }

                // Validate Name if present
                if (updateCurrencyDTO.Name != null)
                {
                    if (updateCurrencyDTO.Name.Length <= 2)
                    {
                        return new DataResponseDTO
                        {
                            Success = false,
                            Message = "Currency Name must be more than 2 letters"
                        };
                    }

                    bool nameExists = await currencyRepo.AnyAsync(x => x.Name == updateCurrencyDTO.Name && x.Id != Id);
                    if (nameExists)
                    {
                        return new DataResponseDTO
                        {
                            Success = false,
                            Message = "Currency name already exists"
                        };
                    }

                    existedcurrency.Name = updateCurrencyDTO.Name;
                }

                // Validate Place if present
               

                // If nothing changed
                if (updateCurrencyDTO.Name == null)
                {
                    return new DataResponseDTO
                    {
                        Success = true,
                        Message = "No changes to update"
                    };
                }

                await currencyRepo.UpdateAsync(existedcurrency);
                await unitOfWork.SaveChangesAsync();

                return new DataResponseDTO
                {
                    Success = true,
                    Message = "Currency updated successfully"
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

        public async Task<DataResponseDTO> DeleteCurrency(Guid Id)
        {
            try
            {
                var existedcurrency = await currencyRepo.GetByIdAsync(Id);
                if (existedcurrency == null)
                {
                    return new DataResponseDTO
                    {
                        Success = false,
                        Message = "Currency not found"
                    };
                }

                await currencyRepo.DeleteAsync(existedcurrency);
                await unitOfWork.SaveChangesAsync();

                return new DataResponseDTO
                {
                    Success = true,
                    Message = "Currency Deleted successfully"
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

        public async Task<IQueryable<GetAllPaymentMethodsandStatusesDTO>> GetAllCurrencices()
        {
            var Currencies = currencyRepo.GetAll().AsNoTracking().Select(x => new GetAllPaymentMethodsandStatusesDTO
            {
                Id = x.Id,
                Name = x.Name,
                EmployeeAdded =x.Employee.UserName,
            });



            return Currencies;
        }

        public async Task<Pagination<GetAllPaymentMethodsandStatusesDTO>> GetAllCurrencicesPaginated(string? Name, string? Employee, string SortColumn = "Name", bool IsAscending = false, int Page = 1, int PageSize = 20)
        {
            var currencies = currencyRepo.GetAll();

            if (!string.IsNullOrEmpty(Name))
            {
                currencies = currencies.Where(x => x.Name.Contains(Name));
            }



            if (!string.IsNullOrEmpty(Employee))
            {
                currencies = currencies.Where(x => x.Employee.UserName.Contains(Employee));
            }




            if (IsAscending == false)
            {
                currencies = currencies.OrderByDescending(x => x.Name); // FIXED: added assignment
            }

            var result = currencyRepo.Sort(currencies, SortColumn, IsAscending);


            var Currencies = result.Select(m => new GetAllPaymentMethodsandStatusesDTO
            {
                Id = m.Id,
                Name = m.Name,
                EmployeeAdded = m.Employee.UserName
            });

            //// Check if any products exist before pagination
            //var totalCount = await HotelsDto.CountAsync();


            var paginatedcurrencies = await Pagination<GetAllPaymentMethodsandStatusesDTO>.CreateAsync(Currencies, Page, PageSize);

            return paginatedcurrencies;
        }
        public async Task<GetPaymentStatusandMethodDetailsDTO> GetCurrencyDetails(Guid Id)
        {
            var existedCurrency = currencyRepo.GetAll().AsNoTracking().Where(x => x.Id==Id);

            var currency = existedCurrency.Select(x => new GetPaymentStatusandMethodDetailsDTO
            {
                Name = x.Name,
                EmployeeAdded = x.Employee.UserName

            }).FirstOrDefault();



            return currency;
        }

        public async Task<Pagination<GetMethodOpreationsCountDTO>> GetCurrencyOpreationsCount(int Page = 1, int PageSize = 20)
        {
            var Currencies = currencyRepo.GetAll();

            var OpreationsDTO = Currencies.Select(c => new GetMethodOpreationsCountDTO
            {
                Id = c.Id,
                Name = c.Name,
                OpreationsCount = c.Operations.Count(),
                TotalMoney = c.Operations.Sum(x => x.Value)
            });

            var paginatedOpreations = await Pagination<GetMethodOpreationsCountDTO>.CreateAsync(OpreationsDTO, Page, PageSize);

            return paginatedOpreations;
        }

        public async Task<Pagination<GetCurrencyBookingsCountDTO>> GetCurrencyBookingsCount(int Page = 1, int PageSize = 20)
        {
            var Currencies = currencyRepo.GetAll();

            var BookingsDTO = Currencies.Select(c => new GetCurrencyBookingsCountDTO
            {
                Id = c.Id,
                Name = c.Name,
                BookingsCount = c.Bookings.Count(),
                TotalMoney = c.Bookings.Sum(x => x.TotalPriceAfterDiscount)
            });

            var paginatedBookings = await Pagination<GetCurrencyBookingsCountDTO>.CreateAsync(BookingsDTO, Page, PageSize);

            return paginatedBookings;
        }

        public async Task<Pagination<GetCurrencyVouchersCountDTO>> GetCurrencyVouchersCount(int Page = 1, int PageSize = 20)
        {
            var Currencies = currencyRepo.GetAll();

            var VouchersDTO = Currencies.Select(c => new GetCurrencyVouchersCountDTO
            {
                Id = c.Id,
                Name = c.Name,
                VouchersCount = c.Vouchers.Count(),
                TotalMoney = c.Vouchers.Sum(x => x.TotalPrice)
            });

            var paginatedVouchers = await Pagination<GetCurrencyVouchersCountDTO>.CreateAsync(VouchersDTO, Page, PageSize);

            return paginatedVouchers;
        }

    }
}
