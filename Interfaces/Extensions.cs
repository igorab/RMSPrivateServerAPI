using RMSPrivateServerAPI.Repositories;
using RMSPrivateServerAPI.Services;

namespace RMSPrivateServerAPI.Interfaces
{
    public static class Extensions
    {
        public static IServiceCollection RegisterDataAccessDependencies(this IServiceCollection services)
        {
            services.AddTransient<IRobotRepository, RobotRepository>();

            services.AddTransient<IRobotService, RobotService>();

            return services;
        }
    }
}
