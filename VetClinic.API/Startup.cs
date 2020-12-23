using AutoMapper;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using VetClinic.API.ExtensionMethods;
using VetClinic.API.Filters;
using VetClinic.API.Middlewares;
using VetClinic.BLL;
using VetClinic.BLL.Services.Interfaces;
using VetClinic.BLL.Services.Realizations;
using VetClinic.DAL;
using VetClinic.DAL.Entities;
using VetClinic.DAL.Repositories.Interfaces;
using VetClinic.DAL.Repositories.Realizations;

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

            services.AddControllers(options => { options.Filters.Add(new ValidationFilter()); })
                .AddFluentValidation(options => { options.RegisterValidatorsFromAssemblyContaining<Startup>(); })
                .AddNewtonsoftJson();

            services.Configure<ApiBehaviorOptions>(options => { options.SuppressModelStateInvalidFilter = true; });
            services.AddSwaggerGen();
            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IProcedureService, ProcedureService>();
            services.AddScoped<IClientService, ClientService>();
            services.AddScoped<IPositionService, PositionService>();
            services.AddScoped<IDoctorService, DoctorService>();
            services.AddScoped<IServiceService, ServiceService>();
            services.AddScoped<IAnimalService, AnimalService>();


            services.AddServices();

            services.AddSwaggerConfig();
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, UserManager<User> userManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMiddleware<ExceptionMiddleware>();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            app.UseCustomSwaggerConfig();

            ApplicationUserSeeder.SeedUsers(userManager);
        }
    }
}