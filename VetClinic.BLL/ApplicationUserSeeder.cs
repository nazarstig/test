using IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Events;
using System.Linq;
using System.Net;
using System.Security.Claims;
using VetClinic.BLL.Exceptions;
using VetClinic.DAL.Entities;

namespace VetClinic.BLL
{
    public static class ApplicationUserSeeder
    {
        public static void SeedUsers(UserManager<User> userManager)
        {
            var services = new ServiceCollection();
            services.AddLogging();
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console(restrictedToMinimumLevel: LogEventLevel.Debug)
                .CreateLogger();

            var alice = userManager.FindByNameAsync("alice").Result;
            if (alice == null)
            {
                alice = new User
                {
<<<<<<< HEAD:VetClinic.API/ExtensionMethods/AppExtensions.cs
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
=======
                    UserName = "alice",
                    FirstName = "Alice",
                    LastName = "Smith",
                    Email = "AliceSmith@email.com",
                    EmailConfirmed = true,
                    PhoneNumber = "123456789123",
                };
                var result = userManager.CreateAsync(alice, "Pass123$").Result;
                if (!result.Succeeded)
                {
                    throw new VetClinicException(HttpStatusCode.InternalServerError,result.Errors.First().Description);
                }
>>>>>>> master:VetClinic.BLL/ApplicationUserSeeder.cs

                result = userManager.AddClaimsAsync(alice, new Claim[]{
                        new Claim(JwtClaimTypes.Name, "Alice Smith"),
                        new Claim(JwtClaimTypes.GivenName, "Alice"),
                        new Claim(JwtClaimTypes.FamilyName, "Smith"),
                        new Claim(JwtClaimTypes.WebSite, "http://alice.com"),
                    }).Result;
                if (!result.Succeeded)
                {
                    throw new VetClinicException(HttpStatusCode.InternalServerError, result.Errors.First().Description);
                }

                if (!userManager.IsInRoleAsync(alice, "admin").Result)
                {
                    _ = userManager.AddToRoleAsync(alice, "admin").Result;
                }

                Log.Debug("alice created");
            }
            else
            {
                Log.Debug("alice already exists");
            }

            var bob = userManager.FindByNameAsync("bob").Result;
            if (bob == null)
            {
                bob = new User
                {
                    UserName = "bob",
                    FirstName = "Alice",
                    LastName = "Smith",
                    Email = "BobSmith@email.com",
                    EmailConfirmed = true,
                    PhoneNumber = "123456789321",
                };
                var result = userManager.CreateAsync(bob, "Pass123$").Result;
                if (!result.Succeeded)
                {
                    throw new VetClinicException(HttpStatusCode.InternalServerError, result.Errors.First().Description);
                }

                result = userManager.AddClaimsAsync(bob, new Claim[]{
                        new Claim(JwtClaimTypes.Name, "Bob Smith"),
                        new Claim(JwtClaimTypes.GivenName, "Bob"),
                        new Claim(JwtClaimTypes.FamilyName, "Smith"),
                        new Claim(JwtClaimTypes.WebSite, "http://bob.com"),
                        new Claim("location", "somewhere")
                    }).Result;
<<<<<<< HEAD:VetClinic.API/ExtensionMethods/AppExtensions.cs
                        if (!result.Succeeded)
                        {
                            throw new Exception(result.Errors.First().Description);
                        }

                        if (!userMgr.IsInRoleAsync(bob, client.Name).Result)
                        {
                            _ = userMgr.AddToRoleAsync(bob, client.Name).Result;
                        }
=======
                if (!result.Succeeded)
                {
                    throw new VetClinicException(HttpStatusCode.InternalServerError, result.Errors.First().Description);
                }
>>>>>>> master:VetClinic.BLL/ApplicationUserSeeder.cs

                if (!userManager.IsInRoleAsync(bob, "client").Result)
                {
                    _ = userManager.AddToRoleAsync(bob, "client").Result;
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
