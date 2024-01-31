using ConcessionariaAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConcessionariaAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProprietarioController : ControllerBase
    {
        [HttpPost]
        public void Create([FromBody] Proprietario proprietario)
        {
            /*
             *  Aqui a ideia é a seguinte 
             *  Temos uma lista de enderecos e telefones que recebemos no json
             *  Vamos percorrer as lista e verificar se os endereços e telefones existem por meio de um FindBYId
             *  Se existir, vamos adicionar a uma lista auxiliar
             *  Senão, criamos no banco um nova endereço ou telefone e adicionamos a lista
             *  Depois, definimos proprietario.Telefones e proprietario.Enderecos para serem essas lista com os endereços encontrados ou criados
             */
            /*
             * Exemplo de JSON para criar Proprietario
             * {
                  "nome": "PropNome",
                  "email": "Email",
                  "cpf": "CPF",
                  "dataNascimento": "2024-01-31T10:20:22.763Z",
                  "enderecos": [
                    {
                      "rua": "Teste",
                      "numero": 51,
                      "bairro": "Teste",
                      "cidade": "Teste"
                    }
                  ],
                  "telefones": [
                    {
                      "tipo": "R",
                      "numeroTelefone": "1234"
                    }
                  ]
                }
             */
            using (var _context = new ConcessionariaContext())
            {
                ICollection<Telefone> telefones = new List<Telefone>();
                foreach (var telefone in proprietario.Telefones)
                {
                    // Telefone existente -> buscar e adicionar ao proprietário
                    if (telefone.TelefoneId != null)
                    {
                        var result = _context.Telefone.FirstOrDefault(t => t.TelefoneId == telefone.TelefoneId);

                        if (result != null)
                        {
                            telefones.Add(result);
                        }
                    }
                    // Telefone não existe -> então criar no contexto (salvar no banco) e adicionar ao proprietário
                    else
                    {
                        _context.Telefone.Add(telefone);
                        telefones.Add(telefone);
                    }

                }
                proprietario.Telefones = telefones;

                ICollection<Endereco> enderecos = new List<Endereco>();
                foreach (var endereco in proprietario.Enderecos)
                {

                    // Endereço existente -> buscar e adicionar ao proprietário
                    if (endereco.EnderecoId != null)
                    {
                        var result = _context.Endereco.FirstOrDefault(e => e.EnderecoId == endereco.EnderecoId);

                        if (result != null)
                        {
                            enderecos.Add(result);
                        }
                    }
                    // Endereço não existe -> então criar no contexto (salvar no banco) e adicionar ao proprietário
                    else
                    {
                        _context.Endereco.Add(endereco);
                        enderecos.Add(endereco);
                    }

                }
                proprietario.Enderecos = enderecos;
                _context.Proprietario.Add(proprietario);
                _context.SaveChanges();
            }
        }

        [HttpGet]
        public List<Proprietario> GetAll()
        {
            using (var _context = new ConcessionariaContext())
            {
                return _context.Proprietario.Include("Enderecos").Include("Telefones").ToList();
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            using (var _context = new ConcessionariaContext())
            {
                var result =  _context.Proprietario.Include("Enderecos").Include("Telefones").FirstOrDefault(p => p.ProprietarioId == id);
               
                if (result != null)
                {
                    return Ok(result);
                }
            }
            return NotFound();
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Proprietario updatedProprietario)
        {
            using (var _context = new ConcessionariaContext())
            {
                var existingProprietario = _context.Proprietario.Include("Enderecos").Include("Telefones").FirstOrDefault(p => p.ProprietarioId == id);

                if (existingProprietario != null)
                {
                    existingProprietario.Nome = updatedProprietario.Nome;
                    existingProprietario.Email = updatedProprietario.Email;
                    existingProprietario.CPF = updatedProprietario.CPF;
                    existingProprietario.DataNascimento = updatedProprietario.DataNascimento;

                    ICollection<Telefone> telefones = new List<Telefone>();
                    foreach (var telefone in updatedProprietario.Telefones)
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

                    existingProprietario.Telefones = telefones;

                    ICollection<Endereco> enderecos = new List<Endereco>();

                    foreach (var endereco in updatedProprietario.Enderecos)
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

                    existingProprietario.Enderecos = enderecos;
                    existingProprietario.Telefones = telefones;
                    _context.SaveChanges();
                    return Ok(existingProprietario);
                }
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            using (var _context = new ConcessionariaContext())
            {
                var proprietarioToDelete = _context.Proprietario.FirstOrDefault(p => p.ProprietarioId == id);
        
                if (proprietarioToDelete != null)
                {
                    _context.Proprietario.Remove(proprietarioToDelete);
                    _context.SaveChanges();
                    return Ok();
                }
                return NotFound();
            }
        }
    }
}
