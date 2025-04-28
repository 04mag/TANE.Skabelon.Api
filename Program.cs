
using Microsoft.EntityFrameworkCore;
using TANE.Skabelon.Api.Repositories;
using TANE.Skabelon.Api.GenericRepositories;
using TANE.Skabelon.Api.Context;
using TANE.Skabelon.Api.Repository;


namespace TANE.Skabelon.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            {
                // Add connection string og dbcontext 
                builder.Services.AddDbContext<SkabelonDbContext>(options =>
                {
                    options.UseSqlServer(Environment.GetEnvironmentVariable("DATABASE_CONNECTION_STRING") ?? builder.Configuration.GetConnectionString("DefaultConnection"));
                    
                    
                });
            }

            // Create datanase
            using (var scope = builder.Services.BuildServiceProvider().CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<SkabelonDbContext>();
                context.Database.EnsureCreated();

            }

            // Add repositories
            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            builder.Services.AddScoped<IDagSkabelonRepository, DagSkabelonRepository>();
            builder.Services.AddScoped<ITurSkabelonRepository, TurSkabelonRepository>();
            builder.Services.AddScoped<IRejseplanSkabelonRepository, RejseplanSkabelonRepository>();

            // Add controllers
            builder.Services.AddControllers();

            // 

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
