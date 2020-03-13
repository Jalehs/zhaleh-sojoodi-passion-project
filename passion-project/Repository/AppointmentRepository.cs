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

        public bool CreateAppointment(AppointmentVM appointmentModel)
        {
            try
            {
                Appointment appointment = new Appointment
                {
                    AppointmentDate = appointmentModel.AppointmentDate,
                    AppointmentTime = appointmentModel.AppointmentTime,
                    AppointmentSummery = appointmentModel.AppointmentSummery
                };
                _context.Appointment.Add(appointment);
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
