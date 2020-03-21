using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace passion_project.ViewModel
{
    public class DoctorIndexVM
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DoctorId { get; set; }
        [DisplayName("First Name")]
        [Required(ErrorMessage = "First Name is required.")]
        public string DoctorFirstName { get; set; }
        [Required(ErrorMessage = "Last Name is required.")]
        [DisplayName("Last Name")]
        public string DoctorLastName { get; set; }
        public string Speciality { get; set; }
        public string ImageUrl { get; set; }
        [Required(ErrorMessage = "Phone Number is required.")]
        [DisplayName("Phone Number")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Invalid phone number")]
        public string DoctorPhoneNumber { get; set; }
        [DisplayName("Email Address")]
        [EmailAddress(ErrorMessage = "Invalid email Address")]
        public string DoctorEmailAddress { get; set; }
        [Required(ErrorMessage = "Room Number is required.")]
        [DisplayName("Room Number")]
        public int? RoomNumber { get; set; }
        public string Biography { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime AppointmentDate { get; set; }
    }
}
