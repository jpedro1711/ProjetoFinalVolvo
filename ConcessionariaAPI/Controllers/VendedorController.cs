using ConcessionariaAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ConcessionariaAPI.Services;
using ConcessionariaAPI.Exceptions;
using ConcessionariaAPI.Repositories.Dto;
using Microsoft.Extensions.Logging.EventLog;

namespace ConcessionariaAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VendedorController : ControllerBase
    {

        private IVendedorService<Vendedor> _service;
        private LogService LogService { get; set; }
        public VendedorController()
        {
            _service = new VendedorService(new ConcessionariaContext());
        }

         [HttpPost]
        public async Task<Vendedor> Create([FromBody] Vendedor vendedor)
        {
            try
            {
                return await _service.Create(vendedor);
            }
            catch (Exception ex)
            {
                LogService.SaveLog(ex.StackTrace);
                return null;
            }
        }

        [HttpGet]
        public Task<List<Vendedor>> GetAll()
        {
            return _service.GetAll();
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
        
        [HttpGet("Salario/{id}/{mes}/{ano}")]
        public async Task<IActionResult> GetSalarioMesAno(int id, int mes, int ano)
        {
             try
            {
                var result = await _service.GetSalarioMesAno(id, mes, ano);
                return Ok(result);
            }
            catch (EntityException e)
            {
                return NotFound();
            }
        }

       [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Vendedor updatedVendedor)
        {
            try
            {
                var result = await _service.Update(id, updatedVendedor);
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
