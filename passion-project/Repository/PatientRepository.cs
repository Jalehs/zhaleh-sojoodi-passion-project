using passion_project.Models.HealthCenter;
using passion_project.ViewModel.Patient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace passion_project.Repository
{
    public class PatientRepository
    {
        private readonly ApplicationDbContext _context;

        public PatientRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public IEnumerable<PatientVM> GetAll(string sortOrder)
        {
            var patients = _context.Patient.Select(p => new PatientVM
            {
               PatientId = p.PatientId,
               PatientFirstName = p.PatientFirstName,
               PatientLastName = p.PatientLastName,
               Phn = p.Phn, 
               PatientGender = p.PatientGender,
               PatientBirthDate = p.PatientBirthDate, 
               PatientPhoneNumber = p.PatientPhoneNumber,
               PatientEmailAddress = p.PatientEmailAddress,
               PatientAddress = p.PatientAddress,
               PatientCity = p.PatientCity,
               PatientPostalCode = p.PatientPostalCode,
               PatientHistory = p.PatientHistory
            });
            if (sortOrder == "name-asc")
            {
                patients = patients.OrderBy(ap => ap.PatientLastName);
            }
            else if (sortOrder == "name-desc")
            {
                patients = patients.OrderByDescending(ap => ap.PatientLastName);
            }
            return patients;
        }

        public Patient GetPatient(int id)
        {
            return _context.Patient.Where(p => p.PatientId == id).FirstOrDefault(); 
        }

        public bool CreatePatient(PatientVM patientModel)
        {
            try
            {
                Patient patient = new Patient
                {
                    PatientFirstName = patientModel.PatientFirstName,
                    PatientLastName = patientModel.PatientLastName,
                    Phn = patientModel.Phn,
                    PatientGender = patientModel.PatientGender,
                    PatientBirthDate = patientModel.PatientBirthDate,
                    PatientEmailAddress = patientModel.PatientEmailAddress,
                    PatientPhoneNumber = patientModel.PatientPhoneNumber,
                    PatientAddress = patientModel.PatientAddress,
                    PatientCity = patientModel.PatientCity,
                    PatientPostalCode = patientModel.PatientPostalCode,
                    PatientHistory = patientModel.PatientHistory
                };
                _context.Patient.Add(patient);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public PatientVM Details(int id)
        {
            var patient = GetPatient(id);
            return new PatientVM
            {
                PatientId = patient.PatientId,
                PatientFirstName = patient.PatientFirstName,
                PatientLastName = patient.PatientLastName,
                Phn = patient.Phn,
                PatientGender = patient.PatientGender,
                PatientBirthDate = patient.PatientBirthDate,
                PatientEmailAddress = patient.PatientEmailAddress,
                PatientPhoneNumber = patient.PatientPhoneNumber,
                PatientAddress = patient.PatientAddress,
                PatientCity = patient.PatientCity,
                PatientPostalCode = patient.PatientPostalCode,
                PatientHistory = patient.PatientHistory,
            };

        }
        public bool Update(int id, PatientVM patientModel)
        {
            try
            {
                var patient = GetPatient(id);
                patient.Phn = patientModel.Phn;
                patient.PatientPhoneNumber = patientModel.PatientPhoneNumber;
                patient.PatientEmailAddress = patientModel.PatientEmailAddress;
                patient.PatientAddress = patientModel.PatientAddress;
                patient.PatientCity = patientModel.PatientCity;
                patient.PatientPostalCode = patientModel.PatientPostalCode;
                patient.PatientHistory = patientModel.PatientHistory;

                _context.Patient.Update(patient);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
            
        }

        public bool Delete(PatientVM patientModel)
        {
            var patient = GetPatient(patientModel.PatientId);
            try
            {
                _context.Patient.Remove(patient);
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
