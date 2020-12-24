using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using VetClinic.BLL.Exceptions;
using VetClinic.BLL.Services.Interfaces;
using VetClinic.DAL.Entities;
using VetClinic.DAL.Repositories.Interfaces;

namespace VetClinic.BLL.Services.Realizations
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryWrapper _repositoryWrapper;

        public AppointmentService(IMapper mapper, IRepositoryWrapper repositoryWrapper)
        {
            _mapper = mapper;
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task<ICollection<Appointment>> GetAllAppointmentsAsync()
        {
            return await _repositoryWrapper.AppointmentRepository.GetAsync(include: Include());
        }

        public async Task<Appointment> GetAppointmentByIdAsync(int id)
        {
            var appointment = await _repositoryWrapper.AppointmentRepository.GetFirstOrDefaultAsync(a => a.Id == id, Include());
            if (appointment == null)
                throw new VetClinicException(HttpStatusCode.NotFound, $"Appointment with id {id} doesn't exist");

            return appointment;
        }

        public async Task<Appointment> CreateAppointmentAsync(Appointment appointment)
        {
            await ValidateModelAsync(appointment, isUpdate: false);

            _repositoryWrapper.AppointmentRepository.Add(appointment);
            await _repositoryWrapper.SaveAsync();

            var createdAppointment = await GetAppointmentByIdAsync(appointment.Id);

            return createdAppointment;
        }

        public async Task<Appointment> UpdateAppointmentAsync(int id, Appointment appointment)
        {
            var appointmentToUpdate = await GetAppointmentByIdAsync(id);
            await ValidateModelAsync(appointment, isUpdate: true);

            UpdatePerformedProcedures(appointment, appointmentToUpdate);
            _mapper.Map(appointment, appointmentToUpdate);

            _repositoryWrapper.AppointmentRepository.Update(appointmentToUpdate);
            await _repositoryWrapper.SaveAsync();

            appointmentToUpdate = await GetAppointmentByIdAsync(id);

            return appointmentToUpdate;
        }

        public async Task<Appointment> DeleteAppointmentAsync(int id)
        {
            var appointment = await GetAppointmentByIdAsync(id);

            _repositoryWrapper.AppointmentRepository.Remove(appointment);
            await _repositoryWrapper.SaveAsync();

            return appointment;
        }

        private async Task ValidateModelAsync(Appointment appointment, bool isUpdate)
        {
            var fieldErrorModels = new List<FieldErrorModel>();

            if (!await _repositoryWrapper.AnimalRepository.IsAnyAsync(a => a.Id == appointment.AnimalId))
                fieldErrorModels.Add(new FieldErrorModel("animalId", $"Animal with id {appointment.AnimalId} doesn't exist"));

            if (!await _repositoryWrapper.ServiceRepository.IsAnyAsync(s => s.Id == appointment.ServiceId))
                fieldErrorModels.Add(new FieldErrorModel("serviceId", $"Service with id {appointment.ServiceId} doesn't exist"));

            if (isUpdate)
            {
                if (!await _repositoryWrapper.StatusRepository.IsAnyAsync(s => s.Id == appointment.StatusId))
                    fieldErrorModels.Add(new FieldErrorModel("statusId", $"Status with id {appointment.StatusId} doesn't exist"));

                if (!await _repositoryWrapper.DoctorRepository.IsAnyAsync(d => d.Id == appointment.DoctorId))
                    fieldErrorModels.Add(new FieldErrorModel("doctorId", $"Doctor with id {appointment.DoctorId} doesn't exist"));

                await ValidatePerformedProcedures(appointment, fieldErrorModels);
            }

            if (fieldErrorModels.Count > 0)
                throw new VetClinicException(HttpStatusCode.BadRequest, "Model is invalid", fieldErrorModels.ToArray());
        }

        private async Task ValidatePerformedProcedures(Appointment appointment, List<FieldErrorModel> fieldErrorModels)
        {
            var messages = new List<string>();

            foreach (var ap in appointment.AppointmentProcedures)
            {
                var procedure = await _repositoryWrapper.ProcedureRepository.GetFirstOrDefaultAsync(p => p.Id == ap.ProcedureId);
                if (procedure == null)
                    messages.Add($"Procedure with id {ap.ProcedureId} doesn't exist");
            }

            if (messages.Count > 0)
                fieldErrorModels.Add(new FieldErrorModel("procedureIds", messages.ToArray()));
        }

        private void UpdatePerformedProcedures(Appointment source, Appointment destination)
        {
            foreach (var ap in destination.AppointmentProcedures)
            {
                _repositoryWrapper.AppointmentProceduresRepository.Remove(ap);
            }

            foreach (var ap in source.AppointmentProcedures)
            {
                ap.AppointmentId = destination.Id;
                _repositoryWrapper.AppointmentProceduresRepository.Add(ap);
            }
        }

        private static Func<IQueryable<Appointment>, IIncludableQueryable<Appointment, object>> Include()
        {
            return app => app
                .Include(a => a.Animal).ThenInclude(a => a.AnimalType)
                .Include(a => a.Animal).ThenInclude(a => a.Client).ThenInclude(c => c.User)
                .Include(a => a.Service)
                .Include(a => a.Status)
                .Include(a => a.Doctor).ThenInclude(d => d.User)
                .Include(a => a.AppointmentProcedures).ThenInclude(a => a.Procedure);
        }
    }
}