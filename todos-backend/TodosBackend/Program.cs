using Microsoft.EntityFrameworkCore;
using TodosBackend.Data;

namespace TodosBackend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            string connectionString = "User ID=user;Password=password;Server=localhost;Port=5433;Database=postgres;";
            builder.Services.AddDbContext<TodosDbContext>(options =>
                options.UseNpgsql(connectionString, b => b.MigrationsAssembly("TodosBackend")));//

            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    policy =>
                    {
                        policy.WithOrigins("http://localhost:3000");
                    });
            });

            builder.Services.AddScoped<ITodoRepository, FakeTodosRepository>();
            builder.Services.AddSingleton<FakeDbContext, FakeDbContext>();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();
            app.UseCors();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}