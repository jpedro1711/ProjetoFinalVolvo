using ConcessionariaAPI.Models;
using ConcessionariaAPI.Repositories;
using Microsoft.EntityFrameworkCore;
using ConcessionariaAPI.Exceptions;

namespace ConcessionariaAPI.Services
{
    public class EnderecoService : IService<Endereco>
    {
        private IRepository<Endereco> _repository;
        private IRepository<Proprietario> _proprietarioRepository;
        private IRepository<Vendedor> _vendedoresRepository;

        public EnderecoService(ConcessionariaContext context)
        {
            _repository = new EnderecoRepository(context);
            _proprietarioRepository = new ProprietarioRepository(context);
        }

        public async Task<Endereco> Create(Endereco endereco)
        {
            ICollection<Proprietario> proprietarios = new List<Proprietario>();
            foreach (var proprietario in endereco.Proprietarios)
            {
                if (proprietario.ProprietarioId != null)
                {
                    var result = await _proprietarioRepository.GetById((int)proprietario.ProprietarioId);

                    if (result != null)
                    {
                        proprietarios.Add(result);
                    }
                }
                else
                {
                    var created = await _proprietarioRepository.Create(proprietario);
                    proprietarios.Add(created);
                }
            }

            endereco.Proprietarios = proprietarios;

            ICollection<Vendedor> vendedores = new List<Vendedor>();
            foreach (var vendedor in endereco.Vendedores)
            {
                if (vendedor.VendedorId != null)
                {
                    var result = await _vendedoresRepository.GetById((int)vendedor.VendedorId);

                    if (result != null)
                    {
                        vendedores.Add(result);
                    }
                }
                else
                {
                    var created = await _vendedoresRepository.Create(vendedor);
                    vendedores.Add(vendedor);
                }
            }
            endereco.Vendedores = vendedores;

            var enderecoCreated = await _repository.Create(endereco);
            return enderecoCreated;
        }

        public async Task Delete(int id)
        {
            await _repository.Delete(id);
        }

        public async Task<List<Endereco>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<Endereco> GetById(int id)
        {
            return await _repository.GetById(id);
        }

        public async Task<Endereco> Update(int id, Endereco updatedEndereco)
        {
            var existingEndereco = await _repository.GetById(id);

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
                        var existingProprietario = await _proprietarioRepository.GetById((int)proprietario.ProprietarioId);
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
                        var created = await _proprietarioRepository.Create(proprietario);
                        proprietarios.Add(proprietario);
                    }
                }

                existingEndereco.Proprietarios = proprietarios;

                ICollection<Vendedor> vendedores = new List<Vendedor>();
                foreach (var vendedor in updatedEndereco.Vendedores)
                {
                    if (vendedor.VendedorId != null)
                    {
                        var existingVendedor = await _vendedoresRepository.GetById((int)vendedor.VendedorId);
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
                        var vendedorCriado = await _vendedoresRepository.Create(vendedor);
                        vendedores.Add(vendedor);
                    }
                }

                existingEndereco.Vendedores = vendedores;
                var updated = await _repository.Update(id, existingEndereco);
                return updated;
            }
            throw new EntityException("Proprietário não encontrado", 404, "UPDATE, EnderecoService");
        }
    }
}
