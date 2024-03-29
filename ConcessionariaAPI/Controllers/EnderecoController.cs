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
    public class EnderecoController : ControllerBase
    {
        private IEnderecoService _enderecoService;

        public EnderecoController()
        {
            _enderecoService = new EnderecoService(new ConcessionariaContext());
        }

        [HttpPost]
        public async Task<Endereco> Create([FromBody] EnderecoDto endereco)
        {
            return await _enderecoService.Create(endereco);
        }

        [HttpGet]
        public async Task<List<Endereco>> GetAll()
        {
            return await _enderecoService.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var result = await _enderecoService.GetById(id);
                return Ok(result);
            }
            catch (EntityException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] EnderecoDto updatedEndereco)
        {
            try
            {
                var result = await _enderecoService.Update(id, updatedEndereco);
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
                _enderecoService.Delete(id);
                return NoContent();
            }
            catch (EntityException e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}