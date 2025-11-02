using Application.Services.AccountServices;
using Application.Services.Classes;
using Domain.Interfaces.IModelsRepo;
using Domain.Interfaces.IRepository;
using Domain.Interfaces.IServices;
using Domain.Interfaces.IUnitOfWork;
using Infrastructure.Repositories;
using Infrastructure.Repositories.ModelRepo;
using Infrastructure.Repository;
using Infrastructure.UnitOfWork;

namespace Presentation.ServiceExtensions
{
    public static class ServiceCollection
    {
        public static IServiceCollection ServicesCollection(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepo<>), typeof(Repository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IAccountsRepo, AccountsRepo>();
            services.AddScoped<IAccountServices, AccountServices>();

            services.AddScoped<IHotelRepo, HotelRepo>();
            services.AddScoped<IHotelService, HotelService>();



            return services;
        }
    }
}
