using Domain.Interfaces.IRepository;
using Infrastructure.Repository;

namespace Presentation.ServiceExtensions
{
    public static class ServiceCollection
    {
        public static IServiceCollection ServicesCollection(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepo<>), typeof(Repository<>));
            
            return services;
        }
    }
}
