using Application.Dtos;
using Application.Dtos.HotelsandLocations;
using Application.DTOS.Currency_Values;
using Domain.Interfaces.IModelsRepo;
using Domain.Interfaces.IUnitOfWork;
using Domain.Models;
using Domain.Models.MTM;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.CurrencyValuesService
{
    public class CurrencyValuesService : ICurrencyValuesService
    {
        private readonly ICurrencyValuesRepo currencyValuesRepo;
       
        private readonly IUnitOfWork unitOfWork;

        public CurrencyValuesService(ICurrencyValuesRepo currencyValuesRepo, IUnitOfWork unitOfWork)
        {
            this.currencyValuesRepo = currencyValuesRepo;
            this.unitOfWork = unitOfWork;
        }
        public async Task<DataResponseDTO> AddNewCurrencyValue(AddNewCurrencyValueDTO currencyValueDTO)
        {
            try
            {
                if (currencyValueDTO == null)
                {
                    return new DataResponseDTO
                    {
                        Success = false,
                        Message = "Data Is Required"
                    };

                }

                if (currencyValueDTO.CurrencyFrom == Guid.Empty)
                {
                    return new DataResponseDTO
                    {
                        Success = false,
                        Message = "The Value Is Not Correct ,Currency From Is Required"
                    };
                }

                if (currencyValueDTO.CurrencyTo == Guid.Empty)
                {
                    return new DataResponseDTO
                    {
                        Success = false,
                        Message = "The Value Is Not Correct ,Currency To Is Required"
                    };
                }

                if (currencyValueDTO.Price<=0)
                {
                    return new DataResponseDTO
                    {
                        Success = false,
                        Message = "Value Of Price Must Be More Than 0"
                    };
                }

                if(currencyValueDTO.CurrencyFrom == currencyValueDTO.CurrencyTo)
                {
                    return new DataResponseDTO
                    {
                        Success = false,
                        Message = "Currency From and To are The Same Value"
                    };
                }

                bool currencyValueexists = await currencyValuesRepo.
                AnyAsync(x => x.CurrencyFromId ==  currencyValueDTO.CurrencyFrom && x.CurrencyToId ==currencyValueDTO.CurrencyTo);

                if (currencyValueexists)
                {
                    return new DataResponseDTO
                    {
                        Success = false,
                        Message = "Currency From and To Both Exists"
                    };
                }

                var currencyValue = new CurrencyValues
                {
                    CurrencyFromId = currencyValueDTO.CurrencyFrom,
                    Price = currencyValueDTO.Price,
                    CurrencyToId = currencyValueDTO.CurrencyTo
                };


                await currencyValuesRepo.AddAsync(currencyValue);
                await unitOfWork.SaveChangesAsync();

                return new DataResponseDTO
                {
                    Success = true,
                    Message = "Currency Value Added Successfully",
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

        public async Task<DataResponseDTO> DeleteCurrencyValue(Guid Id)
        {
            try
            {
                var existedcurrencyvalue = await currencyValuesRepo.GetByIdAsync(Id);
                if (existedcurrencyvalue == null)
                {
                    return new DataResponseDTO
                    {
                        Success = false,
                        Message = "Currency Value not found"
                    };
                }

                await currencyValuesRepo.DeleteAsync(existedcurrencyvalue);
                await unitOfWork.SaveChangesAsync();

                return new DataResponseDTO
                {
                    Success = true,
                    Message = "Currency Value Deleted successfully"
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

        public async Task<DataResponseDTO> UpdateCurrencyValue(Guid Id, UpdateCurrencyValueDTO updateCurrencyValueDTO)
        {
            try
            {
                var existedcurrencyvalue = await currencyValuesRepo.GetByIdAsync(Id);
                if (existedcurrencyvalue == null)
                {
                    return new DataResponseDTO
                    {
                        Success = false,
                        Message = "Cuurency Value not found"
                    };
                }

                if (updateCurrencyValueDTO == null)
                {
                    return new DataResponseDTO
                    {
                        Success = false,
                        Message = "No data provided"
                    };
                }

                if(updateCurrencyValueDTO.CurrencyFrom == updateCurrencyValueDTO.CurrencyTo)
                {
                    return new DataResponseDTO
                    {
                        Success = false,
                        Message = "Currency From and To are The Same Value"
                    };
                }

                if (updateCurrencyValueDTO.Price<=0)
                {
                    return new DataResponseDTO
                    {
                        Success = false,
                        Message = "Price Must Be More Than Zero"
                    };
                }

                // Validate Name if present
                if ((updateCurrencyValueDTO.CurrencyFrom != null || updateCurrencyValueDTO.CurrencyFrom!=Guid.Empty) && updateCurrencyValueDTO.CurrencyFrom != updateCurrencyValueDTO.CurrencyTo)
                {

                    existedcurrencyvalue.CurrencyFromId = (Guid)updateCurrencyValueDTO.CurrencyFrom;

                }

               


                if ((updateCurrencyValueDTO.CurrencyTo != null || updateCurrencyValueDTO.CurrencyTo!=Guid.Empty) && updateCurrencyValueDTO.CurrencyFrom != updateCurrencyValueDTO.CurrencyTo)
                {
                    existedcurrencyvalue.CurrencyToId = (Guid)updateCurrencyValueDTO.CurrencyTo;
                }


                if (updateCurrencyValueDTO.Price != null && updateCurrencyValueDTO.Price>0)
                {
                    existedcurrencyvalue.Price = (decimal)updateCurrencyValueDTO.Price;
                }


               bool currencyValueexists = await currencyValuesRepo.
               AnyAsync(x => x.CurrencyFromId ==  updateCurrencyValueDTO.CurrencyFrom && x.CurrencyToId ==updateCurrencyValueDTO.CurrencyTo);

                if (currencyValueexists)
                {
                    return new DataResponseDTO
                    {
                        Success = false,
                        Message = "Currency From and To Both Exists"
                    };
                }







                // If nothing changed
                if (updateCurrencyValueDTO.CurrencyFrom == null && updateCurrencyValueDTO.CurrencyTo == null && updateCurrencyValueDTO.Price==null)
                {
                    return new DataResponseDTO
                    {
                        Success = true,
                        Message = "No changes to update"
                    };
                }

                await currencyValuesRepo.UpdateAsync(existedcurrencyvalue);
                await unitOfWork.SaveChangesAsync();

                return new DataResponseDTO
                {
                    Success = true,
                    Message = "Currency Value updated successfully"
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


        public async Task<Pagination<GetAllCurrencyValuesDTO>> GetAllCurrencyValuesPaginated
        (string? CurrencyFrom, string? CurrencyTo, string? Employee, string SortColumn = "Price", bool IsAscending = false, int Page = 1, int PageSize = 20)
        {
            var currencyvalues = currencyValuesRepo.GetAll();

            if (!string.IsNullOrEmpty(CurrencyFrom))
            {
                currencyvalues = currencyvalues.Where(x => x.CurrencyFrom.Name.Contains(CurrencyFrom));
            }

            if (!string.IsNullOrEmpty(CurrencyTo))
            {
                currencyvalues = currencyvalues.Where(x => x.CurrencyTo.Name.Contains(CurrencyTo));
            }

            if (!string.IsNullOrEmpty(Employee))
            {
                currencyvalues = currencyvalues.Where(x => x.Employee.UserName.Contains(Employee));
            }


            

            if (IsAscending == false)
            {
                currencyvalues = currencyvalues.OrderByDescending(x => x.Price); // FIXED: added assignment
            }

            var result = currencyValuesRepo.Sort(currencyvalues, SortColumn, IsAscending);


            var CurrencyValues = result.Select(c => new GetAllCurrencyValuesDTO
            {
                Id = c.Id,
                CurrencyFrom = c.CurrencyFrom.Name,
                CurrencyTo = c.CurrencyTo.Name,
                Price = c.Price,
                EmployeeAdd=c.Employee.UserName,
                
            });

            //// Check if any products exist before pagination
            //var totalCount = await HotelsDto.CountAsync();


            var paginatedCurrencyValues = await Pagination<GetAllCurrencyValuesDTO>.CreateAsync(CurrencyValues, Page, PageSize);

            return paginatedCurrencyValues;
        }

        public async Task<GetAllCurrencyValuesDTO> GetCurrencyValueDetails(Guid Id)
        {
            var existedcurrencyvalue = currencyValuesRepo.GetAll().AsNoTracking().Where(x => x.Id==Id);

            var currencyvalue = existedcurrencyvalue.Select(c => new GetAllCurrencyValuesDTO
            {
                Id = c.Id,
                CurrencyFrom = c.CurrencyFrom.Name,
                CurrencyTo = c.CurrencyTo.Name,
                Price = c.Price,
                EmployeeAdd=c.Employee.UserName,

            }).FirstOrDefault();



            return currencyvalue;

        }
    }
    
}
