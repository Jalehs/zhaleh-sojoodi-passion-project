using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace passion_project.ViewModel.DoctorPatient
{
    public class DoctorPatientVM
    {
        public int DoctorId { get; set; }
        [DisplayName("First Name")]
        [Required(ErrorMessage = "First Name is required.")]
        public string DoctorFirstName { get; set; }
        [Required(ErrorMessage = "Last Name is required.")]
        [DisplayName("Last Name")]
        public string DoctorLastName { get; set; }
        public string Speciality { get; set; }
        public int PatientId { get; set; }
        [DisplayName("First Name")]
        [Required(ErrorMessage = "First Name is required.")]
        public string PatientFirstName { get; set; }
        [DisplayName("Last Name")]
        [Required(ErrorMessage = "Last Name is required.")]
        public string PatientLastName { get; set; }
        [DisplayName("Patient Health Number")]
        [Required(ErrorMessage = "Patient Health Number is required.")]
        public int? Phn { get; set; }
    }
}
