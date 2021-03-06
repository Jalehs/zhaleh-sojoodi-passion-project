﻿using Microsoft.EntityFrameworkCore;
using passion_project.Models.HealthCenter;
using passion_project.ViewModel.Appointment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace passion_project.Repository
{
    public class AppointmentRepository
    {
        private readonly ApplicationDbContext _context;

        public AppointmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<AppointmentVM> GetAllAppointments(string lastName)
        {
            if (lastName == null)
            {
                return _context.Appointment.Select(ap => new AppointmentVM
                {
                    AppointmentId = ap.AppointmentId,
                    PatientId = ap.PatientId,
                    DoctorId = ap.DoctorId,
                    PatientFirstName = ap.Patient.PatientFirstName,
                    PatientLastName = ap.Patient.PatientLastName,
                    DoctorFirstName = ap.Doctor.DoctorFirstName,
                    DoctorLastName = ap.Doctor.DoctorLastName,
                    AppointmentDate = ap.AppointmentDate,
                    AppointmentTime = ap.AppointmentTime,
                    AppointmentSummery = ap.AppointmentSummery
                }).OrderByDescending(ap => ap.AppointmentDate);
            }
            else
            {
                var appointements =  _context.Appointment.Where(ap => ap.Patient.PatientLastName == lastName);
                return appointements.Select(ap => new AppointmentVM
                {
                    AppointmentId = ap.AppointmentId,
                    PatientId = ap.PatientId,
                    DoctorId = ap.DoctorId,
                    PatientFirstName = ap.Patient.PatientFirstName,
                    PatientLastName = ap.Patient.PatientLastName,
                    DoctorFirstName = ap.Doctor.DoctorFirstName,
                    DoctorLastName = ap.Doctor.DoctorLastName,
                    AppointmentDate = ap.AppointmentDate,
                    AppointmentTime = ap.AppointmentTime,
                    AppointmentSummery = ap.AppointmentSummery
                }).OrderByDescending(ap => ap.AppointmentDate);
            }
    
        }
       
        public IEnumerable<AppointmentVM> GetAppointmentsByDoctorId(int id)
        {
            return _context.Appointment.Where(a => a.DoctorId == id).Select(a => new AppointmentVM
            {
                AppointmentId = a.AppointmentId,
                PatientId = a.Patient.PatientId,
                PatientFirstName = a.Patient.PatientFirstName,
                PatientLastName = a.Patient.PatientLastName,
                AppointmentDate = a.AppointmentDate,
                AppointmentTime = a.AppointmentTime,
                AppointmentSummery = a.AppointmentSummery
            }).OrderByDescending(a => a.AppointmentDate);

        }

        public IEnumerable<AppointmentVM> GetAppointmentsByPatientLastName(int id, string lastName)
        {
            var appointmentsByDoctorId = _context.Appointment.Where(a => a.DoctorId == id);
            var appointmentByLastName = appointmentsByDoctorId.Where(a => a.Patient.PatientLastName == lastName);
            return appointmentByLastName.Select(a => new AppointmentVM
            {
                AppointmentId = a.AppointmentId,
                PatientId = a.Patient.PatientId,
                PatientFirstName = a.Patient.PatientFirstName,
                PatientLastName = a.Patient.PatientLastName,
                AppointmentDate = a.AppointmentDate,
                AppointmentTime = a.AppointmentTime,
                AppointmentSummery = a.AppointmentSummery

            }).OrderByDescending(a => a.AppointmentDate);
        }
        public IEnumerable<AppointmentVM> GetAppointmentsByPatientId(int id)
        {
            return _context.Appointment.Where(a => a.PatientId == id).Select(a => new AppointmentVM
            {
                AppointmentId = a.AppointmentId,
                DoctorId = a.DoctorId,
                PatientId = a.PatientId,
                DoctorFirstName = a.Doctor.DoctorFirstName,
                DoctorLastName = a.Doctor.DoctorLastName,
                AppointmentDate = a.AppointmentDate,
                AppointmentTime = a.AppointmentTime,
                AppointmentSummery = a.AppointmentSummery
            }).OrderByDescending(a =>a.AppointmentDate);
        }

        public IEnumerable<AppointmentVM> GetAppointmentsByDoctorLastName(int id, string lastName)
        {
            var appointmentsByPatientId = _context.Appointment.Where(a => a.PatientId == id);
            var appointmentsByLastName = appointmentsByPatientId.Where(a => a.Doctor.DoctorLastName == lastName);
            return appointmentsByLastName.Select(a => new AppointmentVM
            {
                AppointmentId = a.AppointmentId,
                DoctorId = a.DoctorId,
                PatientId = a.PatientId,
                DoctorFirstName = a.Doctor.DoctorFirstName,
                DoctorLastName = a.Doctor.DoctorLastName,
                AppointmentDate = a.AppointmentDate,
                AppointmentTime = a.AppointmentTime,
                AppointmentSummery = a.AppointmentSummery
            }).OrderByDescending(a => a.AppointmentDate);
        }
        public Appointment GetAppointment(int id)
        {
            return _context.Appointment.FirstOrDefault(a => a.AppointmentId == id);
        }

        public bool CreateAppointment(AppointmentVM appointmentModel, int id)
        {
            try
            {
                var patient = _context.Patient.FirstOrDefault(p => p.PatientEmailAddress == appointmentModel.PatientEmailAddress);
                var doctor = _context.Doctor.FirstOrDefault(d => d.DoctorId == id);
                Appointment appointment = new Appointment
                {
                    AppointmentDate = appointmentModel.AppointmentDate,
                    AppointmentTime = appointmentModel.AppointmentTime,
                    AppointmentSummery = appointmentModel.AppointmentSummery
                };
                patient.Appointment.Add(appointment);
                doctor.Appointment.Add(appointment);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public AppointmentVM Details(int id)
        {
            var appointment = GetAppointment(id);
            var patient = _context.Patient.Where(p => p.PatientId == appointment.PatientId).FirstOrDefault();
           AppointmentVM appointmentModel = new AppointmentVM
           {
                DoctorId = appointment.DoctorId,
                PatientId = patient.PatientId,
                AppointmentId = appointment.AppointmentId,
                AppointmentDate = appointment.AppointmentDate,
                AppointmentTime = appointment.AppointmentTime,
                AppointmentSummery = appointment.AppointmentSummery,
                PatientFirstName = patient.PatientFirstName,
                PatientLastName = patient.PatientLastName
           };
            return appointmentModel;
        }

        public bool UpdateAppointment(AppointmentVM appointmentModel)
        {
            try
            {
                var appointment = GetAppointment(appointmentModel.AppointmentId);
                appointment.AppointmentSummery = appointmentModel.AppointmentSummery;
                _context.Appointment.Update(appointment);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
       

        public bool DeleteAppointment(int id)
        {
            var appointment = GetAppointment(id);
            try
            {
                _context.Remove(appointment);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public IEnumerable<TimeSpan> GetAllTime()
        {
            List<TimeSpan> allTime = new List<TimeSpan>();
            TimeSpan startTime = new TimeSpan(10, 0, 0);
            TimeSpan endTime = new TimeSpan(18, 0, 0);
            while (startTime < endTime)
            {
                allTime.Add(startTime);
                startTime = startTime.Add(new TimeSpan(0, 30, 0));
            }
            return allTime;
        }

        public IEnumerable<TimeSpan> GetAllBookedTime(int id, DateTime date)
        {
            IEnumerable<Appointment> appointments = _context.Appointment.Where(a => a.DoctorId == id).Where(a => a.AppointmentDate == date).ToList();
            List<TimeSpan> bookedTime = new List<TimeSpan>();
            foreach(var app in appointments)
            {
                bookedTime.Add(app.AppointmentTime);
            }
            return bookedTime;

        }
    }
}
