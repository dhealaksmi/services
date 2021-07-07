using Microsoft.AspNetCore.Mvc;
using PraProjectBNI.Data;
using PraProjectBNI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PraProjectBNI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private IStudent _student;

        public StudentsController(IStudent student)
        {
            _student = student;
        }
        // GET: api/<StudentsController>
        [HttpGet]
        public async Task<IEnumerable<Student>> Get()
        {
            var results = await _student.GetAll();
            return results;
        }

        // GET api/<StudentsController>/5
        [HttpGet("{id}")]
        public async Task<Student> Get(int id)
        {
            var result = await _student.GetById(id.ToString());
            return result;
        }

        // POST api/<StudentsController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Student student)
        {
            try
            {
                await _student.Insert(student);
                return Ok($"Data Student {student.Nama} berhasil ditambahkan");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<StudentsController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Student student)
        {
            try
            {
                await _student.Update(id.ToString(), student);
                return Ok($"Data Student ID {id} berhasil diupdate");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<StudentsController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _student.Delete(id.ToString());
                return Ok($"Data student {id} berhasil didelete");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
