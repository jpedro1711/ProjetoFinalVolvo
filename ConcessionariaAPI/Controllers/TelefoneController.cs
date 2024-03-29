using ConcessionariaAPI.Exceptions;
using ConcessionariaAPI.Models;
using ConcessionariaAPI.Models.dtos;
using ConcessionariaAPI.Services;
using ConcessionariaAPI.Services.interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConcessionariaAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TelefoneController : ControllerBase
    {
        private ITelefoneService _service;

        public TelefoneController()
        {
            _service = new TelefoneService(new ConcessionariaContext());
        }
        [HttpPost]
        public async Task<Telefone> Create([FromBody] TelefoneDto telefone)
        {
            return await _service.Create(telefone);
        }

        [HttpGet]
        public async Task<List<Telefone>> GetAll()
        {
            return await _service.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var result = await _service.GetById(id);
                return Ok(result);
            }
            catch (EntityException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] TelefoneDto updatedTelefone)
        {
            try
            {
                var result = await _service.Update(id, updatedTelefone);
                return Ok(result);
            }
            catch (EntityException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _service.Delete(id);
                return NoContent();
            }
            catch (EntityException e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}