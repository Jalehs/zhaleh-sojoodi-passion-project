using System;
using System.Collections.Generic;

namespace passion_project.Models.AppointmentSystem
{
    public partial class Appointment
    {
        public int AppointmentId { get; set; }
        public int? DoctorPatientId { get; set; }
        public DateTime? AppointmentDate { get; set; }
        public TimeSpan? AppointmentTime { get; set; }
        public string AppointmentSummery { get; set; }

        public virtual DoctorPatient DoctorPatient { get; set; }
    }
}
