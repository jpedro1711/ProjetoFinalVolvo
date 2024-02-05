using ConcessionariaAPI.Models;
using Microsoft.AspNetCore.Mvc;
using ConcessionariaAPI.Services;
using Microsoft.EntityFrameworkCore;
using ConcessionariaAPI.Exceptions;
using System;
using ConcessionariaAPI.Models.dtos;

namespace ConcessionariaAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VeiculoController : ControllerBase
    {
        private VeiculoService _service;

        public VeiculoController()
        {
            _service = new VeiculoService(new ConcessionariaContext());
        }

        [HttpPost]
        public async Task<Veiculo> Create([FromBody] VeiculoDto veiculo)
        {
            return await _service.Create(veiculo);
        }

       [HttpGet]
        public Task<List<Veiculo>> GetAll()
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
                return NotFound(e.Message);
            }
        }        

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] VeiculoDto updatedVeiculo)
        {
            try
            {
                var result = await _service.Update(id, updatedVeiculo);
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

        [HttpGet("byKilometersOrSystemVersion")]
        public async Task<IActionResult> GetVeiculosByKilomers(int km, string system)
        {
            try
            {
                var result = await _service.GetVeiculosByKilomers(km, system);
                return Ok(result);
            }
            catch (EntityException e)
            {
                return BadRequest(e.Message);
            }
           
        }
    }
}