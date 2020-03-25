using System;
using System.Collections.Generic;

namespace passion_project.Models.HealthCenter
{
    public partial class Patient
    {
        public Patient()
        {
            Appointment = new HashSet<Appointment>();
        }

        public int PatientId { get; set; }
        public string PatientFirstName { get; set; }
        public string PatientLastName { get; set; }
        public int? Phn { get; set; }
        public string PatientGender { get; set; }
        public DateTime? PatientBirthDate { get; set; }
        public string PatientImageUrl { get; set; }
        public string PatientPhoneNumber { get; set; }
        public string PatientEmailAddress { get; set; }
        public string PatientAddress { get; set; }
        public string PatientCity { get; set; }
        public string PatientPostalCode { get; set; }
        public string PatientHistory { get; set; }

        public virtual ICollection<Appointment> Appointment { get; set; }
    }
}
