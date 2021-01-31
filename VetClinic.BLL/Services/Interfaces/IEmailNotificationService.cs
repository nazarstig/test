using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace VetClinic.BLL.Services.Interfaces
{
    public interface IEmailNotificationService
    {
        public Task<bool> CreateAndSendEmailAsync(string toEmail, string subject, string message);
       
        public Task<bool> SendEmailAsync(string email, string subject, string msg );
      
    }
}

