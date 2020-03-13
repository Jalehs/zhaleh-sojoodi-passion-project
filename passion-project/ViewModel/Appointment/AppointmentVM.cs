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
        [DisplayName("First Name")]
        [Required(ErrorMessage = "First Name is required.")]
        public string PatientFirstName { get; set; }
        [DisplayName("Last Name")]
        [Required(ErrorMessage = "Last Name is required.")]
        public string PatientLastName { get; set; }
        [DisplayName("First Name")]
        [Required(ErrorMessage = "First Name is required.")]
        public string DoctorFirstName { get; set; }
        [Required(ErrorMessage = "Last Name is required.")]
        [DisplayName("Last Name")]
        public string DoctorLastName { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Appointment Date is required.")]
        public DateTime? AppointmentDate { get; set; }
        [Required(ErrorMessage = "Appointment Time is required.")]
        public TimeSpan? AppointmentTime { get; set; }
        public string AppointmentSummery { get; set; }

    }
}
