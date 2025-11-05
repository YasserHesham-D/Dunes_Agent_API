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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.ServicesService
{
    public class ServicesService(UserManager<Employee> userManager,IServicesRepo serviceRepo,IUnitOfWork unitOfWork) : IServicesService
    {
        public async Task<bool> AddNewServiceAsync(AddNewServiceRequest request, string UserId)
        {
            var user = await userManager.FindByIdAsync(UserId);
            
            if (user == null)
                return false;

            var NewService = new Service
            {
                Duration = request.Duration,
                TimeDuration = request.TimeDuration,
                Description = request.ServiceDescription,
                EmployeeAddedId = user.Id,
                ServiceName = request.ServiceName,
                LocationServices = new List<LocationServices>()
            };

            if(request.locationServices.Count > 0 )
            {
                foreach(var ls in request.locationServices)
                {
                    NewService.LocationServices.Add(new LocationServices
                    {
                        LocationId = ls.LocationId,
                        AdultsPrice = ls.AdultPrice,
                        ChildsPrice = ls.ChildPrice,
                        KidsPrice = ls.KidPrice

                    });
                }
            }

            await serviceRepo.AddAsync(NewService);
            await unitOfWork.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteServiceAsync(Guid id)
        {
            var Exist = await serviceRepo.GetByIdAsync(id);
            if (Exist == null) return false;

            var locationservices = Exist.LocationServices.FirstOrDefault();

           // await serviceRepo.DeleteAsync(locationservices);


            await serviceRepo.DeleteAsync(Exist);
            await unitOfWork.SaveChangesAsync();

            return true;
        }

        public async Task<Pagination<GetAllServicesDTO>> GetAllServicesPaginated
            (string? empname, string? serviceName,string? locationname ,
            int? duration, string sortColumn, bool isAscending, int page, int pageSize, string columnName = "EntryDate")
        {

            var services =  serviceRepo.GetAll();

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
                services = services.Where(x => x.LocationServices.Select(x => x.Location.Name).Contains(locationname));
            }

            var result = serviceRepo.Sort(services,sortColumn, isAscending);



            var Services = result.Select(x => new GetAllServicesDTO
            {
                serviceName = x.ServiceName,
                EmployeeName = x.Employee.UserName,
                LocationName = x.LocationServices.Select(x => x.Location.Name).FirstOrDefault(),
                Duration = x.Duration,
                ServiceDescription = x.Description,
                servicelocationdtos = x.LocationServices.Select(x => new Servicelocationdto
                {
                    
                    Adult = x.AdultsPrice,
                    Child = x.ChildsPrice,
                    Kid = x.KidsPrice
                }).ToList()

            });

            var paginatedServices = await Pagination<GetAllServicesDTO>.CreateAsync(Services, page, pageSize);

            return  paginatedServices;
            
        }

        public async Task<bool> PatchService(Guid id, UpdateServiceRequest request)
        {
            var exist = await serviceRepo.GetByIdAsync(id);

            var service = await serviceRepo.GetAll()
                .Include(s => s.LocationServices)
                    .ThenInclude(sl => sl.Location)
                .Include(s => s.Employee)
                .FirstOrDefaultAsync(s => s.Id == id);

            var sl = service.LocationServices.FirstOrDefault();


            if (exist == null) return false;
            if (request == null) return false;

            if(!string.IsNullOrEmpty(request.ServiceName)) 
                exist.ServiceName = request.ServiceName;

            if(!string.IsNullOrEmpty(request.ServiceDescription))
                exist.Description = request.ServiceDescription;

            if (!string.IsNullOrEmpty(request.Duration.ToString()))
                exist.Duration = (int)request.Duration;
               
            if(!string.IsNullOrEmpty(request.TimeDuration.ToString()))
                exist.TimeDuration = (Domain.Enums.TimeDuration)request.TimeDuration;

            if (request.LocationId != Guid.Empty && request.LocationId != null)
                sl.LocationId = (Guid)request.LocationId;

            if (request.KidsPrice != null)
               sl.KidsPrice = (decimal)request.KidsPrice;

            if (request.ChildPrice != null)
                sl.ChildsPrice = (decimal)request.ChildPrice;

            if (request.AdultsPrice != null)
                sl.AdultsPrice = (decimal)request.AdultsPrice;

            await serviceRepo.UpdateAsync(exist);
            await unitOfWork.SaveChangesAsync();

            return true;

        }
    }
}
