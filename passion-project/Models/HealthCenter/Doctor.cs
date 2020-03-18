using System;
using System.Collections.Generic;

namespace passion_project.Models.HealthCenter
{
    public partial class Doctor
    {
        public Doctor()
        {
            Appointment = new HashSet<Appointment>();
        }

        public int DoctorId { get; set; }
        public string DoctorFirstName { get; set; }
        public string DoctorLastName { get; set; }
        public string Speciality { get; set; }
        public string DoctorImageUrl { get; set; }
        public string DoctorPhoneNumber { get; set; }
        public string DoctorEmailAddress { get; set; }
        public int? RoomNumber { get; set; }
        public string Biography { get; set; }

        public virtual ICollection<Appointment> Appointment { get; set; }
    }

    public enum Speciality
    {
        Dentists,
        Ophthalmologists,
        FamilyPhysicians,
        ENTSpecialists
    }
}
