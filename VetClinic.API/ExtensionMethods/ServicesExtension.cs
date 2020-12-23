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
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IProcedureService, ProcedureService>();
            services.AddScoped<IClientService, ClientService>();
            services.AddScoped<IPositionService, PositionService>();
            services.AddScoped<IDoctorService, DoctorService>();
            services.AddScoped<IServiceService, ServiceService>();
            services.AddScoped<IAnimalService, AnimalService>();
        }
    }
}