using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using passion_project.Models;
using passion_project.Models.HealthCenter;
using passion_project.Repository;
using passion_project.Services;
using passion_project.ViewModel.Appointment;

namespace passion_project.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly EmailSettings _emailSettings;

        public AppointmentController(ApplicationDbContext context, IOptions<EmailSettings> emailSettings)
        {
            _context = context;
            _emailSettings = emailSettings.Value;
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            AppointmentRepository appointmentRepo = new AppointmentRepository(_context);
            return View(appointmentRepo.GetAllAppointments());
        }

        public IActionResult GetAppointmentsByDoctorId(int id)
        {
            AppointmentRepository appointmentRepo = new AppointmentRepository(_context);
            return View(appointmentRepo.GetAppointmentsByDoctorId(id).ToList());
        }

        public IActionResult GetAppointmentsByPatientId(int id)
        {
            AppointmentRepository appointmentRepo = new AppointmentRepository(_context);
            return View(appointmentRepo.GetAppointmentsByPatientId(id));
        }

        // GET: Appointment/Details/5
        public IActionResult Details(int id)
        {
            AppointmentRepository appointmentRepo = new AppointmentRepository(_context);
            if (appointmentRepo.Details(id) == null)
            {
                return NotFound();
            }
            return View(appointmentRepo.Details(id));
        }

        // GET: Appointment/Create
        [HttpGet]
        [Authorize]
        public IActionResult Create(int id)
        {
            AppointmentRepository appointmentRepo = new AppointmentRepository(_context);
            ViewBag.timeAvailable = new SelectList(String.Empty);
            
            var doctor = _context.Doctor.Where(d => d.DoctorId == id).FirstOrDefault();

            if (User.IsInRole("Admin"))
            {
                AppointmentVM appointmentModel = new AppointmentVM
                {
                    DoctorFirstName = doctor.DoctorFirstName,
                    DoctorLastName = doctor.DoctorLastName
                };
                return View(appointmentModel);
            }
            else
            {
                var userName = User.Identity.Name;
                var patient = _context.Patient.Where(p => p.PatientEmailAddress == userName).FirstOrDefault();
                ViewBag.PatientFirstName = patient.PatientFirstName;
                ViewBag.PatientLastName = patient.PatientLastName;
                AppointmentVM appointmentModel = new AppointmentVM
                {
                    
                    PatientId = patient.PatientId,
                    PatientEmailAddress = userName,
                    PatientFirstName = patient.PatientFirstName,
                    PatientLastName = patient.PatientLastName,
                    DoctorFirstName = doctor.DoctorFirstName,
                    DoctorLastName = doctor.DoctorLastName
                };
                return View(appointmentModel);
            }
        }

        // POST: Appointment/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult Create(AppointmentVM appointmentVM, int id)
        {
            if (ModelState.IsValid)
            {
                AppointmentRepository appointmentRepo = new AppointmentRepository(_context);
                if (new EmailHelper(_emailSettings).SendMail(appointmentVM.PatientEmailAddress, "Appointment Reminder", appointmentVM))
                {
                    if (appointmentRepo.CreateAppointment(appointmentVM, id))
                    {
                        if (User.IsInRole("Admin"))
                        {
                            return RedirectToAction("GetAppointmentsByDoctorId", new { id = id });
                        }
                        else
                        {
                            return RedirectToAction("GetAppointmentsByPatientId", new { id = appointmentVM.PatientId });
                        }
                    }

                }
            }
            ViewBag.PatientFirstName = appointmentVM.PatientFirstName;
            ViewBag.PatientLastName = appointmentVM.PatientLastName;
            return View(appointmentVM);
        }

        // GET: Appointment/Edit/5
        public IActionResult Edit(int id)
        {
            AppointmentRepository appointmentRepo = new AppointmentRepository(_context);
            var appointment = appointmentRepo.GetAppointment(id);
            var patient = _context.Patient.Where(p => p.PatientId == appointment.PatientId).FirstOrDefault();
            if (appointment == null)
            {
                return NotFound();
            }
            var appointmentVM = new AppointmentVM
            {
                DoctorId = appointment.DoctorId, 
                PatientFirstName = patient.PatientFirstName,
                PatientLastName = patient.PatientLastName, 
                AppointmentId = appointment.AppointmentId,
                AppointmentDate = appointment.AppointmentDate,
                AppointmentTime = appointment.AppointmentTime,
                AppointmentSummery = appointment.AppointmentSummery
            };
            return View(appointmentVM);
        }

        // POST: Appointment/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, AppointmentVM appointmentVM)
        {
            AppointmentRepository appointmentRepo = new AppointmentRepository(_context);
            var appointment = appointmentRepo.GetAppointment(id);
            if (id != appointmentVM.AppointmentId)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
               if(appointmentRepo.UpdateAppointment(appointmentVM)) 
                    return RedirectToAction("GetAppointmentsByDoctorId", new { id = appointment.DoctorId});
            }
            return View(appointmentVM);
        }

        // GET: Appointment/Delete/5
        public IActionResult Delete(int id)
        {
            AppointmentRepository appointmentRepo = new AppointmentRepository(_context);
            var appointment = appointmentRepo.GetAppointment(id);
            var patient = _context.Patient.Where(p => p.PatientId == appointment.PatientId).FirstOrDefault();
            var doctor = _context.Doctor.Where(d => d.DoctorId == appointment.DoctorId).FirstOrDefault();
            if (appointment == null)
            {
                return NotFound();
            }

            var appointmentVM = new AppointmentVM
            {
                DoctorId = appointment.DoctorId,
                PatientFirstName = patient.PatientFirstName,
                PatientLastName = patient.PatientLastName,
                DoctorFirstName = doctor.DoctorFirstName,
                DoctorLastName = doctor.DoctorLastName,
                AppointmentDate = appointment.AppointmentDate,
                AppointmentTime = appointment.AppointmentTime
            };
            return View(appointmentVM);
        }

        // POST: Appointment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            AppointmentRepository appointmentRepo = new AppointmentRepository(_context);
            var appointment = appointmentRepo.GetAppointment(id);
            if (appointmentRepo.DeleteAppointment(id))
                return RedirectToAction("GetAppointmentsByDoctorId", new { id = appointment.DoctorId });
            return View(appointment);
        }

        public IEnumerable<TimeSpan> GetAvailableTime(int id, DateTime selectedDate)
        {
            AppointmentRepository appointmentRepo = new AppointmentRepository(_context);
            IEnumerable<TimeSpan> ts = appointmentRepo.GetAllTime().Except(appointmentRepo.GetAllBookedTime(id, selectedDate));
            return ts;
        }

    }

}
