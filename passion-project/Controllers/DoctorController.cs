﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Mvc;
using passion_project.Models.HealthCenter;
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
        public IActionResult Index(string speciality)
        {
            DoctorRepository doctorRepo = new DoctorRepository(_context, _hostingEnviroment);
            if (speciality == null)
            {
                return View(doctorRepo.GetAllDoctors());
            }
            else
            {
                return View(doctorRepo.GetDoctorsBySpeciality(speciality));
            }
            
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
            return View(doctorModel);
        }

        public IActionResult Edit(int id)
        {
            DoctorRepository doctorRepo = new DoctorRepository(_context, _hostingEnviroment);
            var doctor = doctorRepo.GetDoctor(id);
            if (doctor == null)
            {
                return NotFound();
            }
            DoctorIndexVM doctorModel = new DoctorIndexVM
            {
                DoctorId = doctor.DoctorId,
                DoctorFirstName = doctor.DoctorFirstName,
                DoctorLastName = doctor.DoctorLastName,
                ImageUrl = doctor.DoctorImageUrl,
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
        public IActionResult Edit(int id, DoctorIndexVM doctorModel)
        {
            DoctorRepository doctorRepo = new DoctorRepository(_context, _hostingEnviroment);
            if (doctorRepo.Update(id, doctorModel))
            {
                return RedirectToAction("Details", new { id = id });
            }
            return View(doctorModel);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            DoctorRepository doctorRepo = new DoctorRepository(_context, _hostingEnviroment);
            if (doctorRepo.Details(id) == null)
            {
                return NotFound();
            }
            return View(doctorRepo.Details(id));
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            DoctorRepository doctorRepo = new DoctorRepository(_context, _hostingEnviroment);
            var doctor = doctorRepo.GetDoctor(id);
            DoctorIndexVM doctorModel = new DoctorIndexVM
            {
                DoctorId = doctor.DoctorId,
                DoctorFirstName = doctor.DoctorFirstName,
                DoctorLastName = doctor.DoctorLastName
            };
            return View(doctorModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(DoctorIndexVM doctorModel)
        {
            DoctorRepository doctorRepo = new DoctorRepository(_context, _hostingEnviroment);
            if (doctorModel == null)
            {
                return NotFound();
            }
            if(doctorRepo.Delete(doctorModel))
            {
                return RedirectToAction("Index");
            }
            return View(doctorModel);
        }

    }
 }
