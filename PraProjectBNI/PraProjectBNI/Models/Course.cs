using System;
using System.Collections.Generic;

#nullable disable

namespace PraProjectBNI.Models
{
    public partial class Course
    {
        public Course()
        {
            DosenCourses = new HashSet<DosenCourse>();
            Enrollments = new HashSet<Enrollment>();
        }

        public int IdCourse { get; set; }
        public string NamaCourse { get; set; }
       // public int? JumlahMahasiswa { get; set; }

        public virtual ICollection<DosenCourse> DosenCourses { get; set; }
        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
}
