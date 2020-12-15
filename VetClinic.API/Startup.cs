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
using VetClinic.BLL.Services;

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
                 options.Authority = "https://localhost:5001";
                 options.ApiName = "VetClinicApi";
                 options.ApiSecret = "angular_secret";
             });

            string connection = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ApplicationContext>(options =>
                options.UseSqlServer(connection, builder =>
                    builder.MigrationsAssembly("VetClinic.DAL")));

            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationContext>()
                .AddDefaultTokenProviders();

            services.AddAutoMapper(typeof(Startup));

            services.AddControllers();

            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<AbstractValidator<User>, AppUserValidator>();

            services.AddSwaggerConfig();

            services.AddTransient<ProcedureService>();
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
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Procedure}/{action=GetProcedure}/{id=3}");
            });

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

       
            //app.UseEndpoints(endpoints =>
            //{
            //    // ����������� ���������
            //    endpoints.MapControllerRoute(
            //        name: "default",
            //        pattern: "{controller=Home}/{action=Index}/{id?}");
            //});


            app.UseCustomSwaggerConfig();

            app.SeedUsersWithRoles(Configuration);
        }
    }
}