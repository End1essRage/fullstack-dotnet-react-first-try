using Microsoft.EntityFrameworkCore;
using TodosBackend.Data.DataAccess.Abstractions;
using TodosBackend.Web.Services.Abstractions;
using TodosBackend.Web.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using TodosBackend.Data.DataAccess;

namespace TodosBackend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //Подключаю DBContext, использую postgres и проводник Npgsql
            string connectionString = builder.Configuration.GetSection("AppSettings:ConnectionString").Value;
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(connectionString, b => b.MigrationsAssembly("TodosBackend.Web")));

            //Настройка политики кросс-доменных запросов
            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    policy =>
                    {
                        policy.WithOrigins("http://localhost:3000")
                         .AllowAnyHeader()
                         .AllowAnyMethod()
                         .AllowCredentials();
                    });
            });

            builder.Services.AddScoped<ITodoRepository, TodoRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
            builder.Services.AddTransient<ICodeConfirmationService, CodeConfirmationService>();

            builder.Services.AddStackExchangeRedisCache(options => {
                options.Configuration = "localhost";
                options.InstanceName = "local";
            });

            builder.Services.AddHttpContextAccessor();

            builder.Services.AddControllers();
            
            builder.Services.AddEndpointsApiExplorer();

            //Включение возможности авторизоваться для swagger
            builder.Services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Description = "Standart Authorization header using the Bearer sheme (\"Bearer {token}\")",
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });

                options.OperationFilter<SecurityRequirementsOperationFilter>();
            });

            //Настройка аутентификации
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                        .GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value)),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });
            var app = builder.Build();

            app.UseCors();
            
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            //app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}