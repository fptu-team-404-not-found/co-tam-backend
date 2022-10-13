using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Services;
using Repositories;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using CoTamApp.Controllers;
using BusinessObject.Models;
using Repositories.ValidationHandling;
using Repositories.IRepositories;
using Services.IServices;
using System.Reflection;
using Services.ValidationHandling;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<cotamContext>(options =>
    options.UseSqlServer(builder
    .Configuration
    .GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();

builder.Services.AddControllersWithViews().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo 
    { 
        Title = "Cô Tấm API", 
        Version = "v1",
        Description = "API for Cô Tấm Project",
        Contact = new OpenApiContact
        {
            Name = "Contact Developers",
            Url = new Uri("https://github.com/fptu-team-404-not-found")
        },
        License = new OpenApiLicense
        {
            Name = "License GNU General Public License v3.0",
            Url = new Uri("https://github.com/fptu-team-404-not-found/co-tam-backend/blob/main/LICENSE")
        }
    });
    c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Standard Authorization header using the Bearer scheme. Example: \"bearer {token}\"",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
    c.OperationFilter<SecurityRequirementsOperationFilter>();
});

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
            .AddEntityFrameworkStores<cotamContext>()
            .AddDefaultTokenProviders();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = GoogleDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
}).AddGoogle(options =>
        {
            options.ClientId = "544071594305-u1hf14eq178ifnq3ldd92bu7oifdjij1.apps.googleusercontent.com";
            options.ClientSecret = "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";
        });


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            System.Text.Encoding.ASCII.GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value)),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                    };
                });

//for DI
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IAuthRepository, AuthRepository>();

builder.Services.AddScoped<IAuthCustomerService, AuthCustomerService>();
builder.Services.AddScoped<IAuthCustomerRepository, AuthCustomerRepository>();

builder.Services.AddScoped<IAuthHouseworkerService, AuthHouseworkerService>();
builder.Services.AddScoped<IAuthHouseworkerRepository, AuthHouseworkerRepository>();

builder.Services.AddScoped<IHouseRepository, HouseRepository>();
builder.Services.AddScoped<IHouseService, HouseService>();
builder.Services.AddScoped<HouseValidation>();

builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<CustomerValidation>();

builder.Services.AddScoped<IPromotionRepository, PromotionRepository>();
builder.Services.AddScoped<IPromotionService, PromotionService>();
builder.Services.AddScoped<PromotionValidation>();

builder.Services.AddScoped<IAdminRepository, AdminRepository>();
builder.Services.AddScoped<IAdminService, AdminService>();
builder.Services.AddScoped<ValidationAdminManager>();

builder.Services.AddScoped<IManagerRepository, ManagerReposiotory>();
builder.Services.AddScoped<IManagerService, ManagerService>();
builder.Services.AddScoped<ManagerValidation>();

builder.Services.AddScoped<IServiceRepository, ServiceRepository>();
builder.Services.AddScoped<IServiceService, ServiceService>();
builder.Services.AddScoped<ServiceValidation>();


builder.Services.AddScoped<ValidationAdminManager>();
builder.Services.AddScoped<PromotionValidation>();
builder.Services.AddScoped<ManagerValidation>();
builder.Services.AddScoped<AdminValidation>();

builder.Services.AddScoped<IExtraServiceRepository, ExtraServiceRepository>();
builder.Services.AddScoped<IExtraServiceService, ExtraServiceService>();
builder.Services.AddScoped<ExtraServiceValidation>();

builder.Services.AddScoped<IAreaRepository, AreaRepository>();
builder.Services.AddScoped<IAreaService, AreaService>();
builder.Services.AddScoped<AreaValidation>();

builder.Services.AddScoped<IHouseWorkerRepository, HouseWorkerRepository>();
builder.Services.AddScoped<IHouseWorkerService, HouseWorkerService>();
builder.Services.AddScoped<HouseWorkerValidation>();

builder.Services.AddScoped<IInformationRepository, InformationRepository>();
builder.Services.AddScoped<IInformationService, InformationService>();
builder.Services.AddScoped<InformationValidation>();

builder.Services.AddScoped<IBuildingRepository, BuildingRepository>();
builder.Services.AddScoped<IBuildingService, BuildingService>();
builder.Services.AddScoped<BuildingValidation>();

builder.Services.AddScoped<IPackageRepository, PackageRepository>();
builder.Services.AddScoped<IPackageService, PackageService>();
builder.Services.AddScoped<PackageValidation>();

builder.Services.AddScoped<ICustomerPromotionRepository, CustomerPromotionRepository>();
builder.Services.AddScoped<ICustomerPromotionService, CustomerPromotionService>();
builder.Services.AddScoped<CustomerPromotionValidation>();

builder.Services.AddScoped<AuthController>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseSwagger();

app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("v1/swagger.json", "Cô Tấm API V1");
});

app.UseHttpsRedirection();

app.UseCors(c => c.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();