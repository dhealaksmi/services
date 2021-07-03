using System;
using System.Collections.Generic;

#nullable disable

namespace PraProjectBNI.Models
{
    public partial class Enrollment
    {
        public int IdEnroll { get; set; }
        public int IdCourse { get; set; }
        public int IdStudent { get; set; }

        public virtual Course IdCourseNavigation { get; set; }
        public virtual Student IdStudentNavigation { get; set; }
    }
}
