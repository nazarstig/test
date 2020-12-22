using Microsoft.Extensions.DependencyInjection;
using VetClinic.BLL.Services.Interfaces;
using VetClinic.BLL.Services.Realizations;

namespace VetClinic.API.ExtensionMethods
{
    public static class ServicesExtension
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IAppointmentService, AppointmentService>();
        }
    }
}