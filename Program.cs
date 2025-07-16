using RMSPrivateServerAPI.Data;
using RMSPrivateServerAPI.Interfaces;
using RMSPrivateServerAPI.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(Program));

var configuration = builder.Configuration;

builder.Services.Configure<DbSettings>(configuration.GetSection("ConnectionString"));

builder.Services.AddTransient<DatabaseConnectionFactory>();

builder.Services.AddTransient<PPMRepository>();
builder.Services.AddTransient<RobotRepository>();
builder.Services.AddTransient<RobotTaskRepository>();

builder.Services.RegisterDataAccessDependencies();

/*
//  Добавление контекста базы данных
string? constr = builder.Configuration.GetConnectionString("DefaultConnection");
Action<DbContextOptionsBuilder>? optionsAction = (options) => options.UseSqlServer(constr);
builder.Services.AddDbContext<ApplicationDbContext>(optionsAction);
RMSData.ConnectionString = constr;
Debug.Assert(RMSData.ConnectionTest());
*/

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
