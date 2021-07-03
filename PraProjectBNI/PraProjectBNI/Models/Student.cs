using System;
using System.Collections.Generic;

#nullable disable

namespace PraProjectBNI.Models
{
    public partial class Student
    {
        public Student()
        {
            Enrollments = new HashSet<Enrollment>();
        }

        public int IdStudent { get; set; }
        public string Nama { get; set; }
        public string Domisili { get; set; }
        public int? Angkatan { get; set; }
        public string JenisKelamin { get; set; }

        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
}
