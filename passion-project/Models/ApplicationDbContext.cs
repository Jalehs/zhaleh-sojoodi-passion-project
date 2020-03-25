using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using passion_project.ViewModel.Appointment;
using passion_project.ViewModel.Patient;

namespace passion_project.Models.HealthCenter
{
    public partial class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Appointment> Appointment { get; set; }
        public virtual DbSet<Doctor> Doctor { get; set; }
        public virtual DbSet<Patient> Patient { get; set; }

       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<Appointment>(entity =>
            {
                entity.Property(e => e.AppointmentId).HasColumnName("appointment_id");

                entity.Property(e => e.AppointmentDate)
                    .HasColumnName("appointment_date")
                    .HasColumnType("date");

                entity.Property(e => e.AppointmentSummery)
                    .HasColumnName("appointment_summery")
                    .HasColumnType("text");

                entity.Property(e => e.AppointmentTime).HasColumnName("appointment_time");

                entity.Property(e => e.DoctorId).HasColumnName("doctor_id");

                entity.Property(e => e.PatientId).HasColumnName("patient_id");

                entity.HasOne(d => d.Doctor)
                    .WithMany(p => p.Appointment)
                    .HasForeignKey(d => d.DoctorId)
                    .HasConstraintName("FK__Appointme__docto__286302EC");

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.Appointment)
                    .HasForeignKey(d => d.PatientId)
                    .HasConstraintName("FK__Appointme__patie__29572725");
            });

            modelBuilder.Entity<Doctor>(entity =>
            {
                entity.Property(e => e.DoctorId).HasColumnName("doctor_id");

                entity.Property(e => e.Biography)
                    .HasColumnName("biography")
                    .HasColumnType("text");

                entity.Property(e => e.DoctorEmailAddress)
                    .HasColumnName("doctor_email_address")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DoctorFirstName)
                    .HasColumnName("doctor_first_name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DoctorImageUrl)
                    .HasColumnName("doctor_image_url")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.DoctorLastName)
                    .HasColumnName("doctor_last_name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DoctorPhoneNumber)
                    .HasColumnName("doctor_phone_number")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.RoomNumber).HasColumnName("room_number");

                entity.Property(e => e.Speciality)
                    .HasColumnName("speciality")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Patient>(entity =>
            {
                entity.Property(e => e.PatientId).HasColumnName("patient_id");

                entity.Property(e => e.PatientAddress)
                    .HasColumnName("patient_address")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.PatientBirthDate)
                    .HasColumnName("patient_birth_date")
                    .HasColumnType("date");

                entity.Property(e => e.PatientCity)
                    .HasColumnName("patient_city")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.PatientEmailAddress)
                    .HasColumnName("patient_email_address")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PatientFirstName)
                    .HasColumnName("patient_first_name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PatientGender)
                    .HasColumnName("patient_gender")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.PatientHistory)
                    .HasColumnName("patient_history")
                    .HasColumnType("text");

                entity.Property(e => e.PatientImageUrl)
                    .HasColumnName("patient_image_url")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.PatientLastName)
                    .HasColumnName("patient_last_name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PatientPhoneNumber)
                    .HasColumnName("patient_phone_number")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.PatientPostalCode)
                    .HasColumnName("patient_postal_code")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Phn).HasColumnName("phn");
            });
        }

       
        public DbSet<passion_project.ViewModel.Appointment.AppointmentVM> AppointmentVM { get; set; }

       
        public DbSet<passion_project.ViewModel.Patient.PatientVM> PatientVM { get; set; }
    }
}
