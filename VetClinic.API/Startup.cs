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
using VetClinic.DAL.Entities;
using Microsoft.AspNetCore.Identity;
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
            services.AddAuthentication("RefAndJWTToken")
            .AddIdentityServerAuthentication("RefAndJWTToken", options =>
            {
                options.Authority = "https://localhost:5005";
                options.ApiName = "VetClinicApi";
                options.ApiSecret = "angular_secret";
            });

            string connection = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ApplicationContext>(options =>
                options.UseSqlServer(connection, builder =>
                    builder.MigrationsAssembly("VetClinic.DAL")));
           

            IdentityBuilder builder = services.AddIdentityCore<User>();
            builder = new IdentityBuilder(typeof(User), typeof(IdentityRole), builder.Services);
            builder.AddEntityFrameworkStores<ApplicationContext>()
                .AddDefaultTokenProviders();

            services.AddScoped<IRoleValidator<IdentityRole>, RoleValidator<IdentityRole>>();
            services.AddScoped<RoleManager<IdentityRole>, RoleManager<IdentityRole>>();

            services.AddAutoMapper(typeof(Startup));

            services.AddControllers();

            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
            services.AddScoped<IUserService, UserService>();

            services.AddSwaggerConfig();
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseCustomSwaggerConfig();

            app.SeedUsersWithRoles(Configuration);
        }
    }
}