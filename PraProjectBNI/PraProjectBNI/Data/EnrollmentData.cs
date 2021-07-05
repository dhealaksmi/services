using Microsoft.EntityFrameworkCore;
using PraProjectBNI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PraProjectBNI.Data
{
    public class EnrollmentData : IEnrollment
    {
        private PraBNIContext _db;

        public EnrollmentData(PraBNIContext db)
        {
            _db = db;
        }
        public Task Delete(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Enrollment>> GetAll()
        {
            var results = await _db.Enrollments.Include(e => e.IdStudentNavigation).Include(e => e.IdCourseNavigation)
               .OrderBy(e => e.IdCourse).AsNoTracking().ToListAsync();
            return results;
        }

        public Task<Enrollment> GetById(string id)
        {
            throw new NotImplementedException();
        }

        public Task Insert(Enrollment obj)
        {
            throw new NotImplementedException();
        }

        public Task Update(string id, Enrollment obj)
        {
            throw new NotImplementedException();
        }
    }
}
