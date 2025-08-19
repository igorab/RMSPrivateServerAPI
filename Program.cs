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
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        Debug.Assert(RMSData.ConnectionTest());

        // Add services to the container.
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen( options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "RMS Private Server API", Version = "v1.0" });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
                options.UseInlineDefinitionsForEnums();
            }            
        );

        builder.Services.AddApiVersioning(options =>
        {
            options.ReportApiVersions = true;
        });

        builder.Services.AddAutoMapper(typeof(Program));

        var configuration = builder.Configuration;
        string? connectionString = configuration.GetConnectionString("DefaultConnection");

        // Добавление контекста базы данных
        builder.Services.AddDbContext<WmsDbContext>(op => op.UseNpgsql(connectionString));

        // Добавление контекста базы данных
        builder.Services.AddDbContext<RmsDbContext>(options => options.UseNpgsql(connectionString));
        
        IConfigurationSection configSection = configuration.GetSection("ConnectionStrings");
        builder.Services.Configure<DbSettings>(configSection);
                        
        builder.Services.AddTransient<DatabaseConnectionFactory>();
        
        // Регистрация сервисов
        builder.Services.AddTransient<RobotRepository>();
        builder.Services.AddTransient<PPMRepository>();
        builder.Services.AddTransient<RobotTaskRepository>();
        
        builder.Services.RegisterDataAccessDependencies();

        builder.Services.AddTransient<PointService>(); 

        builder.Services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new RobotActionConverter());
            });

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
