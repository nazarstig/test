using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using VetClinic.ExtensionMethods;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using VetClinic.DAL;
using AutoMapper;

namespace VetClinic.API
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
            string connection = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ApplicationContext>(options =>
                options.UseSqlServer(connection, b => b.MigrationsAssembly("VetClinic.DAL")));
            services.AddAutoMapper(typeof(Startup));
            services.AddControllers();
            services.AddSwaggerConfig();
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseCustomSwaggerConfig();
        }
    }
}
