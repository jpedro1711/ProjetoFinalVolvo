using ConcessionariaAPI.Exceptions;
using ConcessionariaAPI.Models;
using Microsoft.AspNetCore.Mvc;
using ConcessionariaAPI.Services;
using Microsoft.EntityFrameworkCore;
using ConcessionariaAPI.Services.interfaces;

namespace ConcessionariaAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VendaController : Controller
    {

        private IService<Venda> _service;

        public VendaController()
        {
            _service = new VendaServico(new ConcessionariaContext());
        }

        [HttpPost]
        public async Task<Venda> Create([FromBody] Venda venda)
        {
            return await _service.Create(venda);
        }

        [HttpGet]
        public async Task<List<Venda>> GetAll()
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
                return NotFound();
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Venda updatedVenda)
        {
            try
            {
                var result = await _service.Update(id, updatedVenda);
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