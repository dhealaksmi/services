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
    public class CoursesController : ControllerBase
    {
        private ICourse _course;

        public CoursesController(ICourse course)
        {
            _course = course;
        }
        // GET: api/<CoursesController>
        [HttpGet]
        public async Task<IEnumerable<Course>> Get()
        {
            var results = await _course.GetAll();
            return results;
        }

        // GET api/<CoursesController>/5
        [HttpGet("{id}")]
        public async Task<Course> Get(int id)
        {
            var result = await _course.GetById(id.ToString());
            return result;
        }

        // POST api/<CoursesController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Course course)
        {
            try
            {
                await _course.Insert(course);
                return Ok($"Data Course {course.NamaCourse} berhasil ditambahkan");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<CoursesController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Course course)
        {
            try
            {
                await _course.Update(id.ToString(), course);
                return Ok($"Data Course ID {id} berhasil diupdate");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<CoursesController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _course.Delete(id.ToString());
                return Ok($"Data course {id} berhasil didelete");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
