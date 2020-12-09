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
            /*
              JwtSecurityTokenHandler.DefaultMapInboundClaims = false;

              services.AddAuthentication(options =>
              {
                  options.DefaultScheme = "Cookies";
                  //options.DefaultAuthenticateScheme = "Cookies";
                  options.DefaultChallengeScheme = "oidc";
              })
                  .AddCookie("Cookies")
                  .AddOpenIdConnect("oidc", options =>
                  {
                      options.SignInScheme = "Cookies";
                      options.Authority = "https://localhost:5001";

                      options.ClientId = "angular_client";
                      options.ClientSecret = "angular_secret";
                      options.ResponseType = "code";

                      //options.Scope.Add("opdenid");

                      options.SaveTokens = true;
                      options.GetClaimsFromUserInfoEndpoint = true;
                      options.ClaimActions.MapJsonKey("role","role","role");
                      options.TokenValidationParameters.NameClaimType = "name";
                      options.TokenValidationParameters.RoleClaimType = "role";
                  });
            */
            services.AddAuthentication("Bearer")
                 .AddIdentityServerAuthentication(options =>
                 {
                     options.Authority = "https://localhost:5001";
                     //options.RequireHttpsMetadata = false;
                    // options.ApiName = "api1";
                 });
            /*
            services.AddAuthorization(options =>
            {
                options.AddPolicy("ApiScope", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("scope", "ApiOne");
                });
            });
            */

            string connection = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ApplicationContext>(options =>
                options.UseSqlServer(connection, builder =>
                    builder.MigrationsAssembly("VetClinic.DAL")));

            services.AddAutoMapper(typeof(Startup));

            services.AddControllers();

            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();

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
        }
    }
}