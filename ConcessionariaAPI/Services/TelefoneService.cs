using ConcessionariaAPI.Models;
using ConcessionariaAPI.Repositories;
using Microsoft.EntityFrameworkCore;
using ConcessionariaAPI.Exceptions;

namespace ConcessionariaAPI.Services
{
    public class TelefoneService : IService<Telefone>
    {
        private IRepository<Telefone> _repository;
        private IRepository<Proprietario> _proprietarioRepository;
        private IVendedorRepository<Vendedor> _vendedorRepository;
        public TelefoneService(ConcessionariaContext context)
        {
            _repository = new TelefoneRepository(context);
            _proprietarioRepository = new ProprietarioRepository(context);
            _vendedorRepository = new VendedorRepository(context);
        }
        public async Task<Telefone> Create(Telefone telefone)
        {
            ICollection<Proprietario> proprietarios = new List<Proprietario>();
            foreach (var proprietario in telefone.Proprietarios)
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
                    var propCreatedf = await _proprietarioRepository.Create(proprietario);
                    proprietarios.Add(proprietario);
                }
            }

            telefone.Proprietarios = proprietarios;

            ICollection<Vendedor> vendedores = new List<Vendedor>();
            foreach (var vendedor in telefone.Vendedores)
            {
                if (vendedor.VendedorId != null)
                {
                    var result = await _vendedorRepository.GetById((int)vendedor.VendedorId);

                    if (result != null)
                    {
                        vendedores.Add(result);
                    }
                }
                else
                {
                    await _vendedorRepository.Create(vendedor);
                    vendedores.Add(vendedor);
                }
            }
            telefone.Vendedores = vendedores;

            var created = await _repository.Create(telefone);
            return created;
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }

        public async Task<List<Telefone>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<Telefone> GetById(int id)
        {
            return await _repository.GetById(id);
        }

        public async Task<Telefone> Update(int id, Telefone updatedTelefone)
        {
            var existingTelefone = await _repository.GetById(id);

            if (existingTelefone != null)
            {
                existingTelefone.Tipo = updatedTelefone.Tipo;
                existingTelefone.NumeroTelefone = updatedTelefone.NumeroTelefone;

                ICollection<Proprietario> proprietarios = new List<Proprietario>();
                foreach (var proprietario in updatedTelefone.Proprietarios)
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
                        await _proprietarioRepository.Create(proprietario);
                        proprietarios.Add(proprietario);
                    }
                }

                existingTelefone.Proprietarios = proprietarios;

                ICollection<Vendedor> vendedores = new List<Vendedor>();
                foreach (var vendedor in updatedTelefone.Vendedores)
                {
                    if (vendedor.VendedorId != null)
                    {
                        var existingVendedor = await _vendedorRepository.GetById((int)vendedor.VendedorId);
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
                        await _vendedorRepository.Create(vendedor);
                        vendedores.Add(vendedor);
                    }
                }

                existingTelefone.Vendedores = vendedores;

                var created = await _repository.Update(id, existingTelefone);
                return created;
            }
            throw new EntityException("Telefone não encontrado", 404, "UPDATE, TelefoneService");
        }
    }
}
