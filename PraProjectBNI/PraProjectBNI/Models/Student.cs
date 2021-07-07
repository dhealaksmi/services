using System;
using System.Collections.Generic;
using System.Threading.Tasks;

#nullable disable

namespace PraProjectBNI.Models
{
    public class Student
    {
        public int ID_Student { get; set; }
        public string Nama { get; set; }
        public string Domisili { get; set; }
        public int? Angkatan { get; set; }
        public string Jenis_Kelamin { get; set; }
        public object Enrollments { get; internal set; }
        internal void Update(string v, Student student)
        {
            throw new NotImplementedException();
        }

        internal static void Delete(string v)
        {
            throw new NotImplementedException();
        }

        internal static object InsertWithIndentity(Student student)
        {
            throw new NotImplementedException();
        }

        internal static Task GetAll()
        {
            throw new NotImplementedException();
        }

        internal static Dosen GetByName(object Dosen, object Student)
        {
            throw new NotImplementedException();
        }

        internal static Student GetByName(object Student)
        {
            throw new NotImplementedException();
        }

        internal static void Insert(Dosen dosen)
        {
            throw new NotImplementedException();
        }

        internal static void Insert(Student student)
        {
            throw new NotImplementedException();
        }

        internal static object InsertWithIndentity(Dosen dosen)
        {
            throw new NotImplementedException();
        }
    }
}
