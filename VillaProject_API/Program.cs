using Asp.Versioning;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Text;
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

builder.Services.AddDbContext<AppDbContext>(option =>
{
	option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultSQLConnection"));
});

builder.Services.AddResponseCaching();

builder.Services.AddScoped<IVillaRepository, VillaRepository>();
builder.Services.AddScoped<IVillaNumberRepository, VillaNumberRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddAutoMapper(typeof(MappingConfig));

builder.Services.AddApiVersioning(options =>
{
	// to make requests without explicitly specifying the API version
	options.AssumeDefaultVersionWhenUnspecified = true;
	options.DefaultApiVersion = new ApiVersion(1, 0);
	options.ReportApiVersions = true;
})
.AddApiExplorer(options =>
{
	//defines the format for the group name when exploring API versions
	options.GroupNameFormat = "'v'VVV";
	//substitute API version parameters in the route template with the corresponding API version value
	options.SubstituteApiVersionInUrl = true;
});

var key = builder.Configuration.GetValue<string>("ApiSettings:Secret");
builder.Services.AddAuthentication(options =>
{
	options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
	//HTTPS metadata validation
	options.RequireHttpsMetadata = true;
	options.SaveToken = true;
	options.TokenValidationParameters = new()
	{
		ValidateIssuerSigningKey = true,
		IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
		//Disables issuer validation
		ValidateIssuer = false,
		//Disables audience validation
		ValidateAudience = false
	};
});


builder.Services.AddControllers(
	 
	options => 
	{
		//For declining non supported format requests
		/* option.ReturnHttpNotAcceptable = true;*/
		options.CacheProfiles.Add("Default30", new CacheProfile
		{
			Duration = 30
		});
	})
	//For API Patch
	.AddNewtonsoftJson()
	//for xml format requests 
	.AddXmlDataContractSerializerFormatters();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
	options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
	{
		Description = "JWT Auth header using the Bearer scheme. Enter 'Bearer' [space] and then your token",
		Name = "Authorization",
		In = ParameterLocation.Header,
		Scheme = "Bearer"
	});
	options.AddSecurityRequirement(new OpenApiSecurityRequirement()
	{
		{
			new OpenApiSecurityScheme
			{
				Reference = new OpenApiReference
				{
					Type = ReferenceType.SecurityScheme,
					Id = "Bearer"
				},
				Scheme = "oauth2",
				Name = "Bearer",
				In = ParameterLocation.Header
			},
			new List<string>()
		}
	});
	//SWAGER documentation
	options.SwaggerDoc("v1", new OpenApiInfo
	{
		Version = "v1.0",
		Title = "Villa Project API",
		Description = "API for Villa managing",
		TermsOfService = new Uri("https://example.com/terms"),
		Contact = new OpenApiContact
		{
			Name = "Example",
			Url = new Uri("https://example.com/")
		},
		License = new OpenApiLicense
		{
			Name = "Example License",
			Url = new Uri("https://example.com/licence")
		}
	});
	options.SwaggerDoc("v2", new OpenApiInfo
	{
		Version = "v2.0",
		Title = "Villa Project API",
		Description = "API for Villa managing",
		TermsOfService = new Uri("https://example.com/terms"),
		Contact = new OpenApiContact
		{
			Name = "Example",
			Url = new Uri("https://example.com/")
		},
		License = new OpenApiLicense
		{
			Name = "Example License",
			Url = new Uri("https://example.com/licence")
		}
	});
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI(option =>
	{
		option.SwaggerEndpoint("/swagger/v1/swagger.json","VillaProjectV1");
		option.SwaggerEndpoint("/swagger/v2/swagger.json", "VillaProjectV2");
	});
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
