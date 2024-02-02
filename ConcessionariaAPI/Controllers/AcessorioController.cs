using ConcessionariaAPI.Exceptions;
using ConcessionariaAPI.Models;
using ConcessionariaAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConcessionariaAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AcessorioController : ControllerBase
    {
        private IService<Acessorio> _service;

        public AcessorioController()
        {
            _service = new AcessorioService(new ConcessionariaContext());
        }


        [HttpPost]
        public async Task<Acessorio> Create([FromBody] Acessorio acessorio)
        {
            return await _service.Create(acessorio);
        }

        [HttpGet]
        public async Task<List<Acessorio>> GetAll()
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
        public async Task<IActionResult> Update(int id, [FromBody] Acessorio updatedAcessorio)
        {
            try
            {
                var result = await _service.Update(id, updatedAcessorio);
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