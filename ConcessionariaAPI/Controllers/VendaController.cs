using ConcessionariaAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConcessionariaAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VendaController : ControllerBase
    {
        [HttpPost]
        public void Create([FromBody] Venda venda)
        {
            using (var _context = new ConcessionariaContext())
            {                                
                _context.Venda.Add(venda);
                _context.SaveChanges();
            }
        }

        [HttpGet]
        public List<Venda> GetAll()
        {
            using (var _context = new ConcessionariaContext())
            {
                return _context.Venda.ToList();
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            using (var _context = new ConcessionariaContext())
            {
                var result = _context.Venda.FirstOrDefault(p => p.VendaId == id);

                if (result != null)
                {
                    return Ok(result);
                }
            }
            return NotFound();
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Venda updatedVenda)
        {
            using (var _context = new ConcessionariaContext())
            {
                var existingVenda = _context.Venda.FirstOrDefault(p => p.VendaId == id);

                if (existingVenda != null)
                {
                    existingVenda.DataVenda = updatedVenda.DataVenda;
                    existingVenda.VeiculoId = updatedVenda.VeiculoId;
                    existingVenda.Veiculo = updatedVenda.Veiculo;
                    existingVenda.VendedorId = updatedVenda.VendedorId;
                    existingVenda.Vendedor = updatedVenda.Vendedor;
                    _context.SaveChanges();
                    return Ok(existingVenda);
                }
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            using (var _context = new ConcessionariaContext())
            {
                var vendaToDelete = _context.Venda.FirstOrDefault(p => p.VendaId == id);

                if (vendaToDelete != null)
                {
                    _context.Venda.Remove(vendaToDelete);
                    _context.SaveChanges();
                    return Ok();
                }
                return NotFound();
            }
        }
    }
}