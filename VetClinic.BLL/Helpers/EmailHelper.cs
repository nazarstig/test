using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VetClinic.BLL.Helpers
{
    public class EmailHelper
    {
        public static string RegistrationSubject = "Registration";
        public static string RegistrationMessage(string name)
        {
            string message;
            message = string.Format("Dear {0}, <br/>" +
                "Welcome in VetClinic <br/>" +
                "Best regards,<br/>" +
                "Clinic administration", name);
            return message;
        }

        public static string AppointmentDoctorSubject = "You have new appointment";

        public static string AppointmentDoctorMessage(string doctorName, string patientName,
            string animalName, string appointmentDate)
        {
            string message;
            message = string.Format("Dear {0}, <br/>" +
                "You have a new appointment with client {1} for treatment " +
                "his pet {2}. Appointment scheduled at {3}",
                doctorName, patientName, animalName, appointmentDate);
            return message;
        }
    }
}
