using PraProjectBNI.Models;
using PraProjectBNI.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace PraProjectBNI.Data
{
    public class IStudent : ICrud<Student>
    {
        private PraBNIContext _db;

        public async Task<IEnumerable<IStudent>> GetAll()
        {
            var results = await (from s in _db.Student
                                 orderby s.Nama
                                 select s).ToListAsync();
            return (IEnumerable<IStudent>)results;
        }

        public async Task<Student> GetById(string id)
        {
            var result = await (from s in _db.Student
                                orderby s.ID_Student == Convert.ToInt32(id) select s)
                                .FirstOrDefaultAsync();
            return result;
        }

        public async Task Insert(Student obj)
        {
            try
            {
                _db.Student.Add(obj);
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateException dbEx)
            {
                throw new Exception($"Error: {dbEx.Message}");
            }
        }

        public async Task Delete(string id)
        {
            var result = await GetById(id);
            if (result != null)
            {
                try
                {
                    _db.Student.Remove(result);
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

        Task<IEnumerable<Student>> ICrud<Student>.All => throw new NotImplementedException();

        public Task Update(string id, Student obj)
        {
            throw new NotImplementedException();
        }
    }
}
