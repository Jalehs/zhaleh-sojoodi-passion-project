using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using passion_project.Models.AppointmentSystem;
using passion_project.ViewModel;
using passion_project.ViewModel.Patient;

namespace passion_project.Model
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }


        public virtual DbSet<Appointment> Appointment { get; set; }
        public virtual DbSet<Doctor> Doctor { get; set; }
        public virtual DbSet<DoctorPatient> DoctorPatient { get; set; }
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

                entity.Property(e => e.DoctorPatientId).HasColumnName("doctor_patient_id");

                entity.HasOne(d => d.DoctorPatient)
                    .WithMany(p => p.Appointment)
                    .HasForeignKey(d => d.DoctorPatientId)
                    .HasConstraintName("FK__Appointme__docto__2C3393D0");
            });

            //modelBuilder.Entity<AspNetRoleClaims>(entity =>
            //{
            //    entity.HasIndex(e => e.RoleId);

            //    entity.Property(e => e.RoleId).IsRequired();

            //    entity.HasOne(d => d.Role)
            //        .WithMany(p => p.AspNetRoleClaims)
            //        .HasForeignKey(d => d.RoleId);
            //});

            //modelBuilder.Entity<AspNetRoles>(entity =>
            //{
            //    entity.HasIndex(e => e.NormalizedName)
            //        .HasName("RoleNameIndex")
            //        .IsUnique()
            //        .HasFilter("([NormalizedName] IS NOT NULL)");

            //    entity.Property(e => e.Id).ValueGeneratedNever();

            //    entity.Property(e => e.Name).HasMaxLength(256);

            //    entity.Property(e => e.NormalizedName).HasMaxLength(256);
            //});

            //modelBuilder.Entity<AspNetUserClaims>(entity =>
            //{
            //    entity.HasIndex(e => e.UserId);

            //    entity.Property(e => e.UserId).IsRequired();

            //    entity.HasOne(d => d.User)
            //        .WithMany(p => p.AspNetUserClaims)
            //        .HasForeignKey(d => d.UserId);
            //});

            //modelBuilder.Entity<AspNetUserLogins>(entity =>
            //{
            //    entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

            //    entity.HasIndex(e => e.UserId);

            //    entity.Property(e => e.LoginProvider).HasMaxLength(128);

            //    entity.Property(e => e.ProviderKey).HasMaxLength(128);

            //    entity.Property(e => e.UserId).IsRequired();

            //    entity.HasOne(d => d.User)
            //        .WithMany(p => p.AspNetUserLogins)
            //        .HasForeignKey(d => d.UserId);
            //});

            //modelBuilder.Entity<AspNetUserRoles>(entity =>
            //{
            //    entity.HasKey(e => new { e.UserId, e.RoleId });

            //    entity.HasIndex(e => e.RoleId);

            //    entity.HasOne(d => d.Role)
            //        .WithMany(p => p.AspNetUserRoles)
            //        .HasForeignKey(d => d.RoleId);

            //    entity.HasOne(d => d.User)
            //        .WithMany(p => p.AspNetUserRoles)
            //        .HasForeignKey(d => d.UserId);
            //});

            //modelBuilder.Entity<AspNetUserTokens>(entity =>
            //{
            //    entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

            //    entity.Property(e => e.LoginProvider).HasMaxLength(128);

            //    entity.Property(e => e.Name).HasMaxLength(128);

            //    entity.HasOne(d => d.User)
            //        .WithMany(p => p.AspNetUserTokens)
            //        .HasForeignKey(d => d.UserId);
            //});

            //modelBuilder.Entity<AspNetUsers>(entity =>
            //{
            //    entity.HasIndex(e => e.NormalizedEmail)
            //        .HasName("EmailIndex");

            //    entity.HasIndex(e => e.NormalizedUserName)
            //        .HasName("UserNameIndex")
            //        .IsUnique()
            //        .HasFilter("([NormalizedUserName] IS NOT NULL)");

            //    entity.Property(e => e.Id).ValueGeneratedNever();

            //    entity.Property(e => e.Email).HasMaxLength(256);

            //    entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

            //    entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

            //    entity.Property(e => e.UserName).HasMaxLength(256);
            //});

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

            modelBuilder.Entity<DoctorPatient>(entity =>
            {
                entity.Property(e => e.DoctorPatientId).HasColumnName("doctor_patient_id");

                entity.Property(e => e.DoctorId).HasColumnName("doctor_id");

                entity.Property(e => e.PatientId).HasColumnName("patient_id");

                entity.HasOne(d => d.Doctor)
                    .WithMany(p => p.DoctorPatient)
                    .HasForeignKey(d => d.DoctorId)
                    .HasConstraintName("FK__DoctorPat__docto__286302EC");

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.DoctorPatient)
                    .HasForeignKey(d => d.PatientId)
                    .HasConstraintName("FK__DoctorPat__patie__29572725");
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



        public DbSet<passion_project.ViewModel.DoctorIndexVM> DoctorIndexVM { get; set; }



        public DbSet<passion_project.ViewModel.Patient.PatientIndexVM> PatientIndexVM { get; set; }



        public DbSet<passion_project.ViewModel.Patient.PatientCreateVM> PatientCreateVM { get; set; }
    }
}
