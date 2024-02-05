using ConcessionariaAPI.Models;
using ConcessionariaAPI.Repositories;
using Microsoft.EntityFrameworkCore;
using ConcessionariaAPI.Exceptions;
using ConcessionariaAPI.Services.interfaces;
using ConcessionariaAPI.Repositories.interfaces;
using ConcessionariaAPI.Models.dtos;

namespace ConcessionariaAPI.Services
{
    public class TelefoneService : ITelefoneService
    {
        private IRepository<Telefone> _repository;

        public TelefoneService(ConcessionariaContext context)
        {
            _repository = new TelefoneRepository(context);
        }
        public async Task<Telefone> Create(TelefoneDto telefone)
        {
            var created = await _repository.Create(telefone.ToEntity());
            return created;
        }

        public async Task Delete(int id)
        {
            await _repository.Delete(id);
        }

        public async Task<List<Telefone>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<Telefone> GetById(int id)
        {
            return await _repository.GetById(id);
        }

        public async Task<Telefone> Update(int id, TelefoneDto telefoneDto)
        {
            var existingTelefone = await _repository.GetById(id);

            if (existingTelefone == null)
            {
                throw new EntityException("Telefone não encontrado", 404, "UPDATE, TelefoneService");
            }
            var updated = await _repository.Update(id, telefoneDto.ToEntity());
            return updated;

        }
    }
}
