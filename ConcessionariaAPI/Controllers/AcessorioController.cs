using ConcessionariaAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConcessionariaAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AcessorioController : ControllerBase
    {
        [HttpPost]
        public void Create([FromBody] Acessorio acessorio)
        {
            using (var _context = new ConcessionariaContext())
            {
                ICollection<Veiculo> veiculos = new List<Veiculo>();
                
                foreach (var veiculo in acessorio.Veiculos)
                {                    
                    if (veiculo.VeiculoId != null)
                    {
                        var result = _context.Veiculo.FirstOrDefault(t => t.VeiculoId == veiculo.VeiculoId);

                        if (result != null)
                        {
                            veiculos.Add(result);
                        }
                    }                 
                    else
                    {
                        _context.Veiculo.Add(veiculo);
                        veiculos.Add(veiculo);
                    }
                }

                acessorio.Veiculos = veiculos;
                
                _context.Acessorio.Add(acessorio);
                _context.SaveChanges();
            }
        }

        [HttpGet]
        public List<Acessorio> GetAll()
        {
            using (var _context = new ConcessionariaContext())
            {
                return _context.Acessorio.Include("Veiculos").ToList();
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            using (var _context = new ConcessionariaContext())
            {
                var result = _context.Acessorio.Include("Veiculos").FirstOrDefault(p => p.AcessorioID == id);

                if (result != null)
                {
                    return Ok(result);
                }
            }
            return NotFound();
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Acessorio updatedAcessorio)
        {
            using (var _context = new ConcessionariaContext())
            {
                var existingAcessorio = _context.Acessorio.Include("Veiculos").FirstOrDefault(p => p.AcessorioID == id);

                if (existingAcessorio != null)
                {
                    existingAcessorio.Descricao = updatedAcessorio.Descricao;

                    ICollection<Veiculo> veiculos = new List<Veiculo>();
                    foreach (var veiculo in updatedAcessorio.Veiculos)
                    {
                        if (veiculo.VeiculoId != null)
                        {
                            var existingVeiculo = _context.Veiculo.FirstOrDefault(t => t.VeiculoId == veiculo.VeiculoId);
                            if (existingVeiculo != null)
                            {
                                existingVeiculo.NumeroChassi = veiculo.NumeroChassi;
                                existingVeiculo.Valor = veiculo.Valor;
                                existingVeiculo.Quilometragem = veiculo.Quilometragem;
                                existingVeiculo.VersaoSistema = veiculo.VersaoSistema;
                                existingVeiculo.ProprietarioId = veiculo.ProprietarioId;
                                existingVeiculo.Proprietario = veiculo.Proprietario;
                                existingVeiculo.Acessorios.Add(updatedAcessorio);
                                veiculos.Add(existingVeiculo);
                            }
                        }
                        else
                        {
                            _context.Veiculo.Add(veiculo);
                            veiculos.Add(veiculo);
                        }
                    }

                    existingAcessorio.Veiculos = veiculos;                
                    
                    _context.SaveChanges();
                    return Ok(existingAcessorio);
                }
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            using (var _context = new ConcessionariaContext())
            {
                var acessorioToDelete = _context.Acessorio.FirstOrDefault(p => p.AcessorioID == id);

                if (acessorioToDelete != null)
                {
                    _context.Acessorio.Remove(acessorioToDelete);
                    _context.SaveChanges();
                    return Ok();
                }
                return NotFound();
            }
        }
    }
}