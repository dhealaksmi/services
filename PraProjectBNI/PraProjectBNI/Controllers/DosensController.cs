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
    public class DosensController : ControllerBase
    {
        private IDosen _dosen;

        public DosensController(IDosen dosen)
        {
            _dosen = dosen;
        }
        // GET: api/<DosensController>
        [HttpGet]
        public async Task<IEnumerable<Dosen>> Get()
        {
            var results = await _dosen.GetAll();
            return results;
        }

        // GET api/<DosensController>/5
        [HttpGet("{id}")]
        public async Task<Dosen> Get(int id)
        {
            var result = await _dosen.GetById(id.ToString());
            return result;
        }

        // POST api/<DosensController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Dosen dosen)
        {
            try
            {
                await _dosen.Insert(dosen);
                return Ok($"Data Dosen {dosen.NamaDosen} berhasil ditambahkan");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<DosensController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Dosen dosen)
        {
            try
            {
                await _dosen.Update(id.ToString(), dosen);
                return Ok($"Data Dosen ID {id} berhasil diupdate");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<DosensController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _dosen.Delete(id.ToString());
                return Ok($"Data dosen {id} berhasil didelete");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
