using Application.Dtos;
using Application.Dtos.HotelsandLocations;
using Domain.Interfaces.IModelsRepo;
using Domain.Interfaces.IUnitOfWork;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.HotelService
{
    public class HotelService : IHotelService
    {
        private readonly IHotelRepo hotelRepo;
        private readonly IAccountsRepo accountsRepo;
        private readonly IUnitOfWork unitOfWork;

        public HotelService(IHotelRepo hotelRepo, IUnitOfWork unitOfWork)
        {
            this.hotelRepo = hotelRepo;
            this.unitOfWork = unitOfWork;
        }
        public async Task<DataResponseDTO> AddNewHotel(AddNewDTO addNewHotelDTO)
        {
            try
            {
                if (addNewHotelDTO == null)
                {
                    return new DataResponseDTO
                    {
                        Success = false,
                        Message = "Data Is Required"
                    };

                }

                if (addNewHotelDTO.Name.Length<=2)
                {
                    return new DataResponseDTO
                    {
                        Success = false,
                        Message = "Hotel Name Must Be More Than 2 Letters"
                    };
                }

                if (addNewHotelDTO.Place.Length<=2)
                {
                    return new DataResponseDTO
                    {
                        Success = false,
                        Message = "Hotel Place Must Be More Than 2 Letters"
                    };
                }

                bool hotelexists = await hotelRepo.AnyAsync(x => x.Name ==  addNewHotelDTO.Name);

                if (hotelexists)
                {
                    return new DataResponseDTO
                    {
                        Success = false,
                        Message = "Hotel Name Exists"
                    };
                }

                var hotel = new Hotel
                {
                    Name = addNewHotelDTO.Name,
                    Place = addNewHotelDTO.Place,
                };


                await hotelRepo.AddAsync(hotel);
                await unitOfWork.SaveChangesAsync();

                return new DataResponseDTO
                {
                    Success = true,
                    Message = "Hotel Added Successfully",
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

        public async Task<DataResponseDTO> DeleteHotel(Guid Id)
        {
            try
            {
                var existedhotel = await hotelRepo.GetByIdAsync(Id);
                if (existedhotel == null)
                {
                    return new DataResponseDTO
                    {
                        Success = false,
                        Message = "Hotel not found"
                    };
                }

                await hotelRepo.DeleteAsync(existedhotel);
                await unitOfWork.SaveChangesAsync();

                return new DataResponseDTO
                {
                    Success = true,
                    Message = "Hotel Deleted successfully"
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

        public async Task<IQueryable<GetAllDTO>> GetAllHotels()
        {
            var hotels = hotelRepo.GetAll().AsNoTracking().Select(x => new GetAllDTO
            {
                Id = x.Id,
                Name = x.Name,
                Place = x.Place,
                EntryDate = x.EntryDate,
                EmployeeAdded =x.Employee.FullName
            });



            return hotels;
        }

        public async Task<Pagination<GetAllDTO>> GetAllHotelsPaginated
        (string? Name, string? Place, string? Employee, DateTime? DateFrom, DateTime? DateTo, string SortColumn = "EntryDate", bool IsAscending = false, int Page = 1, int PageSize = 20)
        {
            var hotels = hotelRepo.GetAll();

            if (!string.IsNullOrEmpty(Name))
            {
                hotels = hotels.Where(x => x.Name.Contains(Name));
            }

            if (!string.IsNullOrEmpty(Place))
            {
                hotels = hotels.Where(x => x.Place.Contains(Place));
            }

            if (!string.IsNullOrEmpty(Employee))
            {
                hotels = hotels.Where(x => x.Employee.FullName.Contains(Employee));
            }


            if (DateFrom != null && DateTo != null)
            {
                hotels = hotels.Where(x => x.EntryDate >= DateFrom && x.EntryDate <= DateTo);
            }
            if (DateFrom != null && DateTo == null)
            {
                hotels = hotels.Where(x => x.EntryDate >= DateFrom && x.EntryDate <= DateTime.Now);
            }

            if (IsAscending == false)
            {
                hotels = hotels.OrderByDescending(x => x.EntryDate); // FIXED: added assignment
            }

            var result = hotelRepo.Sort(hotels, SortColumn, IsAscending);


            var HotelsDto = result.Select(hotel => new GetAllDTO
            {
                Id = hotel.Id,
                Name = hotel.Name,
                Place = hotel.Place,
                EntryDate = hotel.EntryDate,
                EmployeeAdded = hotel.Employee.FullName
            });

            //// Check if any products exist before pagination
            //var totalCount = await HotelsDto.CountAsync();


            var paginatedhotels = await Pagination<GetAllDTO>.CreateAsync(HotelsDto, Page, PageSize);

            return paginatedhotels;
        }

        public async Task<Dictionary<string, int>> GetEmployeesByHotelAsync()
        {
            return await accountsRepo.GetAll()
         .GroupBy(c => c.Hotel.Name)
         .Select(g => new { Hotel = g.Key, Count = g.Count() })
         .ToDictionaryAsync(x => x.Hotel, x => x.Count);
        }

        //public async Task<Dictionary<string, int>> GetEmployeesByHotelAsync()
        //{
        //    return await hotelRepo.GetAll()
        // .GroupBy(c => c.Name) // assuming Contact has Gender property
        // .Select(g => new { Country = g.Key, Count = g.Count() })
        // .ToDictionaryAsync(x => x.Country, x => x.Count);
        //}

        public async Task<GetDetailsDTO> GetHotelDetails(Guid Id)
        {
            var existedhotel = hotelRepo.GetAll().AsNoTracking().Where(x => x.Id==Id);

            var hotel = existedhotel.Select(x => new GetDetailsDTO
            {
                Name = x.Name,
                Place = x.Place,
                EntryDate = x.EntryDate,
                EmployeeAdded = x.Employee.FullName

            }).FirstOrDefault();



            return hotel;



        }

        public async Task<Pagination<GetEmployeesCountDTO>> GetHotelEmployeesCount(int Page = 1, int PageSize = 20)
        {
            var hotels = hotelRepo.GetAll();

            var EmployeesDTO = hotels.Select(hotel => new GetEmployeesCountDTO
            {
                Id = hotel.Id,
                Name = hotel.Name,
                EmployeesCount = hotel.EmployeesBelong.Count()

            });

            var paginatedEmployees = await Pagination<GetEmployeesCountDTO>.CreateAsync(EmployeesDTO, Page, PageSize);

            return paginatedEmployees;
        }

        public async Task<DataResponseDTO> UpdateHotel(Guid Id, UpdateDTO UpdatedHotelDto)
        {
            try
            {
                var existedhotel = await hotelRepo.GetByIdAsync(Id);
                if (existedhotel == null)
                {
                    return new DataResponseDTO
                    {
                        Success = false,
                        Message = "Hotel not found"
                    };
                }

                if (UpdatedHotelDto == null)
                {
                    return new DataResponseDTO
                    {
                        Success = false,
                        Message = "No data provided"
                    };
                }

                // Validate Name if present
                if (UpdatedHotelDto.Name != null)
                {
                    if (UpdatedHotelDto.Name.Length <= 2)
                    {
                        return new DataResponseDTO
                        {
                            Success = false,
                            Message = "Hotel Name must be more than 2 letters"
                        };
                    }

                    bool nameExists = await hotelRepo.AnyAsync(x => x.Name == UpdatedHotelDto.Name && x.Id != Id);
                    if (nameExists)
                    {
                        return new DataResponseDTO
                        {
                            Success = false,
                            Message = "Hotel name already exists"
                        };
                    }

                    existedhotel.Name = UpdatedHotelDto.Name;
                }

                // Validate Place if present
                if (UpdatedHotelDto.Place != null)
                {
                    if (UpdatedHotelDto.Place.Length <= 2)
                    {
                        return new DataResponseDTO
                        {
                            Success = false,
                            Message = "Hotel place must be more than 2 letters"
                        };
                    }

                    existedhotel.Place = UpdatedHotelDto.Place;
                }

                // If nothing changed
                if (UpdatedHotelDto.Name == null && UpdatedHotelDto.Place == null)
                {
                    return new DataResponseDTO
                    {
                        Success = true,
                        Message = "No changes to update"
                    };
                }

                await hotelRepo.UpdateAsync(existedhotel);
                await unitOfWork.SaveChangesAsync();

                return new DataResponseDTO
                {
                    Success = true,
                    Message = "Hotel updated successfully"
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
