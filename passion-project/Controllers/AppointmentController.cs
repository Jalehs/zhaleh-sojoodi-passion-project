using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using passion_project.Models.HealthCenter;
using passion_project.Repository;
using passion_project.ViewModel.Appointment;

namespace passion_project.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AppointmentController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Appointment
        public IActionResult Index()
        {
            AppointmentRepository appointmentRepo = new AppointmentRepository(_context);
            return View(appointmentRepo.GetAllApoointments());
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
        public IActionResult Create(int id)
        {
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
        public IActionResult Create(AppointmentVM appointmentVM, int id)
        {
            if (ModelState.IsValid)
            {
                AppointmentRepository appointmentRepo = new AppointmentRepository(_context);
                if (appointmentRepo.CreateAppointment(appointmentVM, id))
                {
                    return RedirectToAction(nameof(Index));
                }    
                
            }
            return View(appointmentVM);
        }

        // GET: Appointment/Edit/5
        public IActionResult Edit(int id)
        {
            AppointmentRepository appointmentRepo = new AppointmentRepository(_context);
            var appointment = appointmentRepo.GetAppointment(id);
            if (appointment == null)
            {
                return NotFound();
            }
            var appointmentVM = new AppointmentVM
            {
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
            if (id != appointmentVM.AppointmentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
               if(appointmentRepo.UpdateAppointment(appointmentVM)) 
                    return RedirectToAction(nameof(Index));
            }
            return View(appointmentVM);
        }

        // GET: Appointment/Delete/5
        public IActionResult Delete(int id)
        {
            AppointmentRepository appointmentRepo = new AppointmentRepository(_context);
            var appointment = appointmentRepo.GetAppointment(id);
            if (appointment == null)
            {
                return NotFound();
            }

            var appointmentVM = new AppointmentVM
            {
                PatientEmailAddress = appointment.Patient.PatientEmailAddress,
                DoctorFirstName = appointment.Doctor.DoctorFirstName,
                DoctorLastName = appointment.Doctor.DoctorLastName,
                AppointmentDate = appointment.AppointmentDate,
                AppointmentTime = appointment.AppointmentTime
            };
            return View(appointmentVM);
        }

        // POST: Appointment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(AppointmentVM appoitmentModel)
        {
            AppointmentRepository appointmentRepo = new AppointmentRepository(_context);
            if(appointmentRepo.DeleteAppointment(appoitmentModel))
                return RedirectToAction(nameof(Index));
            return View(appoitmentModel);
        }

    }
}
