
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TANE.Skabelon.Api.GenericRepositories;
using TANE.Skabelon.Api.Context;



namespace TANE.Skabelon.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddAutoMapper(typeof(Program).Assembly);

            // Add connection string og dbcontext 
            builder.Services.AddDbContext<SkabelonDbContext>(options =>
            {
                options.UseSqlServer(Environment.GetEnvironmentVariable("DATABASE_CONNECTION_STRING") ?? builder.Configuration.GetConnectionString("DefaultConnection"));

                Console.WriteLine(Environment.GetEnvironmentVariable("DATABASE_CONNECTION_STRING") ?? builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            // Create database
            using (var scope = builder.Services.BuildServiceProvider().CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<SkabelonDbContext>();
                context.Database.EnsureCreated();


                Console.WriteLine("Database created");
            }


            // Add repositories
            //var conn = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
           

            // Add controllers
            builder.Services.AddControllers();

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
        }   
    }
}
