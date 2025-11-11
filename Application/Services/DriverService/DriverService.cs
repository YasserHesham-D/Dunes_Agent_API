using Application.Dtos;
using Application.Dtos.HotelsandLocations;
using Application.DTOS.Driver;
using Domain.Interfaces.IModelsRepo;
using Domain.Interfaces.IUnitOfWork;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.DriverService
{
    public class DriverService(IDriverRepo driverRepo ,IUnitOfWork unitOfWork) : IDriverService
    {
        

        public async Task<DataResponseDTO> AddNewDriver(AddNewDriverDTO newDriverDTO)
        {
            try
            {
                // ✅ 1. Validate DTO using DataAnnotations
                var validationResults = new List<ValidationResult>();
                var validationContext = new ValidationContext(newDriverDTO, null, null);

                bool isValid = Validator.TryValidateObject(newDriverDTO, validationContext, validationResults, true);

                if (!isValid)
                {
                    // Collect all validation messages
                    string allErrors = string.Join(" | ", validationResults.Select(v => v.ErrorMessage));
                    return new DataResponseDTO
                    {
                        Success = false,
                        Message = allErrors
                    };
                }

                // ✅ 2. Check if a driver with same name or phone exists
                bool exists = await driverRepo.AnyAsync(x => x.Name == newDriverDTO.Name || x.PhoneNumber == newDriverDTO.PhoneNumber);
                if (exists)
                {
                    return new DataResponseDTO
                    {
                        Success = false,
                        Message = "A driver with this name or phone number already exists."
                    };
                }

                // ✅ 3. Create new driver
                var newDriver = new Driver
                {
                    Name = newDriverDTO.Name,
                    PhoneNumber = newDriverDTO.PhoneNumber,
                    CarNumber = newDriverDTO.CarNumber,
                    PlaceOfWork = newDriverDTO.PlaceOfWork,
                    
                };

                await driverRepo.AddAsync(newDriver);
                await unitOfWork.SaveChangesAsync();

                // ✅ 4. Return success
                return new DataResponseDTO
                {
                    Success = true,
                    Message = "Driver added successfully."
                };
            }
            catch (Exception ex)
            {
                return new DataResponseDTO
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<DataResponseDTO> DeleteDriver(Guid Id)
        {
            try
            {
                var existeddriver = await driverRepo.GetByIdAsync(Id);
                if (existeddriver == null)
                {
                    return new DataResponseDTO
                    {
                        Success = false,
                        Message = "Driver not found"
                    };
                }

                await driverRepo.DeleteAsync(existeddriver);
                await unitOfWork.SaveChangesAsync();

                return new DataResponseDTO
                {
                    Success = true,
                    Message = "Driver Deleted successfully"
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

        public async Task<IQueryable<GetAllDriversDTO>> GetAllDrivers()
        {
            var drivers = driverRepo.GetAll().AsNoTracking().Select(x => new GetAllDriversDTO
            {
                Id = x.Id,
                Name = x.Name,
                PhoneNumber = x.PhoneNumber,
                CarNumber = x.CarNumber,
                PlaceOfWork = x.PlaceOfWork,
                EntryDate = x.DateOfAdd,
                EmployeeAdded =x.Employee.UserName
            });



            return drivers;
        }

        public async Task<Pagination<GetAllDriversDTO>> GetAllDriversPaginated
        (string? Name, string? CarNumber, string? PhoneNumber, string? Place, string? Employee, DateTime? DateFrom, DateTime? DateTo, string SortColumn = "DateOfAdd", bool IsAscending = false, int Page = 1, int PageSize = 20)
        {
            var drivers = driverRepo.GetAll();

            if (!string.IsNullOrEmpty(Name))
            {
                drivers = drivers.Where(x => x.Name.Contains(Name));
            }

            if (!string.IsNullOrEmpty(Place))
            {
                drivers = drivers.Where(x => x.PlaceOfWork.Contains(Place));
            }


            if (!string.IsNullOrEmpty(CarNumber))
            {
                drivers = drivers.Where(x => x.CarNumber.Contains(CarNumber));
            }

            if (!string.IsNullOrEmpty(PhoneNumber))
            {
                drivers = drivers.Where(x => x.PhoneNumber.Contains(PhoneNumber));
            }

            if (!string.IsNullOrEmpty(Employee))
            {
                drivers = drivers.Where(x => x.Employee.UserName.Contains(Employee));
            }


            if (DateFrom != null && DateTo != null)
            {
                drivers = drivers.Where(x => x.DateOfAdd >= DateFrom && x.DateOfAdd <= DateTo);
            }
            if (DateFrom != null && DateTo == null)
            {
                drivers = drivers.Where(x => x.DateOfAdd >= DateFrom && x.DateOfAdd <= DateTime.Now);
            }

            if (IsAscending == false)
            {
                drivers = drivers.OrderByDescending(x => x.DateOfAdd); // FIXED: added assignment
            }

            var result = driverRepo.Sort(drivers, SortColumn, IsAscending);


            var DriversDto = result.Select(d => new GetAllDriversDTO
            {
                Id = d.Id,
                Name = d.Name,
                PlaceOfWork = d.PlaceOfWork,
                EntryDate = d.DateOfAdd,
                CarNumber = d.CarNumber,
                PhoneNumber = d.PhoneNumber,
                EmployeeAdded = d.Employee.UserName
            });

            //// Check if any products exist before pagination
            //var totalCount = await HotelsDto.CountAsync();


            var paginateddrivers = await Pagination<GetAllDriversDTO>.CreateAsync(DriversDto, Page, PageSize);

            return paginateddrivers;
        }

        public async Task<DataResponseDTO> UpdateDriver(Guid Id, UpdateDriverDTO updateDriverDTO)
        {
            try
            {
                var existeddriver = await driverRepo.GetByIdAsync(Id);
                // ✅ 1. Validate DTO using DataAnnotations
                var validationResults = new List<ValidationResult>();
                var validationContext = new ValidationContext(updateDriverDTO, null, null);

                bool isValid = Validator.TryValidateObject(updateDriverDTO, validationContext, validationResults, true);

                if (!isValid)
                {
                    // Collect all validation messages
                    string allErrors = string.Join(" | ", validationResults.Select(v => v.ErrorMessage));
                    return new DataResponseDTO
                    {
                        Success = false,
                        Message = allErrors
                    };
                }

                // ✅ 2. Check if a driver with same name or phone exists
                bool exists = await driverRepo.AnyAsync(x => x.Name == updateDriverDTO.Name || x.PhoneNumber == updateDriverDTO.PhoneNumber);
                if (exists)
                {
                    return new DataResponseDTO
                    {
                        Success = false,
                        Message = "A driver with this name or phone number already exists."
                    };
                }


                if (updateDriverDTO==null)
                {
                    return new DataResponseDTO
                    {
                        Success = false,
                        Message = "No Changes To Update"
                    };
                }

                

                if (updateDriverDTO.Name!=null)
                {
                    existeddriver.Name=updateDriverDTO.Name;
                    
                }

                if (updateDriverDTO.CarNumber!=null)
                {
                    existeddriver.CarNumber = updateDriverDTO.CarNumber;
                }

                if (updateDriverDTO.PhoneNumber!=null)
                {
                    existeddriver.PhoneNumber = updateDriverDTO.PhoneNumber;
                }

                if (updateDriverDTO.PlaceOfWork!=null)
                {
                    existeddriver.PlaceOfWork = updateDriverDTO.PlaceOfWork;
                }



                await driverRepo.UpdateAsync(existeddriver);
                await unitOfWork.SaveChangesAsync();

                // ✅ 4. Return success
                return new DataResponseDTO
                {
                    Success = true,
                    Message = "Driver Updated successfully."
                };
            }
            catch (Exception ex)
            {
                return new DataResponseDTO
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }


        public async Task<GetAllDriversDTO> GetDriverDetails(Guid Id)
        {
            var existeddriver = driverRepo.GetAll().AsNoTracking().Where(x => x.Id==Id);

            var driver = existeddriver.Select(x => new GetAllDriversDTO
            {
               
                Name = x.Name,
                PhoneNumber = x.PhoneNumber,
                CarNumber = x.CarNumber,
                PlaceOfWork = x.PlaceOfWork,
                EntryDate = x.DateOfAdd,
                EmployeeAdded =x.Employee.UserName

            }).FirstOrDefault();



            return driver;



        }


        public async Task<Pagination<GetDriverBookingsDTO>> GetDriverBookingsCount(int Page = 1, int PageSize = 20)
        {
            var drivers = driverRepo.GetAll();

            var BookingsDto = drivers.Select(d => new GetDriverBookingsDTO
            {
                Id = d.Id,
                Name = d.Name,
                BookingsCount = d.Bookings.Count()

            });

            var paginatedBookings = await Pagination<GetDriverBookingsDTO>.CreateAsync(BookingsDto, Page, PageSize);

            return paginatedBookings;
        }
    }
}
