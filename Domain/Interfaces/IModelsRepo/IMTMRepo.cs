using Domain.Models.MTM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.IModelsRepo
{
    public interface IMTMRepo
    {
        Task<LocationServices> GetLocationServicesById(Guid Id);
        Task AddLocationServices(LocationServices locationServices);

    }
}
