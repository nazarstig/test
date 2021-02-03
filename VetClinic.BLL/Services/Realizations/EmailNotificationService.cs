using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using VetClinic.BLL.Domain;
using VetClinic.BLL.Helpers;
using VetClinic.BLL.Services.Interfaces;
using VetClinic.DAL.Entities;
using System.Linq;

namespace VetClinic.BLL.Services.Realizations
{
    public class EmailNotificationService : IEmailNotificationService
    {
        private readonly IAppointmentService _appointmentService;
        private readonly IClientService _clientService;

        public EmailNotificationService(IAppointmentService appointmentService,
            IClientService clientService)
        {
            _appointmentService = appointmentService;
            _clientService = clientService;
        }

        public async Task<bool> SendEmailAsync(string emailTo, string subject, string message)
        {
            bool isSend = false;
            MailMessage mailMessage = CreateMailMessageObject(emailTo, subject, message);
            try
            {
                await EmailHelper.smtp.SendMailAsync(mailMessage);
                isSend = true;
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return isSend;
        }

        public MailMessage CreateMailMessageObject(string emailTo, string subject, string message)
        {
            var body = message;
            var mailMessage = new MailMessage();
            mailMessage.To.Add(new MailAddress(emailTo));
            mailMessage.From = new MailAddress(EmailHelper.EmailInfo.EmailSender);
            mailMessage.Subject = subject;
            mailMessage.Body = body;
            mailMessage.IsBodyHtml = true;
            return mailMessage;
        }

        public async Task SendAppointmentNotifications(int appointmentId)
        {
            Appointment appointment = await _appointmentService.GetAppointmentByIdAsync(appointmentId);
            if (appointment != null)
            {
                AppointmentEmailNotificationDto dto = CreateAppointmentNotificationDto(appointment);
                switch (dto.StatusId)
                {
                    case 1:
                        {
                            await SendAppointmentNotificationDoctor(dto);
                            await SendAppointmentNotificationClient(dto);
                            break;
                        }
                }
            }
        }

        public AppointmentEmailNotificationDto CreateAppointmentNotificationDto(Appointment appointment)
        {
            if (appointment != null)
            {
                AppointmentEmailNotificationDto dto = new AppointmentEmailNotificationDto
                {
                    DoctorEmail = appointment?.Doctor?.User?.Email,
                    DoctorFirstName = appointment?.Doctor?.User?.FirstName,
                    DoctorLastName = appointment?.Doctor?.User?.LastName,
                    ClientEmail = appointment?.Animal?.Client?.User?.Email,
                    ClientFirstName = appointment?.Animal?.Client?.User?.FirstName,
                    ClientLastName = appointment?.Animal?.Client?.User?.LastName,
                    AnimalName = appointment?.Animal?.Name,
                    AnimalType = appointment?.Animal?.AnimalType?.AnimalTypeName,
                    Date = appointment?.AppointmentDate.ToString(),
                    ServiceName = appointment?.Service?.ServiceName,
                    StatusId = appointment?.StatusId
                };
                return dto;
            }
            else return null;
        }

        public async Task SendAppointmentNotificationDoctor(AppointmentEmailNotificationDto dto)
        {
            string emailTo = dto.DoctorEmail;
            if (emailTo != null)
            {
                string subject = EmailHelper.AppointmentDoctorSubject;
                string message = EmailHelper.AppointmentDoctorMessage(dto);
                await SendEmailAsync(dto.DoctorEmail,
                          subject, message);
            }
        }

        public async Task SendAppointmentNotificationClient(AppointmentEmailNotificationDto dto)
        {
            string emailTo = dto.ClientEmail;
            if (emailTo != null)
            {
                string subject = EmailHelper.AppointmentClientSubject;
                string message = EmailHelper.AppointmentClientMessage(dto);
                await SendEmailAsync(emailTo,
                    subject, message);
            }
        }

        public async Task SendClientRegistrationNotification(string username)
        {
            ClientsFilter filter = new ClientsFilter { UserName = username };
            Client client = _clientService.GetAllClients(filter).Result.FirstOrDefault();
            User user = client?.User;
            if (user != null)
            {
                string emailTo = user.Email;
                if (emailTo != null)
                {
                    string subject = EmailHelper.RegistrationSubject;
                    string message = EmailHelper.RegistrationMessage(user.FirstName, user.LastName);
                    await SendEmailAsync(emailTo, subject,
                    message);
                }
            }
        }

    }
}

