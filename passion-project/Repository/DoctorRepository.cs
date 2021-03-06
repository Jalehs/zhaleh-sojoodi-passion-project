﻿using Microsoft.AspNetCore.Hosting.Internal;
using passion_project.Models.HealthCenter;
using passion_project.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace passion_project.Repository
{
   
    public class DoctorRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly HostingEnvironment _hostingEnviroment;

        public DoctorRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public DoctorRepository(ApplicationDbContext context, HostingEnvironment hostingEnviroment)
        {
            _context = context;
            _hostingEnviroment = hostingEnviroment;
        }

        public IEnumerable<DoctorIndexVM> GetAllDoctors()
        {
            return _context.Doctor.Select(d => new DoctorIndexVM
            {
                DoctorId = d.DoctorId,
                DoctorFirstName = d.DoctorFirstName,
                DoctorLastName = d.DoctorLastName,
                Speciality = d.Speciality,
                ImageUrl = d.DoctorImageUrl,
                DoctorPhoneNumber = d.DoctorPhoneNumber,
                DoctorEmailAddress = d.DoctorEmailAddress,
                RoomNumber = d.RoomNumber,
                Biography = d.Biography
            });
        }

        public bool Create(DoctorCreateVM doctorModel)
        {
            try
            {
                var doctor = new Doctor
                {
                    DoctorId = doctorModel.DoctorId,
                    DoctorFirstName = doctorModel.DoctorFirstName,
                    DoctorLastName = doctorModel.DoctorLastName,
                    Speciality = doctorModel.Speciality,
                    DoctorPhoneNumber = doctorModel.DoctorPhoneNumber,
                    DoctorEmailAddress = doctorModel.DoctorEmailAddress,
                    RoomNumber = doctorModel.RoomNumber,
                    Biography = doctorModel.Biography
                };

                if (doctorModel.ImageFile != null && doctorModel.ImageFile.Length > 0)
                {
                    var uploadDir = @"/images/doctor/";
                    var fileName = Path.GetFileNameWithoutExtension(doctorModel.ImageFile.FileName);
                    var extention = Path.GetExtension(doctorModel.ImageFile.FileName);

                    fileName = DateTime.UtcNow.ToString("yymmssfff") + fileName + extention;
                    var path = Path.Combine(_hostingEnviroment.WebRootPath, uploadDir.TrimStart(new char[] { '\\', '/' }), fileName);
                    doctorModel.ImageFile.CopyToAsync(new FileStream(path, FileMode.Create));
                    doctor.DoctorImageUrl = "/" + uploadDir.TrimStart(new char[] { '\\', '/' }) + "/" + fileName;
                }
                _context.Add(doctor);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }

        }

        public Doctor GetDoctor(int id)
        {
            return _context.Doctor.FirstOrDefault(d => d.DoctorId == id);
        } 

        public bool Update(int id, DoctorIndexVM doctorModel)
        {
            var doctor = GetDoctor(id);
            try
            {
                doctor.DoctorPhoneNumber = doctorModel.DoctorPhoneNumber;
                doctor.DoctorEmailAddress = doctorModel.DoctorEmailAddress;
                doctor.RoomNumber = doctorModel.RoomNumber;
                doctor.Biography = doctorModel.Biography;
              
                _context.Update(doctor);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public DoctorIndexVM Details(int id)
        {
            var doctor = GetDoctor(id);
            DoctorIndexVM doctorModel = new DoctorIndexVM
            {
                DoctorId = doctor.DoctorId,
                DoctorFirstName = doctor.DoctorFirstName,
                DoctorLastName = doctor.DoctorLastName,
                Speciality = doctor.Speciality,
                ImageUrl = doctor.DoctorImageUrl,
                DoctorPhoneNumber = doctor.DoctorPhoneNumber,
                DoctorEmailAddress = doctor.DoctorEmailAddress,
                RoomNumber = doctor.RoomNumber,
                Biography = doctor.Biography
            };
            return doctorModel;
        }

        public bool Delete(DoctorIndexVM doctorModel)
        {
            Doctor doctor = GetDoctor(doctorModel.DoctorId);
            try
            {
                _context.Doctor.Remove(doctor);
                _context.SaveChanges();
                
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return true;

        }

        public IEnumerable<DoctorIndexVM> GetDoctorsBySpeciality(string speciality)
        {
            var doctor = _context.Doctor.Where(d => d.Speciality == speciality);
            return doctor.Select(d => new DoctorIndexVM
            {
                DoctorId = d.DoctorId,
                DoctorFirstName = d.DoctorFirstName,
                DoctorLastName = d.DoctorLastName,
                Speciality = d.Speciality,
                ImageUrl = d.DoctorImageUrl,
                DoctorPhoneNumber = d.DoctorPhoneNumber,
                DoctorEmailAddress = d.DoctorEmailAddress,
                RoomNumber = d.RoomNumber,
                Biography = d.Biography
            });
        }
    }
}
