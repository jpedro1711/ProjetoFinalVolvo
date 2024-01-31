using ConcessionariaAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConcessionariaAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VeiculoController : ControllerBase
    {
        [HttpPost]
        public void Create([FromBody] Veiculo veiculo)
        {
            using (var _context = new ConcessionariaContext())
            {
                ICollection<Acessorio> acessorios = new List<Acessorio>();
                foreach (var acessorio in veiculo.Acessorios)
                {
                    if (acessorio.AcessorioID != null)
                    {
                        var result = _context.Acessorio.FirstOrDefault(t => t.AcessorioID == acessorio.AcessorioID);

                        if (result != null)
                        {
                            acessorios.Add(result);
                        }
                    }
                    else
                    {
                        _context.Acessorio.Add(acessorio);
                        acessorios.Add(acessorio);
                    }
                }

                veiculo.Acessorios = acessorios;

                _context.Veiculo.Add(veiculo);
                _context.SaveChanges();
            }
        }

        [HttpGet]
        public List<Veiculo> GetAll()
        {
            using (var _context = new ConcessionariaContext())
            {
                return _context.Veiculo.Include("Acessorios").ToList();
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            using (var _context = new ConcessionariaContext())
            {
                var result = _context.Veiculo.Include("Acessorios").FirstOrDefault(p => p.VeiculoId == id);

                if (result != null)
                {
                    return Ok(result);
                }
            }
            return NotFound();
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Veiculo updatedVeiculo)
        {
            using (var _context = new ConcessionariaContext())
            {
                var existingVeiculo = _context.Veiculo.Include("Acessorios").FirstOrDefault(p => p.VeiculoId == id);

                if (existingVeiculo != null)
                {
                    existingVeiculo.NumeroChassi = updatedVeiculo.NumeroChassi;
                    existingVeiculo.Valor = updatedVeiculo.Valor;
                    existingVeiculo.Quilometragem = updatedVeiculo.Quilometragem;
                    existingVeiculo.VersaoSistema = updatedVeiculo.VersaoSistema;
                    existingVeiculo.ProprietarioId = updatedVeiculo.ProprietarioId;
                    existingVeiculo.Proprietario = updatedVeiculo.Proprietario;

                    ICollection<Acessorio> acessorios = new List<Acessorio>();
                    foreach (var acessorio in updatedVeiculo.Acessorios)
                    {
                        if (acessorio.AcessorioID != null)
                        {
                            var existingAcessorio = _context.Acessorio.FirstOrDefault(t => t.AcessorioID == acessorio.AcessorioID);
                            if (existingAcessorio != null)
                            {
                                existingAcessorio.Descricao = acessorio.Descricao;
                                existingAcessorio.Veiculos.Add(updatedVeiculo);
                                acessorios.Add(existingAcessorio);
                            }
                        }
                        else
                        {
                            _context.Acessorio.Add(acessorio);
                            acessorios.Add(acessorio);
                        }
                    }

                    existingVeiculo.Acessorios = acessorios;                
                    
                    _context.SaveChanges();
                    return Ok(existingVeiculo);
                }
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            using (var _context = new ConcessionariaContext())
            {
                var veiculoToDelete = _context.Veiculo.FirstOrDefault(p => p.VeiculoId == id);

                if (veiculoToDelete != null)
                {
                    _context.Veiculo.Remove(veiculoToDelete);
                    _context.SaveChanges();
                    return Ok();
                }
                return NotFound();
            }
        }
    }
}