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
    public class DespesaController : ControllerBase
    {
        private IDespesaService _service;

        public DespesaController()
        {
            _service = new DespesaService(new ConcessionariaContext());
        }        

        [HttpPost]
        public async Task<Despesa> Create([FromBody] DespesaDto despesa)
        {
            return await _service.Create(despesa);
        }

        [HttpGet]
        public async Task<List<Despesa>> GetAll()
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
        public async Task<IActionResult> Update(int id, [FromBody] DespesaDto updatedDespesa)
        {
            try
            {
                var result = await _service.Update(id, updatedDespesa);
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