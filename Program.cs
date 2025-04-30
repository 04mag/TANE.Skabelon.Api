
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

            {
                // Add connection string og dbcontext 
                builder.Services.AddDbContext<SkabelonDbContext>(options =>
                {
                    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));


            });
            }

            // Create datanase
            using (var scope = builder.Services.BuildServiceProvider().CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<SkabelonDbContext>();
             
               // context.Database.EnsureCreated();

            }

            // Add repositories
            //var conn = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
           

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
