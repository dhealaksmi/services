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
    public class DosenCoursesController : ControllerBase
    {
        private IDosenCourse _dosencourse;

        public DosenCoursesController(IDosenCourse dosencourse)
        {
            _dosencourse = dosencourse;
        }
        // GET: api/<DosenCoursesController>
        [HttpGet]
        public async Task<IEnumerable<DosenCourse>> Get()
        {
            var results = await _dosencourse.GetAll();
            return results;
        }

        // GET api/<DosenCoursesController>/5
        [HttpGet("{id}")]
        public async Task<DosenCourse> Get(int id)
        {
            var result = await _dosencourse.GetById(id.ToString());
            return result;
        }

        // POST api/<DosenCoursesController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] DosenCourse dosencourse)
        {
            try
            {
                await _dosencourse.Insert(dosencourse);
                return Ok($"DosenCourse {dosencourse.IdDosenCourse} berhasil ditambahkan");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<DosenCoursesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<DosenCoursesController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _dosencourse.Delete(id.ToString());
                return Ok($"DosenCourse {id} berhasil didelete");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
