using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace passion_project.ViewModel.Appointment
{
    public class AppointmentVM
    {
        public int PatientId { get; set; }
        [DisplayName("First Name")]
        [Required(ErrorMessage = "First Name is required.")]
        public string PatientFirstName { get; set; }
        [DisplayName("Last Name")]
        [Required(ErrorMessage = "Last Name is required.")]
        public string PatientLastName { get; set; }
    }
}
