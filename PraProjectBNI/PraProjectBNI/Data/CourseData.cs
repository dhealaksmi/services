using Microsoft.EntityFrameworkCore;
using PraProjectBNI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PraProjectBNI.Data
{
    public class CourseData : ICourse
    {
        private PraBNIContext _db;

        public CourseData(PraBNIContext db)
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
                    _db.Courses.Remove(result);
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

        public async Task<IEnumerable<Course>> GetAll()
        {
            var results = await (from s in _db.Courses
                                 orderby s.NamaCourse
                                 select s).ToListAsync();
            return results;
        }

        public async Task<Course> GetById(string id)
        {
            var result = await (from c in _db.Courses
                                where c.IdCourse == Convert.ToInt32(id)
                                select c).FirstOrDefaultAsync();
            var enrollments = await (from e in _db.Enrollments.Include(e => e.IdStudentNavigation)
                                     where e.IdCourse == Convert.ToInt32(id)
                                     select e).AsNoTracking().ToListAsync();
            result.Enrollments = enrollments;

            return result;
        }

        public async Task Insert(Course obj)
        {
            try
            {
                _db.Courses.Add(obj);
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

        public async Task Update(string id, Course obj)
        {
            try
            {
                var result = await GetById(id);
                //cek apakah data sudah ada
                if (result != null)
                {
                    //_db.Update(obj);
                    result.NamaCourse = obj.NamaCourse;
                    await _db.SaveChangesAsync();
                }
                else
                {
                    throw new Exception($"Data id:{id} tidak ditemukan");
                }
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
}
