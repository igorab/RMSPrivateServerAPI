using RMSPrivateServerAPI.Repositories;
using RMSPrivateServerAPI.Services;

namespace RMSPrivateServerAPI.Interfaces;

/// <summary>
/// Extension methods
/// </summary>
public static class Extensions
{
    /// <summary>
    /// Регистрация сервисов
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection RegisterDataAccessDependencies(this IServiceCollection services)
    {
        services.AddTransient<IRobotRepository, RobotRepository>();
        services.AddTransient<IRobotService, RobotService>();

        services.AddTransient<IRobotTaskRepository, RobotTaskRepository>();
        services.AddTransient<IRobotTaskService, RobotTaskService>();

        services.AddTransient<IPPMRepository, PPMRepository>();
        services.AddTransient<IPPMService, PPMService>();

        return services;
    }
}
