using IdentityModel;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Events;
using System;
using System.Linq;
using System.Security.Claims;
using VetClinic.DAL;
using VetClinic.DAL.Entities;

namespace VetClinic.API.ExtensionMethods
{
    public static class AppExtensions
    {
        public static void SeedUsersWithRoles(this IApplicationBuilder app, IConfiguration config)
        {
            
            string connectionString = config.GetConnectionString("DefaultConnection");

            var services = new ServiceCollection();
            services.AddLogging();
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console(restrictedToMinimumLevel: LogEventLevel.Debug)
                .CreateLogger();
            services.AddDbContext<ApplicationContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationContext>()
                .AddDefaultTokenProviders();
            
           
            using (var serviceProvider = services.BuildServiceProvider())
            {
                using (var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
                {
                    var context = scope.ServiceProvider.GetService<ApplicationContext>();
                    //context.Database.Migrate();

                    var roleMgr = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                    var client = roleMgr.FindByNameAsync("client").Result;
                    if (client == null)
                    {
                        client = new IdentityRole
                        {
                            Name = "member"
                        };
                        _ = roleMgr.CreateAsync(client).Result;
                    }

                    var admin = roleMgr.FindByNameAsync("admin").Result;
                    if (admin == null)
                    {
                        admin = new IdentityRole
                        {
                            Name = "admin"
                        };
                        _ = roleMgr.CreateAsync(admin).Result;
                    }

                    var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
                    var alice = userMgr.FindByNameAsync("alice").Result;
                    if (alice == null)
                    {
                        alice = new User
                        {
                            UserName = "alice",
                            Email = "AliceSmith@email.com",
                            EmailConfirmed = true,
                        };
                        var result = userMgr.CreateAsync(alice, "Pass123$").Result;
                        if (!result.Succeeded)
                        {
                            throw new Exception(result.Errors.First().Description);
                        }

                        result = userMgr.AddClaimsAsync(alice, new Claim[]{
                        new Claim(JwtClaimTypes.Name, "Alice Smith"),
                        new Claim(JwtClaimTypes.GivenName, "Alice"),
                        new Claim(JwtClaimTypes.FamilyName, "Smith"),
                        new Claim(JwtClaimTypes.WebSite, "http://alice.com"),
                    }).Result;
                        if (!result.Succeeded)
                        {
                            throw new Exception(result.Errors.First().Description);
                        }

                        if (!userMgr.IsInRoleAsync(alice, admin.Name).Result)
                        {
                            _ = userMgr.AddToRoleAsync(alice, admin.Name).Result;
                        }

                        Log.Debug("alice created");
                    }
                    else
                    {
                        Log.Debug("alice already exists");
                    }

                    var bob = userMgr.FindByNameAsync("bob").Result;
                    if (bob == null)
                    {
                        bob = new User
                        {
                            UserName = "bob",
                            Email = "BobSmith@email.com",
                            EmailConfirmed = true
                        };
                        var result = userMgr.CreateAsync(bob, "Pass123$").Result;
                        if (!result.Succeeded)
                        {
                            throw new Exception(result.Errors.First().Description);
                        }

                        result = userMgr.AddClaimsAsync(bob, new Claim[]{
                        new Claim(JwtClaimTypes.Name, "Bob Smith"),
                        new Claim(JwtClaimTypes.GivenName, "Bob"),
                        new Claim(JwtClaimTypes.FamilyName, "Smith"),
                        new Claim(JwtClaimTypes.WebSite, "http://bob.com"),
                        new Claim("location", "somewhere")
                    }).Result;
                        if (!result.Succeeded)
                        {
                            throw new Exception(result.Errors.First().Description);
                        }

                        if (!userMgr.IsInRoleAsync(bob, client.Name).Result)
                        {
                            _ = userMgr.AddToRoleAsync(bob, client.Name).Result;
                        }

                        Log.Debug("bob created");
                    }
                    else
                    {
                        Log.Debug("bob already exists");
                    }
                }
            }

        }
    }
}

