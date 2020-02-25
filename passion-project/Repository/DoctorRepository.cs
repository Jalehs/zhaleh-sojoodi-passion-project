using Microsoft.AspNetCore.Hosting.Internal;
using passion_project.Data;
using passion_project.Models.AppointmentSystem;
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
                DoctorPhoneNumber = d.DoctorPhoneNumber
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
                    var uploadDir = "@images/doctor";
                    var fileName = Path.GetFileNameWithoutExtension(doctorModel.ImageFile.FileName);
                    var extention = Path.GetExtension(doctorModel.ImageFile.FileName);
                    var webRootPath = _hostingEnviroment.WebRootPath;
                    fileName = DateTime.UtcNow.ToString("yymmdd") + fileName + extention;
                    var path = Path.Combine(webRootPath, uploadDir, fileName);
                    doctorModel.ImageFile.CopyToAsync(new FileStream(path, FileMode.Create));
                    doctor.DoctorImageUrl = "/" + uploadDir + "/" + fileName;
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
    }
}
