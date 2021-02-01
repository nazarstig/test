using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VetClinic.DAL.Entities;

namespace VetClinic.BLL.Services.Interfaces
{
    public interface IEmailNotificationService
    {       
        public Task<bool> SendEmailAsync(string emailTo, string subject, string message );
        public Task SendAppointmentNotifications(Appointment appointment);

        public Task SendClientRegistrationNotification(User user);
    }
}

