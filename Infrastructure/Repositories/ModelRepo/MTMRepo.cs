using Domain.Interfaces.IModelsRepo;
using Domain.Interfaces.IRepository;
using Domain.Models.MTM;
using Infrastructure.DBContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.ModelRepo
{
    public class MTMRepo(AppDbContext context) : IMTMRepo
    {
        public async Task AddLocationServices(LocationServices locationServices) =>  await context.LocationServices.AddAsync(locationServices);

        //public async Task<LocationServices> GetLocationServicesById(Guid Id)
        //{
        //   return await context.LocationServices.FindAsync(Id);
        //}
        public async Task<LocationServices> GetLocationServicesById(Guid Id)
        {

            return await context.LocationServices.FirstOrDefaultAsync(x=>x.Id == Id && x.IsDeleted == false);

        }
    }
}
