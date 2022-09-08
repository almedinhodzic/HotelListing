using HotelListing.API.Business.Contracts;
using HotelListing.API.Business.Repository;
using HotelListing.API.Configurations;
using HotelListing.API.Data;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace HotelListing.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Get connection string
            var connectionString = builder.Configuration.GetConnectionString("Default");

            // Connect to SQL server
            builder.Services.AddDbContext<HotelListingDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            // Add AutoMapper
            builder.Services.AddAutoMapper(typeof(MapperConfig));

            // Add Repository Classes
            builder.Services
                .AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            builder.Services
                .AddScoped<ICountryRepository, CountryRepository>();

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Adding serilog for logging
            builder.Host.UseSerilog(
                (ctx, lc) => lc.WriteTo.Console()
                            .ReadFrom.Configuration(ctx.Configuration));

            // Adding cors options
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", b => b.AllowAnyHeader()
                                                    .AllowAnyOrigin()
                                                    .AllowAnyMethod());
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            // Use new cors
            app.UseCors("AllowAll");

            app.MapControllers();

            app.Run();
        }
    }
}