using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Mvc;
using passion_project.Models.HealthCenter;
using passion_project.Repository;
using passion_project.ViewModel.Patient;

namespace passion_project.Controllers
{
    [Authorize]
    public class PatientController : Controller
    {
        private readonly ApplicationDbContext _context;


        public PatientController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Patients
        public IActionResult Index()
        {
            PatientRepository patientRepo = new PatientRepository(_context);
            return View(patientRepo.GetAll());           
        }
        
        // GET: Patients/Details/5
        public IActionResult Details(int id)
        {
            PatientRepository patientRepo = new PatientRepository(_context);
            if (patientRepo.Details(id) == null)
            {
                return NotFound();
            }

            return View(patientRepo.Details(id));
        }

        // GET: Patients/Create
        public IActionResult Create()
        {
            var userName = User.Identity.Name;
            if (User.IsInRole("Admin"))
            {
                return View();
            }
            else
            {
                PatientVM patientModel = new PatientVM
                {
                    
                    PatientEmailAddress = userName
                   
                };
                return View(patientModel);
            }
        }

        // POST: Patients/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PatientVM patientModel)
        {
            PatientRepository patientRepo = new PatientRepository(_context);
            if (ModelState.IsValid)
            {
                if (patientRepo.CreatePatient(patientModel))
                {
                    if (User.IsInRole("Admin"))
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }

            }
            return View(patientModel);
        }

        // GET: Patients/Edit/5
        public IActionResult Edit(int id)
        {
            PatientRepository patientRepo = new PatientRepository(_context);
            var patient = patientRepo.GetPatient(id);
            if (patient == null)
            {
                return NotFound();
            }
            PatientVM patientModel = new PatientVM
            {
                PatientId = patient.PatientId,
                PatientFirstName = patient.PatientFirstName,
                PatientLastName = patient.PatientLastName,
                Phn = patient.Phn,
                PatientPhoneNumber = patient.PatientPhoneNumber,
                PatientEmailAddress = patient.PatientEmailAddress,
                PatientAddress = patient.PatientAddress,
                PatientCity = patient.PatientCity,
                PatientPostalCode = patient.PatientPostalCode,
                PatientHistory = patient.PatientHistory
            };
            return View(patientModel);
        }

        // POST: Patients/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, PatientVM patientModel)
        {
            PatientRepository patientRepo = new PatientRepository(_context);
            if (id != patientModel.PatientId)
            {
                return NotFound();
            }
            if (patientRepo.Update(id, patientModel))
            {
                return RedirectToAction("Details", new { id = id });
            }
            return View(patientModel);
        }

        // GET: Patients/Delete/5
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            PatientRepository patientRepo = new PatientRepository(_context);
            var patient = patientRepo.GetPatient(id);
            if (patient == null)
            {
                return NotFound();
            }
            PatientVM patientModel = new PatientVM
            {
                PatientId = patient.PatientId,
                PatientFirstName = patient.PatientFirstName,
                PatientLastName = patient.PatientLastName
            };
            return View(patientModel);
        }

        // POST: Patients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(PatientVM patientModel)
        {
            PatientRepository patientRepo = new PatientRepository(_context);
            if(patientRepo.Delete(patientModel))
            {
                return RedirectToAction("Index");
            }
            return View(patientModel);
        }

    }
}
