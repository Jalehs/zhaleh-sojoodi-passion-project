using passion_project.Model;
using passion_project.ViewModel.DoctorPatient;
using passion_project.ViewModel.Patient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace passion_project.Repository
{
    public class DoctorPatientRepository
    {
        private readonly ApplicationDbContext _context;

        public DoctorPatientRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        

    }
}
