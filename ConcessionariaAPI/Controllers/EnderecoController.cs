using ConcessionariaAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConcessionariaAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EnderecoController : ControllerBase
    {
        [HttpPost]
        public void Create([FromBody] Endereco endereco)
        {
            using (var _context = new ConcessionariaContext())
            {
                ICollection<Proprietario> proprietarios = new List<Proprietario>();
                foreach (var proprietario in endereco.Proprietarios)
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

                endereco.Proprietarios = proprietarios;

                ICollection<Vendedor> vendedores = new List<Vendedor>();
                foreach (var vendedor in endereco.Vendedores)
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

                endereco.Vendedores = vendedores;

                _context.Endereco.Add(endereco);
                _context.SaveChanges();
            }
        }

        [HttpGet]
        public List<Endereco> GetAll()
        {
            using (var _context = new ConcessionariaContext())
            {
                return _context.Endereco.Include("Proprietarios").Include("Vendedores").ToList();
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            using (var _context = new ConcessionariaContext())
            {
                var result = _context.Endereco.Include("Proprietarios").Include("Vendedores").FirstOrDefault(p => p.EnderecoId == id);

                if (result != null)
                {
                    return Ok(result);
                }
            }
            return NotFound();
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Endereco updatedEndereco)
        {
            using (var _context = new ConcessionariaContext())
            {
                var existingEndereco = _context.Endereco.Include("Proprietarios").Include("Vendedores").FirstOrDefault(p => p.EnderecoId == id);

                if (existingEndereco != null)
                {
                    existingEndereco.Rua = updatedEndereco.Rua;
                    existingEndereco.Numero = updatedEndereco.Numero;
                    existingEndereco.Bairro = updatedEndereco.Bairro;
                    existingEndereco.Cidade = updatedEndereco.Cidade;

                    ICollection<Proprietario> proprietarios = new List<Proprietario>();
                    foreach (var proprietario in updatedEndereco.Proprietarios)
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

                    existingEndereco.Proprietarios = proprietarios;

                    ICollection<Vendedor> vendedores = new List<Vendedor>();
                    foreach (var vendedor in updatedEndereco.Vendedores)
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

                    existingEndereco.Vendedores = vendedores;

                    _context.SaveChanges();
                    return Ok(existingEndereco);
                }
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            using (var _context = new ConcessionariaContext())
            {
                var enderecoToDelete = _context.Endereco.FirstOrDefault(p => p.EnderecoId == id);

                if (enderecoToDelete != null)
                {
                    _context.Endereco.Remove(enderecoToDelete);
                    _context.SaveChanges();
                    return Ok();
                }
                return NotFound();
            }
        }
    }
}