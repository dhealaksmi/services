using System;
using System.Collections.Generic;

#nullable disable

namespace PraProjectBNI.Models
{
    public partial class DosenCourse
    {
        public int IdDosenCourse { get; set; }
        public int IdDosen { get; set; }
        public int IdCourse { get; set; }

        public virtual Course IdCourseNavigation { get; set; }
        public virtual Dosen IdDosenNavigation { get; set; }
    }
}
