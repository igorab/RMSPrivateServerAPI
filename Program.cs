using Microsoft.EntityFrameworkCore;
using RMSPrivateServerAPI.Data;
using RMSPrivateServerAPI.Interfaces;
using RMSPrivateServerAPI.Models.Lib;
using RMSPrivateServerAPI.Repositories;
using System.Diagnostics;

public partial class Program
{ 
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddAutoMapper(typeof(Program));

        var configuration = builder.Configuration;

        var configConStr = configuration.GetSection("ConnectionStrings");
        builder.Services.Configure<DbSettings>( configConStr);

        ////  Добавление контекста базы данных
        //string? constr =  configuration.GetConnectionString("DefaultConnection");
        //Action<DbContextOptionsBuilder>? optionsAction = (options) => options.UseSqlServer(constr);
        //builder.Services.AddDbContext<ApplicationDbContext>(optionsAction);
        //RMSData.ConnectionString = constr;
        //Debug.Assert(RMSData.ConnectionTest());
        
        builder.Services.AddTransient<DatabaseConnectionFactory>();

        builder.Services.AddTransient<RobotRepository>();
        builder.Services.AddTransient<PPMRepository>();
        builder.Services.AddTransient<RobotTaskRepository>();

        builder.Services.RegisterDataAccessDependencies();
       
        //// Регистрация сервисов
        //builder.Services.AddScoped<RobotService>(); 
        //builder.Services.AddScoped<PPMService>(); 
        //builder.Services.AddScoped<RobotTaskService>();

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
