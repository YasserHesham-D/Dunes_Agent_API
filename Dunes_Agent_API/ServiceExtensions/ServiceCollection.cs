using Domain.Interfaces.IRepository;
using Domain.Interfaces.IUnitOfWork;
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

            return services;
        }
    }
}
