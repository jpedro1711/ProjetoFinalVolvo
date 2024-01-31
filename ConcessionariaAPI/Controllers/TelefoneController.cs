using ConcessionariaAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConcessionariaAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TelefoneController : ControllerBase
    {
        [HttpPost]
        public void Create([FromBody] Telefone telefone)
        {
            using (var _context = new ConcessionariaContext())
            {
                ICollection<Proprietario> proprietarios = new List<Proprietario>();
                foreach (var proprietario in telefone.Proprietarios)
                {
                    if (proprietario.ProprietarioId != null)
                    {
                        var result = _context.Proprietario.FirstOrDefault(t => t.ProprietarioId == proprietario.ProprietarioId);

                        if (result != null)
                        {
                            proprietarios.Add(result);
                        }
                    }
                    else
                    {
                        _context.Proprietario.Add(proprietario);
                        proprietarios.Add(proprietario);
                    }
                }

                telefone.Proprietarios = proprietarios;

                ICollection<Vendedor> vendedores = new List<Vendedor>();
                foreach (var vendedor in telefone.Vendedores)
                {
                    if (vendedor.VendedorId != null)
                    {
                        var result = _context.Vendedor.FirstOrDefault(t => t.VendedorId == vendedor.VendedorId);

                        if (result != null)
                        {
                            vendedores.Add(result);
                        }
                    }
                    else
                    {
                        _context.Vendedor.Add(vendedor);
                        vendedores.Add(vendedor);
                    }
                }

                telefone.Vendedores = vendedores;

                _context.Telefone.Add(telefone);
                _context.SaveChanges();
            }
        }

        [HttpGet]
        public List<Telefone> GetAll()
        {
            using (var _context = new ConcessionariaContext())
            {
                return _context.Telefone.Include("Proprietarios").Include("Vendedores").ToList();
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            using (var _context = new ConcessionariaContext())
            {
                var result = _context.Telefone.Include("Proprietarios").Include("Vendedores").FirstOrDefault(p => p.TelefoneId == id);

                if (result != null)
                {
                    return Ok(result);
                }
            }
            return NotFound();
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Telefone updatedTelefone)
        {
            using (var _context = new ConcessionariaContext())
            {
                var existingTelefone = _context.Telefone.Include("Proprietarios").Include("Vendedores").FirstOrDefault(p => p.TelefoneId == id);

                if (existingTelefone != null)
                {
                    existingTelefone.Tipo = updatedTelefone.Tipo;
                    existingTelefone.NumeroTelefone = updatedTelefone.NumeroTelefone;                    

                    ICollection<Proprietario> proprietarios = new List<Proprietario>();
                    foreach (var proprietario in updatedTelefone.Proprietarios)
                    {
                        if (proprietario.ProprietarioId != null)
                        {
                            var existingProprietario = _context.Proprietario.FirstOrDefault(t => t.ProprietarioId == proprietario.ProprietarioId);
                            if (existingProprietario != null)
                            {
                                existingProprietario.Nome = proprietario.Nome;
                                existingProprietario.Email = proprietario.Email;
                                existingProprietario.CPF = proprietario.CPF;
                                existingProprietario.CNPJ = proprietario.CNPJ;
                                existingProprietario.DataNascimento = proprietario.DataNascimento;
                                proprietarios.Add(existingProprietario);
                            }
                        }
                        else
                        {
                            _context.Proprietario.Add(proprietario);
                            proprietarios.Add(proprietario);
                        }
                    }

                    existingTelefone.Proprietarios = proprietarios;

                    ICollection<Vendedor> vendedores = new List<Vendedor>();
                    foreach (var vendedor in updatedTelefone.Vendedores)
                    {
                        if (vendedor.VendedorId != null)
                        {
                            var existingVendedor = _context.Vendedor.FirstOrDefault(t => t.VendedorId == vendedor.VendedorId);
                            if (existingVendedor != null)
                            {
                                existingVendedor.Nome = vendedor.Nome;
                                existingVendedor.Email = vendedor.Email;
                                existingVendedor.SalarioBase = vendedor.SalarioBase;
                                existingVendedor.DataNascimento = vendedor.DataNascimento;
                                existingVendedor.DataAdmissao = vendedor.DataAdmissao;
                                vendedores.Add(existingVendedor);
                            }
                        }
                        else
                        {
                            _context.Vendedor.Add(vendedor);
                            vendedores.Add(vendedor);
                        }
                    }

                    existingTelefone.Vendedores = vendedores;

                    _context.SaveChanges();
                    return Ok(existingTelefone);
                }
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            using (var _context = new ConcessionariaContext())
            {
                var telefoneToDelete = _context.Telefone.FirstOrDefault(p => p.TelefoneId == id);

                if (telefoneToDelete != null)
                {
                    _context.Telefone.Remove(telefoneToDelete);
                    _context.SaveChanges();
                    return Ok();
                }
                return NotFound();
            }
        }
    }
}