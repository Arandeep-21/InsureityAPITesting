using InsureityAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using InsureityAPI.Repository;
using InsureityAPI.Services;
using InsureityAPI.Controllers;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Text.Json.Serialization;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

builder.Services.AddControllers().AddJsonOptions(x =>x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddDbContext<InsureityContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddTransient<IAdminRepo<LoginDetails>,AdminRepo>();
builder.Services.AddTransient<IAgentRepo<LoginDetails>, AgentRepo>();
builder.Services.AddSingleton<AuthController>();
builder.Services.AddTransient<IConsumerRepo<Consumer>, ConsumerRepo>();
builder.Services.AddTransient<IConsumerService<Consumer>, ConsumerService>();
builder.Services.AddTransient<IBusinessRepo<Business>, BusinessRepo>();
builder.Services.AddTransient<IBusinessService<Business>, BusinessService>();
builder.Services.AddTransient<IPolicyMasterRepo<PolicyMaster>, PolicyMasterRepo>();
builder.Services.AddTransient<IPolicyMasterService<PolicyMaster>, PolicyMasterService>();
builder.Services.AddTransient<IPropertyRepo<Property>, PropertyRepo>();
builder.Services.AddTransient<IPropertyService<Property>, PropertyService>();
builder.Services.AddTransient<IBusinessMastersRepo, BusinessMastersRepo>();
builder.Services.AddTransient<IPropertyMastersRepo, PropertyMastersRepo>();
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => {
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description="Standard Authorization header using the Bearer Scheme",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    options.OperationFilter<SecurityRequirementsOperationFilter>();
});




builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddCors(c =>
{
    c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});

builder.Services.AddAutoMapper(typeof(Program).Assembly);



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowOrigin");

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
