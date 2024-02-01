using ConcessionariaAPI.Models;
using ConcessionariaAPI.Repositories;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using ConcessionariaAPI.Exceptions;

namespace ConcessionariaAPI.Services
{
    public class ProprietarioService : IService<Proprietario>
    {
        private IRepository<Proprietario> _repository;
        private IRepository<Telefone> _telefoneRepository;
        private IRepository<Endereco> _enderecoRepository;

        public ProprietarioService(ConcessionariaContext context)
        {
            _repository = new ProprietarioRepository(context);
            _telefoneRepository = new TelefoneRepository(context);
            _enderecoRepository = new EnderecoRepository(context);
        }


        public async Task<Proprietario> Create(Proprietario proprietario)
        {
            List<Telefone> telefones = new List<Telefone>();
            foreach (var telefone in proprietario.Telefones)
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
            
            proprietario.Telefones = telefones;
            

            List<Endereco> enderecos = new List<Endereco>();
            foreach (var endereco in proprietario.Enderecos)
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
            proprietario.Enderecos = enderecos;

            var created = await _repository.Create(proprietario);

            return created;
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }

        public async Task<List<Proprietario>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<Proprietario> GetById(int id)
        {
            return await _repository.GetById(id);
        }

        public async Task<Proprietario> Update(int id, Proprietario updatedProprietario)
        {
            Proprietario existingProprietario = await _repository.GetById(id);

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

                existingProprietario.Telefones = telefones;

                ICollection<Endereco> enderecos = new List<Endereco>();

                foreach (var endereco in updatedProprietario.Enderecos)
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

                existingProprietario.Enderecos = enderecos;

                var updated = await _repository.Update(id, existingProprietario);
                return updated;
            }
            throw new EntityException("Erro ao atualizar proprietário com id " + id + ", não foi encontrado ou dados inválidos");
            

            

        }
    }
}
