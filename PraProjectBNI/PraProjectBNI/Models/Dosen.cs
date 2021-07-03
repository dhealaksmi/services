using System;
using System.Collections.Generic;

#nullable disable

namespace PraProjectBNI.Models
{
    public partial class Dosen
    {
        public Dosen()
        {
            DosenCourses = new HashSet<DosenCourse>();
        }

        public int IdDosen { get; set; }
        public string NamaDosen { get; set; }

        public virtual ICollection<DosenCourse> DosenCourses { get; set; }
    }
}
