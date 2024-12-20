using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using backend.Infrastructure.src.RepoImplementations;
using backend.Business.src.Implementations;
using backend.Domain.src.Abstractions;
using backend.Business.src.Abstractions;
using backend.Business.src.Shared;
using backend.Infrastructure.src.Database;
using System.Text;
using backend.Infrastructure.src.Middleware;
using backend.Infrastructure.src.AuthorizationRequirement;
using Microsoft.AspNetCore.Authorization;

var builder = WebApplication.CreateBuilder(args);

// Add Automapper DI
builder.Services.AddAutoMapper(typeof(Program).Assembly);

// Add db context
builder.Services.AddDbContext<DatabaseContext>();

// Add service DI
builder.Services
.AddScoped<IUserRepo, UserRepo>()
.AddScoped<IUserService, UserService>()
.AddScoped<IAuthService, AuthService>();

builder.Services.AddScoped<ICategoryRepo, CategoryRepo>();
builder.Services.AddScoped<ICategoryService,CategoryService>();
builder.Services.AddScoped<IProductRepo,ProductRepo>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IOrderRepo, OrderRepo>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IOrderProductRepo, OrderProductRepo>();
builder.Services.AddScoped<IOrderProductService, OrderProductService>();
builder.Services.AddScoped<IPaymentRepo, PaymentRepo>();
builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddScoped<IShipmentRepo, ShipmentRepo>();
builder.Services.AddScoped<IShipmentService, ShipmentService>();
builder.Services.AddScoped<IUserCardRepo, UserCardRepo>();
builder.Services.AddScoped<IUserCardService, UserCardService>();
builder.Services.AddScoped<IReviewRateRepo, ReviewRateRepo>();
builder.Services.AddScoped<IReviewRateService, ReviewRateService>();



// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Bearer token authentication",
        Name = "Authentication",
        In = ParameterLocation.Header
    });
    options.OperationFilter<SecurityRequirementsOperationFilter>();
});

// Config the authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer
(
    options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = "anhnguyen-ecommerce-backend",
            ValidateAudience = false,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("GRVm0^v3uFIWsJuW51hanwMsocR40U2@l6%(jocj0sH8vnX^1G")),
            ValidateIssuerSigningKey = true
        };
    }
);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowOrigin",
        builder => builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

builder.Services
.AddSingleton<ErrorHandlerMiddleware>()
.AddSingleton<IAuthorizationHandler,OwnerOnlyRequirementHandler>();

// Config route
builder.Services.Configure<RouteOptions>(options =>
{
    options.LowercaseUrls = true;
});

// add policy based requirement handler to serice


builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("OwnerOnly", policy => policy.Requirements.Add(new OwnerOnlyRequirement()));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ErrorHandlerMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors("AllowOrigin");

app.MapControllers();

app.Run();