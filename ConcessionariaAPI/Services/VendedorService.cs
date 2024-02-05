using ConcessionariaAPI.Models;
using ConcessionariaAPI.Repositories;
using ConcessionariaAPI.Repositories.Dto;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using ConcessionariaAPI.Exceptions;
using ConcessionariaAPI.Services.interfaces;
using ConcessionariaAPI.Repositories.interfaces;

namespace ConcessionariaAPI.Services
{
    public class VendedorService : IVendedorService<Vendedor>
    {
        private IVendedorRepository<Vendedor> _repository;
        private IRepository<Telefone> _telefoneRepository;
        private IRepository<Endereco> _enderecoRepository;

        public VendedorService(ConcessionariaContext context)
        {
            _repository = new VendedorRepository(context);
            _telefoneRepository = new TelefoneRepository(context);
            _enderecoRepository = new EnderecoRepository(context);
        }


        public async Task<Vendedor> Create(Vendedor vendedor)
        {
            List<Telefone> telefones = new List<Telefone>();
            foreach (var telefone in vendedor.Telefones)
            {
                if (telefone.TelefoneId != null)
                {
                    Telefone result = await _telefoneRepository.GetById((int)telefone.TelefoneId);

                    if (result != null)
                    {
                        telefones.Add(result);
                    }
                }
                else
                {
                    Telefone result = await _telefoneRepository.Create(telefone);
                    telefones.Add(result);
                }

            }
            
            vendedor.Telefones = telefones;
            

            List<Endereco> enderecos = new List<Endereco>();
            foreach (var endereco in vendedor.Enderecos)
            {
                if (endereco.EnderecoId != null)
                {
                    Endereco result = await _enderecoRepository.GetById((int)endereco.EnderecoId);

                    if (result != null)
                    {
                        enderecos.Add(result);
                    }
                }
                else
                {
                    Endereco result = await _enderecoRepository.Create(endereco);
                    enderecos.Add(result);
                }

            }
            vendedor.Enderecos = enderecos;

            var created = await _repository.Create(vendedor);

            return created;
        }

        public async Task Delete(int id)
        {
            await _repository.Delete(id);
        }

        public async Task<List<Vendedor>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<Vendedor> GetById(int id)
        {
            return await _repository.GetById(id);
        }

        public async Task<Vendedor> Update(int id, Vendedor updatedVendedor)
        {
            Vendedor existingVendedor = await _repository.GetById(id);

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
                        Telefone existingTelefone = await _telefoneRepository.GetById((int)telefone.TelefoneId);
                        if (existingTelefone != null)
                        {
                            existingTelefone.Tipo = telefone.Tipo;
                            existingTelefone.NumeroTelefone = telefone.NumeroTelefone;
                            telefones.Add(existingTelefone);
                        }
                    }
                    else
                    {
                        Telefone result = await _telefoneRepository.Create(telefone);
                        telefones.Add(result);
                    }
                }

                existingVendedor.Telefones = telefones;

                ICollection<Endereco> enderecos = new List<Endereco>();

                foreach (var endereco in updatedVendedor.Enderecos)
                {
                    if (endereco.EnderecoId != null)
                    {
                        var existingEndereco = await _enderecoRepository.GetById((int)endereco.EnderecoId);
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
                        Endereco result = await _enderecoRepository.Create(endereco);
                        enderecos.Add(result);
                    }
                }

                existingVendedor.Enderecos = enderecos;

                var updated = await _repository.Update(id, existingVendedor);
                return updated;
            }
            throw new EntityException("Erro ao atualizar vendedor com id " + id + ", não foi encontrado ou dados inválidos");                     
        }
        
        public async Task<List<Salario>> GetSalarioMesAno(int id, int mes, int ano)
        {
            return await _repository.GetSalarioMesAno( id, mes, ano);
        }

        public async Task<List<List<Salario>>> GetSalarioVendedores()
        {
            return await _repository.GetSalarioVendedores();
        }
    }  

}
