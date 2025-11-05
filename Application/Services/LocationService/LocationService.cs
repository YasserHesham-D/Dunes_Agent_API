
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

namespace Application.Services.LocationService
{
    public class LocationService : ILocationService
    {
        private readonly ILocationRepo locationRepo;
        private readonly IAccountsRepo accountsRepo;
        private readonly IUnitOfWork unitOfWork;

        public LocationService(ILocationRepo locationRepo, IUnitOfWork unitOfWork)
        {
            this.locationRepo = locationRepo;
            this.unitOfWork = unitOfWork;
        }
        public async Task<DataResponseDTO> AddNewLocation(AddNewDTO addNewLocationDTO)
        {
            try
            {
                if (addNewLocationDTO == null)
                {
                    return new DataResponseDTO
                    {
                        Success = false,
                        Message = "Data Is Required"
                    };

                }

                if (addNewLocationDTO.Name.Length<=2)
                {
                    return new DataResponseDTO
                    {
                        Success = false,
                        Message = "Location Name Must Be More Than 2 Letters"
                    };
                }

                if (addNewLocationDTO.Place.Length<=2)
                {
                    return new DataResponseDTO
                    {
                        Success = false,
                        Message = "Location Place Must Be More Than 2 Letters"
                    };
                }

                bool locationexists = await locationRepo.AnyAsync(x => x.Name ==  addNewLocationDTO.Name);

                if (locationexists)
                {
                    return new DataResponseDTO
                    {
                        Success = false,
                        Message = "Location Name Exists"
                    };
                }

                var location = new Location
                {
                    Name = addNewLocationDTO.Name,
                    Place = addNewLocationDTO.Place,
                };


                await locationRepo.AddAsync(location);
                await unitOfWork.SaveChangesAsync();

                return new DataResponseDTO
                {
                    Success = true,
                    Message = "Location Added Successfully",
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

        public async Task<DataResponseDTO> DeleteLocation(Guid Id)
        {
            try
            {
                var existedlocation = await locationRepo.GetByIdAsync(Id);
                if (existedlocation == null)
                {
                    return new DataResponseDTO
                    {
                        Success = false,
                        Message = "Location not found"
                    };
                }

                await locationRepo.DeleteAsync(existedlocation);
                await unitOfWork.SaveChangesAsync();

                return new DataResponseDTO
                {
                    Success = true,
                    Message = "Location Deleted successfully"
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

        public async Task<IQueryable<GetAllDTO>> GetAllLocations()
        {
            var locations = locationRepo.GetAll().Select(x => new GetAllDTO
            {
                Id = x.Id,
                Name = x.Name,
                Place = x.Place,
                EntryDate = x.EntryDate,
                EmployeeAdded =x.Employee.UserName
            });



            return locations;
        }

        public async Task<Pagination<GetAllDTO>> GetAllLocationsPaginated
            (string? Name, string? Place, string? Employee, DateTime? DateFrom,
            DateTime? DateTo, string SortColumn = "EntryDate", bool IsAscending = false, int Page = 1, int PageSize = 20)
        {
            var locations = locationRepo.GetAll();

            if (!string.IsNullOrEmpty(Name))
            {
                locations = locations.Where(x => x.Name.Contains(Name));
            }

            if (!string.IsNullOrEmpty(Place))
            {
                locations = locations.Where(x => x.Place.Contains(Place));
            }

            if (!string.IsNullOrEmpty(Employee))
            {
                locations = locations.Where(x => x.Employee.UserName.Contains(Employee));
            }


            if (DateFrom != null && DateTo != null)
            {
                locations = locations.Where(x => x.EntryDate >= DateFrom && x.EntryDate <= DateTo);
            }
            if (DateFrom != null && DateTo == null)
            {
                locations = locations.Where(x => x.EntryDate >= DateFrom && x.EntryDate <= DateTime.Now);
            }

            if (IsAscending == false)
            {
                locations = locations.OrderByDescending(x => x.EntryDate); // FIXED: added assignment
            }

            var result = locationRepo.Sort(locations, SortColumn, IsAscending);


            var HotelsDto = result.Select(hotel => new GetAllDTO
            {
                Id = hotel.Id,
                Name = hotel.Name,
                Place = hotel.Place,
                EntryDate = hotel.EntryDate,
                EmployeeAdded = hotel.Employee.UserName
            });

            //// Check if any products exist before pagination
            //var totalCount = await HotelsDto.CountAsync();


            var paginatedlocations = await Pagination<GetAllDTO>.CreateAsync(HotelsDto, Page, PageSize);

            return paginatedlocations;
        }

        public async Task<Dictionary<string, int>> GetEmployeesByLocationAsync()
        {
            return await accountsRepo.GetAll()
         .GroupBy(c => c.Location.Name) 
         .Select(g => new { Location = g.Key, Count = g.Count() })
         .ToDictionaryAsync(x => x.Location, x => x.Count);
        }

        public async Task<GetDetailsDTO> GetLocationDetails(Guid Id)
        {
            var existedlocation = locationRepo.GetAll().AsNoTracking().Where(x => x.Id==Id);

            var location = existedlocation.Select(x => new GetDetailsDTO
            {
                Name = x.Name,
                Place = x.Place,
                EntryDate = x.EntryDate,
                EmployeeAdded = x.Employee.UserName

            }).FirstOrDefault();



            return location;
        }

        public async Task<Pagination<GetEmployeesCountDTO>> GetLocationsEmployeesCount(int Page = 1, int PageSize = 20)
        {
            var locations = locationRepo.GetAll();

            var EmployeesDTO = locations.Select(hotel => new GetEmployeesCountDTO
            {
                Id = hotel.Id,
                Name = hotel.Name,
                EmployeesCount = hotel.EmployeesBelong.Count()

            });

            var paginatedEmployees = await Pagination<GetEmployeesCountDTO>.CreateAsync(EmployeesDTO, Page, PageSize);

            return paginatedEmployees;
        }

        public async Task<DataResponseDTO> UpdateLocation(Guid Id, UpdateDTO UpdatedLocationDto)
        {
            try
            {
                var existedlocation = await locationRepo.GetByIdAsync(Id);
                if (existedlocation == null)
                {
                    return new DataResponseDTO
                    {
                        Success = false,
                        Message = "Location not found"
                    };
                }

                if (UpdatedLocationDto == null)
                {
                    return new DataResponseDTO
                    {
                        Success = false,
                        Message = "No data provided"
                    };
                }

                // Validate Name if present
                if (UpdatedLocationDto.Name != null)
                {
                    if (UpdatedLocationDto.Name.Length <= 2)
                    {
                        return new DataResponseDTO
                        {
                            Success = false,
                            Message = "Location Name must be more than 2 letters"
                        };
                    }

                    bool nameExists = await locationRepo.AnyAsync(x => x.Name == UpdatedLocationDto.Name && x.Id != Id);
                    if (nameExists)
                    {
                        return new DataResponseDTO
                        {
                            Success = false,
                            Message = "Location name already exists"
                        };
                    }

                    existedlocation.Name = UpdatedLocationDto.Name;
                }

                // Validate Place if present
                if (UpdatedLocationDto.Place != null)
                {
                    if (UpdatedLocationDto.Place.Length <= 2)
                    {
                        return new DataResponseDTO
                        {
                            Success = false,
                            Message = "Location place must be more than 2 letters"
                        };
                    }

                    existedlocation.Place = UpdatedLocationDto.Place;
                }

                // If nothing changed
                if (UpdatedLocationDto.Name == null && UpdatedLocationDto.Place == null)
                {
                    return new DataResponseDTO
                    {
                        Success = true,
                        Message = "No changes to update"
                    };
                }

                await locationRepo.UpdateAsync(existedlocation);
                await unitOfWork.SaveChangesAsync();

                return new DataResponseDTO
                {
                    Success = true,
                    Message = "Location updated successfully"
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

