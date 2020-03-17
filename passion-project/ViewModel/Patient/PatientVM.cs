using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace passion_project.ViewModel.Patient
{
    public class PatientVM
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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
        public string PatientGender { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Date of Birth is required.")]
        public DateTime? PatientBirthDate { get; set; }
        [DisplayName("Phone Number")]
        [Required(ErrorMessage = "Phone Number is required.")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Invalid phone number")]
        public string PatientPhoneNumber { get; set; }
        [EmailAddress(ErrorMessage = "Invalid email Address")]
        public string PatientEmailAddress { get; set; }
        public string PatientAddress { get; set; }
        public string PatientCity { get; set; }
        [DisplayName("Postal Code")]
        [RegularExpression(@"[ABCEGHJKLMNPRSTVXY][0-9][ABCEGHJKLMNPRSTVWXYZ] ?[0-9]" + "[ABCEGHJKLMNPRSTVWXYZ][0-9]", 
            ErrorMessage = "This is not a valid Canadian postal code.")]
        public string PatientPostalCode { get; set; }
        public string PatientHistory { get; set; }
    }
}
