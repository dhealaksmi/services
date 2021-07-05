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
        public async Task Delete(string id)
        {
            var result = await GetById(id);
            //cek apakah data ada?
            if (result != null)
            {
                try
                {
                    _db.Enrollments.Remove(result);
                    await _db.SaveChangesAsync();
                }
                catch (DbUpdateException dbEx)
                {
                    throw new Exception($"DbError: {dbEx.Message}");
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error: {ex.Message}");
                }
            }
        }

        public async Task<IEnumerable<Enrollment>> GetAll()
        {
            var results = await _db.Enrollments.Include(e => e.IdStudentNavigation).Include(e => e.IdCourseNavigation)
               .OrderBy(e => e.IdCourse).AsNoTracking().ToListAsync();
            return results;
        }

        public async Task<Enrollment> GetById(string id)
        {
            var result = await (from e in _db.Enrollments
                                where e.IdEnroll == Convert.ToInt32(id)
                                select e).FirstOrDefaultAsync();
            return result;
        }

        public async Task Insert(Enrollment obj)
        {
            try
            {
                _db.Enrollments.Add(obj);
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateException dbEx)
            {
                throw new Exception($"Db Error: {dbEx.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: {ex.Message}");
            }
        }

        public Task Update(string id, Enrollment obj)
        {
            throw new NotImplementedException();
        }
    }
}
