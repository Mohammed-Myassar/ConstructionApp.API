using Asp.Versioning;
using Buisnes_Logic.Interface;
using Buisnes_Logic.Services;
using Buisnes_Logic.Validation;
using BuisnesLogic.Data_Transfer_Object;
using BuisnesLogic.Interface_repository;
using BuisnesLogic.Interface_Services;
using BuisnesLogic.Services;
using BuisnesLogic.Validation;
using Data.DbContexts;
using DbRepositories.Repositories;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers().AddNewtonsoftJson();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add Service Db Context By Connection String
builder.Services.AddDbContext<ConstructionContext>(
    options => options.UseSqlServer(
        builder.Configuration["ConnectionStrings:DefaultConnection"])
);

// Add Service Auto Mapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Add Service Logger by use Serilog
Log.Logger = new LoggerConfiguration()
           .MinimumLevel.Debug()
           .WriteTo.Console()
           .WriteTo.File(builder.Configuration["Logger:PathFileToLogger"],
                            rollingInterval: RollingInterval.Day)
           .CreateLogger();
builder.Host.UseSerilog();

// Add all base Services => (Buisnes Logic)
builder.Services.AddScoped<IRepository, Repository>();
builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<ITaskService, TaskService>();
builder.Services.AddScoped<IResourceService, ResourceService>();
builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddScoped<ILoginService, LoginService>();

// Add Services Validator
builder.Services.AddScoped<IValidatorService<ConstructionProjectDTO>,
    ValidatorService<ConstructionProjectDTO>>();
builder.Services.AddScoped<IValidatorService<EmployeeDTO>,
    ValidatorService<EmployeeDTO>>();
builder.Services.AddScoped<IValidatorService<PaymentTransactionDTO>,
    ValidatorService<PaymentTransactionDTO>>();
builder.Services.AddScoped<IValidatorService<ProjectTaskDTO>,
    ValidatorService<ProjectTaskDTO>>();
builder.Services.AddScoped<IValidatorService<ResourceDTO>,
    ValidatorService<ResourceDTO>>();
builder.Services.AddScoped<IValidatorService<ResourceUsageDTO>,
    ValidatorService<ResourceUsageDTO>>();

// Add Services Fluent Validation
builder.Services.AddScoped<IValidator<ConstructionProjectDTO>, ProjectValidator>();
builder.Services.AddScoped<IValidator<EmployeeDTO>, EmployeeValidator>();
builder.Services.AddScoped<IValidator<PaymentTransactionDTO>, PaymentTransactionValidator>();
builder.Services.AddScoped<IValidator<ProjectTaskDTO>, TaskValidator>();
builder.Services.AddScoped<IValidator<ResourceDTO>, ResourceValidator>();
builder.Services.AddScoped<IValidator<ResourceUsageDTO>, ResourceUsageValidator>();

// Add Service JWT (Authentication) Validation
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(option =>
    {
        option.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidIssuer = builder.Configuration["Authentication:Issuer"],
            ValidAudience = builder.Configuration["Authentication:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Authentication:SecretKey"])),

            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true
        };
    });

// Add Service Vesioning
builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;

}).AddApiExplorer();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
