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
    public class StudentController : ControllerBase
    {
        private IStudent _student;

        public StudentController(IStudent student)
        {
            _student = student;
        }
        // GET: api/<ValuesController>
        [HttpGet]
        public IEnumerable<Student> Get()
        {
            return _student.GetByName();
        }

        // GET api/<ValuesController>/5
      // [HttpGet("{id}")]
       //public string Get(int id)
       //{
         //   return _student.GetByName(id.ToString());
       //}

        [HttpGet]
        [Route("StudentByName/{name}")]
        public IEnumerable<Student> StudentByName(string name)
        {
            return _student.GetByName(Student: name);
        }

        [HttpGet]
        [Route("StudentQuery")]
        public string StudentQuery([FromQuery] Student Nama)
        {
            return $"{Nama}";
        }

        // POST api/<ValuesController>
        [HttpPost]
        public IActionResult Post([FromBody] Student student)
        {
            try
            {
                _student.Insert(student);
                return Ok($"Berhasil menambahkan data student {student.Nama}");
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}");
            }
        }

        [HttpPost]
        [Route("PostWithID")]
        public IActionResult PostWithID([FromBody] Student student)
        {
            try
            {
                var result = _student.InsertWithIndentity(student);
                return Ok($"Berhasil menambahkan Student ID: {result}");
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}");
            }
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id,[FromBody] Student student)
        {
            try
            {
                _student.Update(id.ToString(), student);
                return Ok($"Data student id: {id} berhasil di update");
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _student.Delete(id.ToString());
                return Ok($"Data student id:{id} berhasil dihapus");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
