﻿using passion_project.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace passion_project.ViewModel.Appointment
{
    public class AppointmentVM
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AppointmentId { get; set; }
        public int DoctorId { get; set; }
        public int PatientId { get; set; }
        [DisplayName("First Name")]
        public string PatientFirstName { get; set; }
        [DisplayName("Last Name")]
        public string PatientLastName { get; set; }
        [EmailAddress(ErrorMessage = "Invalid email Address")]
        public string PatientEmailAddress { get; set; }
        [DisplayName("First Name")]
        //[Required(ErrorMessage = "First Name is required.")]
        public string DoctorFirstName { get; set; }
        //[Required(ErrorMessage = "Last Name is required.")]
        [DisplayName("Last Name")]
        public string DoctorLastName { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Appointment Date is required.")]
        [CheckDate(ErrorMessage = "Appointment Date must not be in the past.")]
        [CheckDateWeekend(ErrorMessage = "Appointment Date must not be on the weekend.")]
        public DateTime AppointmentDate { get; set; }
        [Required(ErrorMessage = "Appointment Time is required.")]
        public TimeSpan AppointmentTime { get; set; }
        public string AppointmentSummery { get; set; }
 
    }
}
