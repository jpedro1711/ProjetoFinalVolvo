using ConcessionariaAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConcessionariaAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VendedorController : ControllerBase
    {
        [HttpPost]
        public void Create([FromBody] Vendedor vendedor)
        {           
            using (var _context = new ConcessionariaContext())
            {
                ICollection<Telefone> telefones = new List<Telefone>();
                foreach (var telefone in vendedor.Telefones)
                {                    
                    if (telefone.TelefoneId != null)
                    {
                        var result = _context.Telefone.FirstOrDefault(t => t.TelefoneId == telefone.TelefoneId);

                        if (result != null)
                        {
                            telefones.Add(result);
                        }
                        
                    }
                    else
                    {
                        _context.Telefone.Add(telefone);
                        telefones.Add(telefone);
                    }


                }
                vendedor.Telefones = telefones;

                ICollection<Endereco> enderecos = new List<Endereco>();
                foreach (var endereco in vendedor.Enderecos)
                {                    
                    if (endereco.EnderecoId != null)
                    {
                        var result = _context.Endereco.FirstOrDefault(e => e.EnderecoId == endereco.EnderecoId);

                        if (result != null)
                        {
                            enderecos.Add(result);
                        }                        
                    }
                    else
                    {
                        _context.Endereco.Add(endereco);
                        enderecos.Add(endereco);
                    }

                }
                vendedor.Enderecos = enderecos;
                _context.Vendedor.Add(vendedor);
                _context.SaveChanges();
            }
        }

        [HttpGet]
        public List<Vendedor> GetAll()
        {
            using (var _context = new ConcessionariaContext())
            {
                return _context.Vendedor.Include("Enderecos").Include("Telefones").ToList();
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            using (var _context = new ConcessionariaContext())
            {
                var result = _context.Vendedor.Include("Enderecos").Include("Telefones").FirstOrDefault(p => p.VendedorId == id);

                if (result != null)
                {
                    return Ok(result);
                }
            }
            return NotFound();
        }

        [HttpGet("Salario/{id}/{mes}/{ano}")]
        public IActionResult GetSalario(int id, int mes, int ano)
        {
            using (var _context = new ConcessionariaContext())
            {
                var resultado = _context.Venda
                    .Join(_context.Vendedor,
                        venda => venda.VendedorId,
                        vendedor => vendedor.VendedorId,
                        (venda, vendedor) => new { Venda = venda, Vendedor = vendedor })
                    .Join(_context.Veiculo,
                        venda => venda.Venda.VeiculoId,
                        veiculo => veiculo.VeiculoId,
                        (venda, veiculo) => new { Venda = venda.Venda, Vendedor = venda.Vendedor, Veiculo = veiculo })
                    .Where(x => x.Venda.DataVenda.Month == mes && x.Venda.DataVenda.Year == ano)
                    .GroupBy(result => new
                    {
                        result.Vendedor.VendedorId,
                        result.Vendedor.Nome,
                        result.Vendedor.SalarioBase,
                        Mes = result.Venda.DataVenda.Month,
                        Ano = result.Venda.DataVenda.Year
                    })
                    .Select(groupedResult => new
                    {
                        ID = groupedResult.Key.VendedorId,
                        Nome = groupedResult.Key.Nome,
                        Salário = (double)groupedResult.Key.SalarioBase + ((double)groupedResult.Sum(v => v.Veiculo.Valor) * 0.01),
                        Mês = groupedResult.Key.Mes,
                        Ano = groupedResult.Key.Ano
                    })
                    .ToList();

                if (resultado != null)
                {
                    return Ok(resultado);
                }
            }
            return NotFound();
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Vendedor updatedVendedor)
        {
            using (var _context = new ConcessionariaContext())
            {
                var existingVendedor = _context.Vendedor.Include("Enderecos").Include("Telefones").FirstOrDefault(p => p.VendedorId == id);

                if (existingVendedor != null)
                {
                    existingVendedor.Nome = updatedVendedor.Nome;
                    existingVendedor.Email = updatedVendedor.Email;
                    existingVendedor.SalarioBase = updatedVendedor.SalarioBase;
                    existingVendedor.DataNascimento = updatedVendedor.DataNascimento;
                    existingVendedor.DataAdmissao = updatedVendedor.DataAdmissao;

                    ICollection<Telefone> telefones = new List<Telefone>();
                    foreach (var telefone in updatedVendedor.Telefones)
                    {
                        if (telefone.TelefoneId != null)
                        {
                            var existingTelefone = _context.Telefone.FirstOrDefault(t => t.TelefoneId == telefone.TelefoneId);
                            if (existingTelefone != null)
                            {
                                existingTelefone.Tipo = telefone.Tipo;
                                existingTelefone.NumeroTelefone = telefone.NumeroTelefone;
                                telefones.Add(existingTelefone);
                            }
                        }
                        else
                        {
                            _context.Telefone.Add(telefone);
                            telefones.Add(telefone);
                        }
                    }

                    existingVendedor.Telefones = telefones;

                    ICollection<Endereco> enderecos = new List<Endereco>();

                    foreach (var endereco in updatedVendedor.Enderecos)
                    {
                        if (endereco.EnderecoId != null)
                        {
                            var existingEndereco = _context.Endereco.FirstOrDefault(e => e.EnderecoId == endereco.EnderecoId);
                            if (existingEndereco != null)
                            {
                                existingEndereco.Rua = endereco.Rua;
                                existingEndereco.Numero = endereco.Numero;
                                existingEndereco.Bairro = endereco.Bairro;
                                existingEndereco.Cidade = endereco.Cidade;
                                enderecos.Add(existingEndereco);
                            }
                        }
                        else
                        {
                            _context.Endereco.Add(endereco);
                            enderecos.Add(endereco);
                        }
                    }

                    existingVendedor.Enderecos = enderecos; 
                    
                    _context.SaveChanges();
                    return Ok(existingVendedor);
                }
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            using (var _context = new ConcessionariaContext())
            {
                var vendedorToDelete = _context.Vendedor.FirstOrDefault(p => p.VendedorId == id);

                if (vendedorToDelete != null)
                {
                    _context.Vendedor.Remove(vendedorToDelete);
                    _context.SaveChanges();
                    return Ok();
                }
                return NotFound();
            }
        }
    }
}
