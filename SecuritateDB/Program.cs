using ApplicationBusiness.AutoMapperConfig;
using ApplicationBusiness.CatalogsManager;
using ApplicationBusiness.CourierManager;
using ApplicationBusiness.CustomerManager;
using ApplicationBusiness.Interfaces;
using ApplicationBusiness.TokenManager;
using ApplicationBusiness.UserManager;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Repository;
using Repository.Interfaces;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var jwtIssuer = builder.Configuration.GetSection("Jwt:Issuer").Get<string>();
var jwtKey = builder.Configuration.GetSection("Jwt:Key").Get<string>();
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:4200")
                                                  .AllowAnyHeader()
                                                  .AllowAnyMethod(); ;
                      });
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
         ValidIssuer = jwtIssuer,
         ValidAudience = jwtIssuer,
         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
     };
 });

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<Context>(options =>
{
    options.UseOracle(builder.Configuration.
        GetConnectionString("OraDBConnection"));
});

builder.Services.AddScoped<IUnitOfWork, Context>();
builder.Services.AddScoped<ITokenManager, TokenManager>();
builder.Services.AddScoped<IUserManager, UserManager>();
builder.Services.AddScoped<ICourierManager, CourierManager>();
builder.Services.AddScoped<ICatalogsManager, CatalogsManager>();
builder.Services.AddScoped<ICustomerManager, CustomerManager>();

var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new OrderMap());
});

IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();

app.Run();
