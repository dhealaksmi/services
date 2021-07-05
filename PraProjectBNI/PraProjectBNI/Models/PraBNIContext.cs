using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace PraProjectBNI.Models
{
    public partial class PraBNIContext : IdentityDbContext
    {
        public PraBNIContext()
        {
        }

        public PraBNIContext(DbContextOptions<PraBNIContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<Dosen> Dosens { get; set; }
        public virtual DbSet<DosenCourse> DosenCourses { get; set; }
        public virtual DbSet<Enrollment> Enrollments { get; set; }
        public virtual DbSet<Student> Students { get; set; }

        /* protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
         {
             if (!optionsBuilder.IsConfigured)
             {
 #warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                 optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=PraBNI;Trusted_Connection=True;");
             }
         }*/

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Course>(entity =>
            {
                entity.HasKey(e => e.IdCourse);

                entity.ToTable("Course");

                entity.Property(e => e.IdCourse).HasColumnName("ID_Course");

               // entity.Property(e => e.JumlahMahasiswa).HasColumnName("Jumlah_Mahasiswa");

                entity.Property(e => e.NamaCourse)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("Nama_Course");
            });

            modelBuilder.Entity<Dosen>(entity =>
            {
                entity.HasKey(e => e.IdDosen);

                entity.ToTable("Dosen");

                entity.Property(e => e.IdDosen)
                    .ValueGeneratedNever()
                    .HasColumnName("ID_Dosen");

                entity.Property(e => e.NamaDosen)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("Nama_Dosen");
            });

            modelBuilder.Entity<DosenCourse>(entity =>
            {
                entity.HasKey(e => e.IdDosenCourse);

                entity.ToTable("Dosen_Course");

                entity.Property(e => e.IdDosenCourse).HasColumnName("ID_Dosen_Course");

                entity.Property(e => e.IdCourse).HasColumnName("ID_Course");

                entity.Property(e => e.IdDosen).HasColumnName("ID_Dosen");

                entity.HasOne(d => d.IdCourseNavigation)
                    .WithMany(p => p.DosenCourses)
                    .HasForeignKey(d => d.IdCourse)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Dosen_Course_Course");

                entity.HasOne(d => d.IdDosenNavigation)
                    .WithMany(p => p.DosenCourses)
                    .HasForeignKey(d => d.IdDosen)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Dosen_Course_Dosen");
            });

            modelBuilder.Entity<Enrollment>(entity =>
            {
                entity.HasKey(e => e.IdEnroll);

                entity.ToTable("Enrollment");

                entity.Property(e => e.IdEnroll)
                    .ValueGeneratedNever()
                    .HasColumnName("ID_Enroll");

                entity.Property(e => e.IdCourse).HasColumnName("ID_Course");

                entity.Property(e => e.IdStudent).HasColumnName("ID_Student");

                entity.HasOne(d => d.IdCourseNavigation)
                    .WithMany(p => p.Enrollments)
                    .HasForeignKey(d => d.IdCourse)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Enrollment_Course");

                entity.HasOne(d => d.IdStudentNavigation)
                    .WithMany(p => p.Enrollments)
                    .HasForeignKey(d => d.IdStudent)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Enrollment_Student");
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasKey(e => e.IdStudent);

                entity.ToTable("Student");

                entity.Property(e => e.IdStudent)
                    .ValueGeneratedNever()
                    .HasColumnName("ID_Student");

                entity.Property(e => e.Domisili)
                    .HasMaxLength(10)
                    .IsFixedLength(true);

                entity.Property(e => e.JenisKelamin)
                    .HasMaxLength(10)
                    .HasColumnName("Jenis_Kelamin")
                    .IsFixedLength(true);

                entity.Property(e => e.Nama)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
