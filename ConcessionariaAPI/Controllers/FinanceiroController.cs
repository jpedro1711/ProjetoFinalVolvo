using ConcessionariaAPI.Exceptions;
using ConcessionariaAPI.Models;
using ConcessionariaAPI.Repositories.Dto;
using ConcessionariaAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConcessionariaAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FinanceiroController : ControllerBase
    {
        private IFinanceiroService<BalancoFinanceiro> _service;

        public FinanceiroController()
        {
            _service = new FinanceiroService(new ConcessionariaContext());
        }

        [HttpGet("Financeiro/Balanco")]
        public async Task<IActionResult> GetBalancoFinanceiro()
        {            
            try
            {
                var result = await _service.GetBalancoFinanceiro();
                return Ok(result);
            }
            catch (EntityException e)
            {
                return NotFound();
            }
        }
       
    }
}