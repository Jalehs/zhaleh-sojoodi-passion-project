﻿using Microsoft.AspNetCore.Hosting.Internal;
using passion_project.Model;
using passion_project.Models.AppointmentSystem;
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
        private readonly HostingEnvironment _hostingEnviroment;

        public PatientRepository(ApplicationDbContext context, HostingEnvironment hostingEnviroment)
        {
            _context = context;
            _hostingEnviroment = hostingEnviroment;
        }
        public IEnumerable<PatientIndexVM> GetAll()
        {
            return _context.Patient.Select(p => new PatientIndexVM
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
        }

        public Patient GetPatient(int id)
        {
            return _context.Patient.Where(p => p.PatientId == id).FirstOrDefault(); 
        }

        public bool CreatePatient(PatientCreateVM patientModel)
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

        public PatientIndexVM Details(int id)
        {
            var patient = GetPatient(id);
            return new PatientIndexVM
            {
                PatientId = patient.PatientId,
                PatientFirstName = patient.PatientFirstName,
                PatientLastName = patient.PatientLastName,
                Phn = patient.Phn,
                PatientGender = patient.PatientGender,
                PatientBirthDate = patient.PatientBirthDate,
                PatientEmailAddress = patient.PatientEmailAddress,
                PatientPhoneNumber = patient.PatientPhoneNumber,
                PatientAddress = patient .PatientAddress,
                PatientCity = patient.PatientCity,
                PatientPostalCode = patient.PatientPostalCode,
                PatientHistory = patient.PatientPostalCode,
            };

        }
        public bool Update(int id, PatientCreateVM patientModel)
        {
            try
            {
                var patient = GetPatient(id);
                patient.PatientFirstName = patientModel.PatientFirstName;
                patient.PatientLastName = patientModel.PatientLastName;
                patient.Phn = patientModel.Phn;
                patient.PatientPhoneNumber = patientModel.PatientPhoneNumber;
                patient.PatientEmailAddress = patientModel.PatientEmailAddress;
                patient.PatientAddress = patientModel.PatientAddress;
                patient.PatientCity = patientModel.PatientCity;
                patient.PatientPostalCode = patientModel.PatientPostalCode;

                _context.Patient.Update(patient);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
            
        }

        public bool Delete(PatientIndexVM patientModel)
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