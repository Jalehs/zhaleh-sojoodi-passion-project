using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Mvc;
using passion_project.Model;
using passion_project.Repository;
using passion_project.ViewModel;

namespace passion_project.Controllers
{
    public class DoctorController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly HostingEnvironment _hostingEnviroment;

        public DoctorController(ApplicationDbContext context, HostingEnvironment hostingEnviroment)
        {
            _context = context;
            _hostingEnviroment = hostingEnviroment;
        }
        public IActionResult Index()
        {
            DoctorRepository doctorRepo = new DoctorRepository(_context, _hostingEnviroment);
            return View(doctorRepo.GetAllDoctors());
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(DoctorCreateVM doctorModel)
        {
            DoctorRepository doctorRepo = new DoctorRepository(_context, _hostingEnviroment);
            if (ModelState.IsValid)
            {
                if (doctorRepo.Create(doctorModel))
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(doctorModel);
        }

        public IActionResult Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            DoctorRepository doctorRepo = new DoctorRepository(_context, _hostingEnviroment);
            var doctor = doctorRepo.GetDoctor(id);
            if (doctor == null)
            {
                return NotFound();
            }
            DoctorCreateVM doctorModel = new DoctorCreateVM
            {
                DoctorId = doctor.DoctorId,
                DoctorFirstName = doctor.DoctorFirstName,
                DoctorLastName = doctor.DoctorLastName,
                Speciality = doctor.Speciality,
                DoctorPhoneNumber = doctor.DoctorPhoneNumber,
                DoctorEmailAddress = doctor.DoctorEmailAddress,
                RoomNumber = doctor.RoomNumber,
                Biography = doctor.Biography

            };
            return View(doctorModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, DoctorCreateVM doctorModel)
        {
            DoctorRepository doctorRepo = new DoctorRepository(_context, _hostingEnviroment);
            if (ModelState.IsValid)
            {
                if (doctorRepo.Update(id, doctorModel))
                {
                    return RedirectToAction("Index");
                }
            }
            return View(doctorModel);
        }

    }




    }
