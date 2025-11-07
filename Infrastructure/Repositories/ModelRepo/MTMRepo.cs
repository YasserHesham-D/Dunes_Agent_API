using Domain.Interfaces.IModelsRepo;
using Domain.Interfaces.IRepository;
using Domain.Models.MTM;
using Infrastructure.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.ModelRepo
{
    public class MTMRepo(AppDbContext context) : IMTMRepo
    {
        public void DeleteServiceLocation(LocationServices locationServices)
        {
             context.LocationServices.Remove(locationServices);
        }

    }
}
