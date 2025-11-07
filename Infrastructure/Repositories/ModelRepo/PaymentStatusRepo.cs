using Domain.Interfaces.IModelsRepo;
using Domain.Models;
using Infrastructure.DBContext;
using Infrastructure.Repository;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.ModelRepo
{
    public class PaymentStatusRepo : Repository<PaymentStatus>, IPaymentStatusRepo
    {
        private readonly AppDbContext _context;
        private readonly ILogger<PaymentStatus> _logger;
        public PaymentStatusRepo(AppDbContext context, ILogger<PaymentStatus> logger) : base(context, logger)
        {
            _context = context;
            _logger = logger;
        }
    }
    
}
