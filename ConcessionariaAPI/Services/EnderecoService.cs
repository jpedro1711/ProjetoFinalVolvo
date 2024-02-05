using ConcessionariaAPI.Models;
using ConcessionariaAPI.Repositories;
using Microsoft.EntityFrameworkCore;
using ConcessionariaAPI.Exceptions;
using ConcessionariaAPI.Services.interfaces;
using ConcessionariaAPI.Models.dtos;
using ConcessionariaAPI.Repositories.interfaces;

namespace ConcessionariaAPI.Services
{
    public class EnderecoService : IEnderecoService
    {
        private IRepository<Endereco> _repository;
        private IRepository<Proprietario> _proprietarioRepository;
        private IRepository<Vendedor> _vendedoresRepository;

        public EnderecoService(ConcessionariaContext context)
        {
            _repository = new EnderecoRepository(context);
            _proprietarioRepository = new ProprietarioRepository(context);
        }

        public async Task<Endereco> Create(EnderecoDto endereco)
        { 
            var enderecoCreated = await _repository.Create(endereco.ToEntity());
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

        public async Task<Endereco> Update(int id, EnderecoDto updatedEndereco)
        {
            var existingEndereco = await _repository.GetById(id);

            if (existingEndereco != null)
            {          
                var updated = await _repository.Update(id, updatedEndereco.ToEntity());
                return updated;
            }
            throw new EntityException("Endereço não encontrado", 404, "UPDATE, EnderecoService");
        }
    }
}
