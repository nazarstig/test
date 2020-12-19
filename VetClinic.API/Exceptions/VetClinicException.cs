using System;
using System.Net;
using VetClinic.API.DTO.Error;

namespace VetClinic.API.Exceptions
{
    public class VetClinicException : Exception
    {
        public HttpStatusCode Status { get; set; }
        public FieldErrorModel[] Errors { get; set; }

        public VetClinicException(HttpStatusCode status, string msg, params FieldErrorModel[] fieldErrors) : base(msg)
        {
            Status = status;
            Errors = fieldErrors;
        }
    }
}