using System;
using System.Collections.Generic;

namespace passion_project.Models.AppointmentSystem
{
    public partial class DoctorPatient
    {
        public DoctorPatient()
        {
            Appointment = new HashSet<Appointment>();
        }

        public int DoctorPatientId { get; set; }
        public int? DoctorId { get; set; }
        public int? PatientId { get; set; }

        public virtual Doctor Doctor { get; set; }
        public virtual Patient Patient { get; set; }
        public virtual ICollection<Appointment> Appointment { get; set; }
    }
}
