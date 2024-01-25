using Microsoft.EntityFrameworkCore;
using Serilog;
using VillaProject_API;
using VillaProject_API.Data;
using VillaProject_API.Repository;
using VillaProject_API.Repository.IRepository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Logger Serilog adding
Log.Logger = new LoggerConfiguration().MinimumLevel.Information()
    .WriteTo.File("log/villaLogs.txt", rollingInterval: RollingInterval.Month)
    .CreateLogger();
builder.Host.UseSerilog();

builder.Services.AddDbContext<AppDbContext>(option => { 
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultSQLConnection")); 
});
builder.Services.AddScoped<IVillaRepository, VillaRepository>();
builder.Services.AddScoped<IVillaNumberRepository, VillaNumberRepository>();

builder.Services.AddAutoMapper(typeof(MappingConfig));

builder.Services.AddControllers(
    //For declining non supported format requests 
    option => {/* option.ReturnHttpNotAcceptable = true;*/ })
    //For API Patch
    .AddNewtonsoftJson()
    //for xml format requests 
    .AddXmlDataContractSerializerFormatters();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

app.Run();
