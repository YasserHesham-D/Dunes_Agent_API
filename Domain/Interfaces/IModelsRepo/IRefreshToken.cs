using Domain.Interfaces.IRepository;
using Domain.Models.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.IModelsRepo
{
    public interface IRefreshToken : IRepo<RefreshToken>
    {
        public Task DeleteAsync(int id);

    }
}
