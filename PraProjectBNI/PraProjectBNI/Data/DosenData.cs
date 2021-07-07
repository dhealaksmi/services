using Microsoft.EntityFrameworkCore;
using PraProjectBNI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PraProjectBNI.Data
{
    public class DosenData : IDosen
    {
        private PraBNIContext _db;

        public DosenData(PraBNIContext db)
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
                    _db.Dosens.Remove(result);
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

        public async Task<IEnumerable<Dosen>> GetAll()
        {
            var results = await (from d in _db.Dosens
                                 orderby d.NamaDosen
                                 select d).ToListAsync();
            return results;
        }


        public async Task<Dosen> GetById(string id)
        {
            var result = await (from d in _db.Dosens
                                where d.IdDosen == Convert.ToInt32(id)
                                select d).FirstOrDefaultAsync();
            var dosenCourses = await (from dc in _db.DosenCourses.Include(dc => dc.IdCourseNavigation)
                                     where dc.IdDosen == Convert.ToInt32(id)
                                     select dc).AsNoTracking().ToListAsync();
            result.DosenCourses = dosenCourses;

            return result;
        }

        public async Task Insert(Dosen obj)
        {
            try
            {
                _db.Dosens.Add(obj);
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

        public async Task Update(string id, Dosen obj)
        {
            try
            {
                var result = await GetById(id);
                //cek apakah data sudah ada
                if (result != null)
                {
                    //_db.Update(obj);
                    result.NamaDosen = obj.NamaDosen;
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
