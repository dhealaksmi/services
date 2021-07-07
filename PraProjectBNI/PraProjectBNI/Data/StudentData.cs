using Microsoft.EntityFrameworkCore;
using PraProjectBNI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PraProjectBNI.Data
{
    public class StudentData : IStudent
    {
        private PraBNIContext _db;

        public StudentData(PraBNIContext db)
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
                    _db.Students.Remove(result);
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

        public async Task<IEnumerable<Student>> GetAll()
        {
            var results = await (from s in _db.Students
                                 orderby s.Nama
                                 select s).ToListAsync();
            return results;
        }

        public async Task<Student> GetById(string id)
        {
            var result = await (from s in _db.Students
                                where s.IdStudent == Convert.ToInt32(id)
                                select s).FirstOrDefaultAsync();
            var enrollments = await (from e in _db.Enrollments.Include(e => e.IdCourseNavigation)
                                     where e.IdStudent == Convert.ToInt32(id)
                                     select e).AsNoTracking().ToListAsync();
            result.Enrollments = enrollments;

            return result;
        }

        public async Task Insert(Student obj)
        {
            try
            {
                _db.Students.Add(obj);
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

        public async Task Update(string id, Student obj)
        {
            try
            {
                var result = await GetById(id);
                //cek apakah data sudah ada
                if (result != null)
                {
                    //_db.Update(obj);
                    result.Nama = obj.Nama;
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
