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

        services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new RobotActionConverter());
            });

        services.AddHostedService<RobotTaskAssignmentService>();
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

        app.Run();
    }
}
