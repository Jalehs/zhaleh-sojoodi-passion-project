using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace passion_project.ViewModel
{
    public class DoctorCreateVM
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DoctorId { get; set; }
        [DisplayName("First Name")]
        [Required(ErrorMessage = "First Name is required.")]
        public string DoctorFirstName { get; set; }
        [Required(ErrorMessage = "Last Name is required.")]
        [DisplayName("Last Name")]
        public string DoctorLastName { get; set; }
        public string Speciality { get; set; }
        public IFormFile ImageFile { get; set; }
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
    }
}
