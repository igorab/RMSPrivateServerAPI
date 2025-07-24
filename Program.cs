using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using RMSPrivateServerAPI.Data;
using RMSPrivateServerAPI.Interfaces;
using RMSPrivateServerAPI.Models.Lib;
using RMSPrivateServerAPI.Repositories;
using System.Diagnostics;
using System.Reflection;

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

        // Добавление контекста базы данных
        builder.Services.AddDbContext<ApplicationDbContext>(op => op.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));


        IConfigurationSection configSection = configuration.GetSection("ConnectionStrings");
        builder.Services.Configure<DbSettings>(configSection);
                        
        builder.Services.AddTransient<DatabaseConnectionFactory>();
        
        // Регистрация сервисов
        builder.Services.AddTransient<RobotRepository>();
        builder.Services.AddTransient<PPMRepository>();
        builder.Services.AddTransient<RobotTaskRepository>();
        
        builder.Services.RegisterDataAccessDependencies();
               
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            //app.MapOpenApi();
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
