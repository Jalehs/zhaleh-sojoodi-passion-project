using System;
using System.Collections.Generic;

namespace passion_project.Models.AppointmentSystem
{
    public partial class Doctor
    {
        public Doctor()
        {
            DoctorPatient = new HashSet<DoctorPatient>();
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

        public virtual ICollection<DoctorPatient> DoctorPatient { get; set; }
    }
}
