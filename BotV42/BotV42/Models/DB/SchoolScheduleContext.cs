using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BotV42.Models.DB
{
    public partial class SchoolScheduleContext : DbContext
    {
        public SchoolScheduleContext()
        {
        }

        public SchoolScheduleContext(DbContextOptions<SchoolScheduleContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Classrooms> Classrooms { get; set; }
        public virtual DbSet<DayOfSweek> DayOfSweek { get; set; }
        public virtual DbSet<Lessons> Lessons { get; set; }
        public virtual DbSet<Scheduler> Scheduler { get; set; }
        public virtual DbSet<Shours> Shours { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=SchoolSchedule;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Classrooms>(entity =>
            {
                entity.HasKey(e => e.IdClassroom);

                entity.Property(e => e.IdClassroom).HasColumnName("id_classroom");

                entity.Property(e => e.Floor).HasColumnName("floor");

                entity.Property(e => e.Nr)
                    .HasColumnName("nr")
                    .HasMaxLength(5)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<DayOfSweek>(entity =>
            {
                entity.HasKey(e => e.IdDay);

                entity.ToTable("DayOfSWeek");

                entity.Property(e => e.IdDay).HasColumnName("id_day");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(16)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Lessons>(entity =>
            {
                entity.HasKey(e => e.IdLesson);

                entity.Property(e => e.IdLesson).HasColumnName("id_lesson");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(24)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Scheduler>(entity =>
            {
                entity.HasKey(e => e.IdScheduler);

                entity.ToTable("scheduler");

                entity.Property(e => e.IdScheduler).HasColumnName("id_scheduler");

                entity.Property(e => e.IdFClss).HasColumnName("id_fClss");

                entity.Property(e => e.IdFDow).HasColumnName("id_fDOW");

                entity.Property(e => e.IdFH).HasColumnName("id_fH"); 

                entity.Property(e => e.IdFLssn).HasColumnName("id_fLssn"); 

                entity.HasOne(d => d.IdFClssNavigation)
                    .WithMany(p => p.Scheduler)
                    .HasForeignKey(d => d.IdFClss)
                    .HasConstraintName("FK__scheduler__id_fC__2E1BDC42");

                entity.HasOne(d => d.IdFDowNavigation)
                    .WithMany(p => p.Scheduler)
                    .HasForeignKey(d => d.IdFDow)
                    .HasConstraintName("FK__scheduler__id_fD__2B3F6F97");

                entity.HasOne(d => d.IdFHNavigation)
                    .WithMany(p => p.Scheduler)
                    .HasForeignKey(d => d.IdFH)
                    .HasConstraintName("FK__scheduler__id_fH__2D27B809");

                entity.HasOne(d => d.IdFLssnNavigation)
                    .WithMany(p => p.Scheduler)
                    .HasForeignKey(d => d.IdFLssn)
                    .HasConstraintName("FK__scheduler__id_fL__2C3393D0");
            });

            modelBuilder.Entity<Shours>(entity =>
            {
                entity.HasKey(e => e.IdHour);

                entity.ToTable("SHours");

                entity.Property(e => e.IdHour).HasColumnName("id_hour");

                entity.Property(e => e.MinutesEnd).HasColumnName("minutesEnd");

                entity.Property(e => e.MinutesStart).HasColumnName("minutesStart");

                entity.Property(e => e.TimeEnd)
                    .HasColumnName("timeEnd")
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.TimeStart)
                    .HasColumnName("timeStart")
                    .HasMaxLength(6)
                    .IsUnicode(false);
            });
        }
    }
}
