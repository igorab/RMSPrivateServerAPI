using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Diagnostics;
using System.Reflection;

#pragma warning disable CS1591
using RMSPrivateServerAPI.Data;
using RMSPrivateServerAPI.Interfaces;
using RMSPrivateServerAPI.Repositories;
using RMSPrivateServerAPI.Models.Lib;
using RMSPrivateServerAPI.Models;
using RMSPrivateServerAPI.Services;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

public partial class Program
{
    public static void ConfigureServices(IServiceCollection services, ConfigurationManager configuration)
    {
        // Add services to the container.
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "RMS Private Server API", Version = "v1.0" });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
                options.UseInlineDefinitionsForEnums();
            }
        );

        services.AddApiVersioning(options =>
        {
            options.ReportApiVersions = true;
        });

        services.AddAutoMapper(typeof(Program));
        
        string? connectionString = configuration.GetConnectionString("DefaultConnection");

        // Добавление контекста базы данных
        services.AddDbContext<WmsDbContext>(op => op.UseNpgsql(connectionString));

        // Добавление контекста базы данных
        services.AddDbContext<RmsDbContext>(options => options.UseNpgsql(connectionString));

        IConfigurationSection configSection = configuration.GetSection("ConnectionStrings");
        services.Configure<DbSettings>(configSection);
        services.AddTransient<DatabaseConnectionFactory>();

        // Регистрация сервисов
        services.AddTransient<RobotRepository>();
        services.AddTransient<PPMRepository>();
        services.AddTransient<RobotTaskRepository>();

        services.RegisterDataAccessDependencies();

        services.AddTransient<PointService>();

        //services.AddHostedService<RobotTaskAssignmentService>();

        services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new RobotActionConverter());
            });        
    }

    /// <summary>
    /// Запуск сервиса создания задач
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    public static IHostBuilder CreateHostBuilder(string[] args)
    {
        return Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    var config = hostContext.Configuration;

                    string? connectionString = config.GetConnectionString("DefaultConnection");

                    services.AddDbContext<WmsDbContext>(op => op.UseNpgsql(connectionString));

                    IConfigurationSection configSection = config.GetSection("ConnectionStrings");
                    services.Configure<DbSettings>(configSection);
                    services.AddSingleton<DatabaseConnectionFactory>();

                    services.AddSingleton<IRobotService, RobotService>();
                    services.AddSingleton<IRobotRepository, RobotRepository>();

                    services.AddSingleton<IRobotTaskService, RobotTaskService>();
                    services.AddSingleton<IRobotTaskRepository, RobotTaskRepository>();

                    services.AddHostedService<RobotTaskAssignmentService>();
                });
    }

    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        Debug.Assert(RMSData.ConnectionTest());

        ConfigureServices(builder.Services, builder.Configuration);
        
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();
        
        // Определение маршрута
        app.MapGet("/api/v1/test", () =>
        {
            return Results.Ok("OK");
        });

        //CreateHostBuilder(args).Build().Run();

        var hostTask = Task.Run(() => CreateHostBuilder(args).Build().Run());

        app.Run();        
    }   
}
