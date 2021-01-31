using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using VetClinic.BLL.Helpers;
using VetClinic.BLL.Services.Interfaces;

namespace VetClinic.BLL.Services.Realizations
{
    public class EmailNotificationService : IEmailNotificationService
    {

        public async Task<bool> CreateAndSendEmailAsync(string toEmail, string subject, string message)
        {
            bool result = false;
 //           string emailMsg = EmailHelper.RegistrationMessage(toEmail);//"Dear " + toEmail + ", <br /><br />Welcome in VetClinic! </b> <br /><br /> Thanks & Regards, <br />Secretar Petrovych";
 //          string emailSubject = EmailHelper.RegistrationSubject;  //EmailInfo.EMAIL_SUBJECT_DEFAULT
            result = await this.SendEmailAsync(toEmail, subject, message);
            return result;
        }

        public async Task<bool> SendEmailAsync(string email, string subject,  string msg )
        {
            bool isSend = false;
            try
            {
                var body = msg;
                var message = new MailMessage();
                message.To.Add(new MailAddress(email));
                message.From = new MailAddress("vetclinicif114notification@gmail.com");
                message.Subject = subject;
                message.Body = body;
                message.IsBodyHtml = true;

                using (var smtp = new SmtpClient())
                {

                    var credential = new NetworkCredential
                    {
                        UserName = "vetclinicif114notification@gmail.com",
                        Password = "Roberto!1243$"
                    };
                    smtp.Credentials = credential;
                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = Convert.ToInt32(587);
                    smtp.EnableSsl = true;

                    await smtp.SendMailAsync(message);
                    isSend = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }  
            return isSend;
        }
    }
}

