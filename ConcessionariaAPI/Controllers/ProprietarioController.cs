﻿using ConcessionariaAPI.Exceptions;
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
    public class ProprietarioController : ControllerBase
    {
        private IProprietarioService _service;

        public ProprietarioController()
        {
            _service = new ProprietarioService(new ConcessionariaContext());
        }

        [HttpPost]
        public async Task<Proprietario> Create([FromBody] ProprietarioDto proprietario)
        {
            return await _service.Create(proprietario);
        }

        [HttpGet]
        public Task<List<Proprietario>> GetAll()
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
        public async Task<IActionResult> Update(int id, [FromBody] ProprietarioDto updatedProprietario)
        {
            try
            {
                var result = await _service.Update(id, updatedProprietario);
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
