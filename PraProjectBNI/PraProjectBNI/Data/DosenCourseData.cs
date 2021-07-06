using Microsoft.EntityFrameworkCore;
using PraProjectBNI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PraProjectBNI.Data
{
    public class DosenCourseData : IDosenCourse
    {
        private PraBNIContext _db;

        public DosenCourseData(PraBNIContext db)
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
                    _db.DosenCourses.Remove(result);
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

        public async Task<IEnumerable<DosenCourse>> GetAll()
        {
            var results = await _db.DosenCourses.Include(d => d.IdDosenNavigation).Include(d => d.IdCourseNavigation)
               .OrderBy(d => d.IdCourse).AsNoTracking().ToListAsync();
            return results;
        }

        public async Task<DosenCourse> GetById(string id)
        {
            var result = await (from d in _db.DosenCourses
                                where d.IdDosenCourse == Convert.ToInt32(id)
                                select d).FirstOrDefaultAsync();
            return result;
        }

        public async Task Insert(DosenCourse obj)
        {
            try
            {
                _db.DosenCourses.Add(obj);
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

        public Task Update(string id, DosenCourse obj)
        {
            throw new NotImplementedException();
        }
    }
}
