using Application.Services.AccountServices;
using Application.Services.HotelService;
using Application.Services.LocationService;
using Application.Services.ServicesService;
using Domain.Interfaces.IModelsRepo;
using Domain.Interfaces.IRepository;
using Domain.Interfaces.IUnitOfWork;
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
            services.AddScoped<IRefreshToken, RTokenRepo>();
            services.AddScoped<ITokenBlackListService, InMemoryTokenBlacklistService>();
            

            services.AddScoped<IHotelRepo, HotelRepo>();
            services.AddScoped<IHotelService, HotelService>();

            services.AddScoped<ILocationRepo, LocationRepo>();
            services.AddScoped<ILocationService, LocationService>();

            services.AddScoped<IServicesRepo, ServicesRepo>();
            services.AddScoped<IServicesService, ServicesService>();


            return services;
        }
    }
}
