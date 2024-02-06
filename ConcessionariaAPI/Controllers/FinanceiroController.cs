using ConcessionariaAPI.Exceptions;
using ConcessionariaAPI.Models;
using ConcessionariaAPI.Models.dtos;
using ConcessionariaAPI.Services;
using ConcessionariaAPI.Services.interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ConcessionariaAPI.Relatorio;

namespace ConcessionariaAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FinanceiroController : ControllerBase
    {
        private IFinanceiroService _service;

        public FinanceiroController()
        {
            _service = new FinanceiroService(new ConcessionariaContext());
        }

        [HttpGet("Balanco")]
        public async Task<IActionResult> GetBalancoFinanceiro()
        {            
            try
            {
                var result = await _service.GetBalancoFinanceiro();
                if(result.Count > 0){
                    RelatorioPDF.gerarRelatorioPDF(result);
                }                    
                return Ok(result);
            }
            catch (EntityException e)
            {                
                return NotFound(e.Message);
            }
        }
       
    }
}