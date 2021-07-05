using PraProjectBNI.Models;
using SampleAPI.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PraProjectBNI.Data
{
    public interface IStudent : ICrud<Student>
    {
        IEnumerable<Student> GetByName(string Student);
        Student GetById(int id);
        new void Insert(Student student);
        void Update(int id, Student student);
        void Delete(int id);
        IEnumerable<Student> GetByName();
        object InsertWithIndentity(Student student);
        new void Update(string v, Student student);
        new void Delete(string v);
    }
}
