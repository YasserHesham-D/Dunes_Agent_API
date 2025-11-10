using Domain.Interfaces.IModelsRepo;
using Domain.Models;
using Infrastructure.DBContext;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.ModelRepo
{
    public class PaymentMethodRepo : Repository<PaymentMethod>, IPaymentMethodRepo
    {
         private readonly AppDbContext _context;
         private readonly ILogger<PaymentMethod> _logger;
         public PaymentMethodRepo(AppDbContext context, ILogger<PaymentMethod> logger) : base(context, logger)
         {
           _context = context;
           _logger = logger;
         }
    
    }
}
