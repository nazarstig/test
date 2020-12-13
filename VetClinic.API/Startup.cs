using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using AutoMapper;
using VetClinic.API.ExtensionMethods;
using VetClinic.DAL;
using VetClinic.DAL.Repositories.Interfaces;
using VetClinic.DAL.Repositories.Realizations;
using VetClinic.BLL.Services.Interfaces;
using VetClinic.BLL.Services.Realizations;

namespace VetClinic.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            string connection = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ApplicationContext>(options =>
                options.UseSqlServer(connection, builder =>
                    builder.MigrationsAssembly("VetClinic.DAL")));

            services.AddAutoMapper(typeof(Startup));

            services.AddControllers();

            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
            services.AddScoped<IPositionService, PositionService>();

            services.AddSwaggerConfig();
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            app.UseCustomSwaggerConfig();
        }
    }
}