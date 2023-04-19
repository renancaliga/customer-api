using AutoMapper;
using CustomerAPI.Application.Models;
using CustomerAPI.Domain.Entities;
using CustomerAPI.Domain.Interfaces;
using CustomerAPI.Infra.Data.Context;
using CustomerAPI.Infra.Data.Repository;
using CustomerAPI.Service.Services;
using Microsoft.EntityFrameworkCore;

namespace CustomerAPI.Application
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddCors();
            services.AddControllers();         
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddDbContext<SqlContext>(
                opt => opt.UseSqlServer(Configuration.GetConnectionString("connectionString")));
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddScoped<IBaseRepository<Customer>, BaseRepository<Customer>>();
            services.AddScoped<IBaseService<Customer>, BaseService<Customer>>();

            services.AddSingleton(new MapperConfiguration(config =>
            {
                config.CreateMap<CreateCustomerModel, Customer>();
                config.CreateMap<UpdateCustomerModel, Customer>();
                config.CreateMap<Customer, CustomerModel>();
            }).CreateMapper());

        }

        public void Configure(WebApplication app, IWebHostEnvironment environment)
        {

            var url = "http://localhost:8080/";


            app.UseCors(b => b.WithOrigins(url));

            if (app.Environment.IsDevelopment())
            {

                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();
        }
    }
}
