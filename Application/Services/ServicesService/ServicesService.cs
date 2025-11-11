using Application.Dtos;
using Application.Dtos.HotelsandLocations;
using Application.Dtos.Service;
using Application.Services.LocationService;
using Domain.Interfaces.IModelsRepo;
using Domain.Interfaces.IUnitOfWork;
using Domain.Models;
using Domain.Models.Accounts;
using Domain.Models.MTM;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.ServicesService
{

    public class ServicesService(ILocationRepo locationRepo,ILogger<Service> logger,UserManager<Employee> userManager,IServicesRepo serviceRepo,IUnitOfWork unitOfWork,IMTMRepo mTMRepo) : IServicesService
    {
        public async Task<DataResponseDTO> AddNewServiceAsync(AddNewServiceRequest request, string UserId)
        {
            var user = await userManager.FindByIdAsync(UserId);
            
            if (user == null)
                return new DataResponseDTO
                {
                    Success= false,
                    Message = "You dont have permission"
                };

            //if (await serviceRepo.AnyAsync(x => x.ServiceName.Equals(request.ServiceName)))
            //    return new DataResponseDTO
            //    {
            //        Success = false,
            //        Message = "Service Name Already Exists"
            //    };


            var NewService = new Service
            {
                Duration = request.Duration,
                TimeDuration = request.TimeDuration,
                Description = request.ServiceDescription,
                EmployeeAddedId = user.Id,
                ServiceName = request.ServiceName,
                Type = request.type,
                
                LocationServices = request.locationServices.Select(x => new LocationServices
                {
                    
                    LocationId = x.LocationId,
                    AdultsPrice = x.AdultPrice,
                    ChildsPrice = x.ChildPrice,
                    KidsPrice = x.KidPrice

                }).ToList(),
            };

            await serviceRepo.AddAsync(NewService);
            await unitOfWork.SaveChangesAsync();

           
             return new DataResponseDTO
             {
                 Success = true,
                 Message = "Service Added Succesfully ."
             };

        }

        public async Task<DataResponseDTO> DeleteServiceAsync(Guid id)
        {
            var Exist = await serviceRepo.GetByIdAsync(id);
            if (Exist == null)
                return new DataResponseDTO
                {
                    Success = false,
                    Message = "Service Not Found"
                };

            var x = serviceRepo.GetAll().Include(x=>x.LocationServices).Where(x =>x.Id == id).FirstOrDefault();

            await unitOfWork.BeginTransactionAsync();
            try
            {
                var locationservices = x.LocationServices.ToList();

                foreach (var locationservice in locationservices)
                {
                    locationservice.IsDeleted = true;
                }

                Exist.IsDeleted = true;

                await unitOfWork.CommitAsync();
                unitOfWork.Dispose();

                return new DataResponseDTO
                {
                    Success = true,
                    Message = "Deleted Succesfully"
                };

            }
            catch (Exception ex)
            {
                await unitOfWork.RollbackAsync();
                unitOfWork.Dispose();

                logger.LogInformation(ex,"Error ON Deleting service");
                return new DataResponseDTO
                {
                    Success = false,
                    Message = "Error On Deleting Servcie"
                };
            }
        }

        public async Task<Pagination<GetAllServicesDTO>> GetAllServicesPaginated
            (string? empname, string? serviceName,string? locationname ,
            int? duration, string sortColumn, bool isAscending, int page, int pageSize, string columnName = "EntryDate")
        {

            var services = serviceRepo.GetAll().Where(x => x.IsDeleted == false);


            if (!string.IsNullOrEmpty(empname))
            {
                services = services.Where(x => x.Employee.UserName.Contains( empname));
            }
            if (!string.IsNullOrEmpty(serviceName))
            {
                services = services.Where(x => x.ServiceName.Contains(serviceName));
            }
            if(!string.IsNullOrEmpty(duration.ToString()))
            {
                services = services.Where(x => x.Duration == duration);
            }
            if (!string.IsNullOrEmpty(locationname))
            {

                services = services.Where(s =>
                    s.LocationServices.Any(ls => ls.Location.Name.Contains(locationname))).Where(x => x.IsDeleted == false);

            }

            var result = serviceRepo.Sort(services,sortColumn, isAscending);



            var Services = result.Select(x => new GetAllServicesDTO
            {
                serviceName = x.ServiceName,
                EmployeeName = x.Employee.UserName,
                LocationName = x.LocationServices.Select(x => x.Location.Name).FirstOrDefault(),
                Duration = x.Duration,
                ServiceDescription = x.Description,
                 servicelocations = x.LocationServices.Select(x => new ServicelocationDTO
                 {
                    
                    Adult = x.AdultsPrice,
                    Child = x.ChildsPrice,
                    Kid = x.KidsPrice
                }).ToList()

            });

            var paginatedServices = await Pagination<GetAllServicesDTO>.CreateAsync(Services, page, pageSize);

            return  paginatedServices;
            
        }

        public async Task<DataResponseDTO> PatchService(Guid id, UpdateServiceRequest request)
        {
            var exist = await serviceRepo.GetByIdAsync(id);

            if (exist == null) return new DataResponseDTO
            {
                Success = false,
                Message = "Service Not Found"
            };

            if (request == null) return new DataResponseDTO
            {
                Success = false,
                Message = "Invalid Request"
            };

            if (!string.IsNullOrEmpty(request.ServiceName))
            {
                if (await serviceRepo.AnyAsync(x => x.ServiceName.Equals(request.ServiceName)))
                    return new DataResponseDTO 
                    {
                        Success = false,
                        Message = "Service Name Already Exist"
                    };

                exist.ServiceName = request.ServiceName;
            }

            if(!string.IsNullOrEmpty(request.ServiceDescription))
                exist.Description = request.ServiceDescription;

            if (!string.IsNullOrEmpty(request.Duration.ToString()))
                exist.Duration = (int)request.Duration;
               
            if(request.TimeDuration.Equals(0))
                exist.TimeDuration = (Domain.Enums.TimeDuration)request.TimeDuration;

            if (!string.IsNullOrEmpty(request.type))
                exist.Type = request.type;
            
            // Delete
            if(request.LocationServiceDelete != null && request.LocationServiceDelete != Guid.Empty)
            {
                var LocatoinServiceExist = await mTMRepo.GetLocationServicesById(request.LocationServiceDelete.Value);
                if (LocatoinServiceExist == null)
                    return new DataResponseDTO
                    {
                        Success = false,
                        Message = "Location Servcie Not Found"
                    };
                LocatoinServiceExist.IsDeleted = true;
            }

            // add mtm
            if (request.locationServcieAdds != null && request.locationServcieAdds.Count > 0)
            {
                foreach (var ls in request.locationServcieAdds)
                {

                    var newls = new LocationServices
                    {
                        LocationId = ls.LocationId,
                        ServiceId = exist.Id,
                        AdultsPrice = ls.AdultPrice,
                        ChildsPrice = ls.ChildPrice,
                        KidsPrice = ls.KidPrice,
                    };
                    await mTMRepo.AddLocationServices(newls);
                }
            }

            // update mtm
            if (request.locationServiceUpdates != null && request.locationServiceUpdates.Count > 0)
            {
                foreach (var ls in request.locationServiceUpdates)
                {
                    var existedls = await mTMRepo.GetLocationServicesById(ls.Id);

                    if (existedls == null)
                        return new DataResponseDTO
                        {
                            Success = false,
                            Message = "LocationService Not Found"
                        };



                    if (ls.LocationId != Guid.Empty)
                    {
                        if (await locationRepo.AnyAsync(x => x.Id == ls.LocationId))
                            existedls.LocationId = ls.LocationId;
                        else return new DataResponseDTO
                        {
                            Success = false,
                            Message = "Location Not Found"
                        };
                        
                    }
                    if (ls.KidPrice.ToString() != null && ls.KidPrice >= 1)
                        existedls.KidsPrice = ls.KidPrice;

                    if (ls.ChildPrice.ToString() != null && ls.ChildPrice >= 1)
                        existedls.ChildsPrice = ls.ChildPrice;

                    if (ls.AdultPrice.ToString() != null && ls.AdultPrice >= 1)
                        existedls.AdultsPrice = ls.AdultPrice;

                }
            }

            await unitOfWork.SaveChangesAsync();
            
                return new DataResponseDTO
                {
                    Success = true,
                    Message = "Service updated Succesfully"
                };


        }

    }
}
