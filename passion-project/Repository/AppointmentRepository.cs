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

        public IEnumerable<AppointmentVM> GetAllApoointments()
        {
            return _context.Appointment.Select(ap => new AppointmentVM
            {
                PatientFirstName = ap.Patient.PatientFirstName,
                PatientLastName = ap.Patient.PatientLastName,
                DoctorFirstName = ap.Doctor.DoctorFirstName,
                DoctorLastName = ap.Doctor.DoctorLastName,
                AppointmentDate = ap.AppointmentDate,
                AppointmentTime = ap.AppointmentTime,
                AppointmentSummery = ap.AppointmentSummery
            });
        }

        public IEnumerable<AppointmentVM> GetAppointmentsByDoctorId(int id)
        {
             IEnumerable<Appointment> appointments= _context.Appointment.Where(a => a.DoctorId == id);
            return appointments.Select(ap => new AppointmentVM
            {
                PatientFirstName = ap.Patient.PatientFirstName,
                PatientLastName = ap.Patient.PatientLastName,
                AppointmentDate = ap.AppointmentDate,
                AppointmentTime = ap.AppointmentTime,
                AppointmentSummery = ap.AppointmentSummery
            });
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
            return new AppointmentVM
            {
                AppointmentId = appointment.AppointmentId,
                AppointmentDate = appointment.AppointmentDate,
                AppointmentTime = appointment.AppointmentTime,
                AppointmentSummery = appointment.AppointmentSummery,
                PatientFirstName = appointment.Patient.PatientFirstName,
                PatientLastName = appointment.Patient.PatientLastName,
                DoctorFirstName = appointment.Doctor.DoctorFirstName,
                DoctorLastName = appointment.Doctor.DoctorLastName
            };
        }

        public bool UpdateAppointment(AppointmentVM appointmentModel)
        {
            try
            {
                var appointment = GetAppointment(appointmentModel.AppointmentId);
                appointment.AppointmentDate = appointmentModel.AppointmentDate;
                appointment.AppointmentTime = appointmentModel.AppointmentTime;
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
       

        public bool DeleteAppointment(AppointmentVM appointmentModel)
        {
            var appointment = GetAppointment(appointmentModel.AppointmentId);
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

    }
}
