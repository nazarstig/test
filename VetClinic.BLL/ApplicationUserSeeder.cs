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
                    UserName = "alice",
                    FirstName = "Alice",
                    LastName = "Smith",
                    Email = "AliceSmith@email.com",
                    EmailConfirmed = true,
                };
                var result = userManager.CreateAsync(alice, "Pass123$").Result;
                if (!result.Succeeded)
                {
                    throw new VetClinicException(HttpStatusCode.InternalServerError,result.Errors.First().Description);
                }

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
                    EmailConfirmed = true
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
                if (!result.Succeeded)
                {
                    throw new VetClinicException(HttpStatusCode.InternalServerError, result.Errors.First().Description);
                }

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
