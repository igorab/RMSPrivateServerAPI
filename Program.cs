using Microsoft.EntityFrameworkCore;
using RMSPrivateServerAPI.Data;
using RMSPrivateServerAPI.Models.Lib;
using RMSPrivateServerAPI.Services;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

//  Добавление контекста базы данных
string? constr = builder.Configuration.GetConnectionString("DefaultConnection");
Action<DbContextOptionsBuilder>? optionsAction = (options) => options.UseSqlServer(constr);
builder.Services.AddDbContext<ApplicationDbContext>(optionsAction);
RMSData.ConnectionString = constr;

Debug.Assert(RMSData.ConnectionTest());

// Регистрация сервисов
builder.Services.AddScoped<RobotService>(); 
builder.Services.AddScoped<PPMService>(); 

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddOpenApi();

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
