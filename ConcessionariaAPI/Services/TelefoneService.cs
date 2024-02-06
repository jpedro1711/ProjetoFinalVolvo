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
            if(telefone.TelefoneId != null){
                throw new EntityException("ID não deve ser informado!");
            }

            if(telefone.Tipo.Equals("") || telefone.Tipo == null){
                throw new EntityException("Tipo do telefone deve ser informado!");
            }

            if(!telefone.Tipo.Equals('r') && !telefone.Tipo.Equals('c')){
                throw new EntityException("O tipo do telefone deve ser 'r' para residencial ou 'c' para celular");
            }
            
            if((telefone.NumeroTelefone.Length < 12 && telefone.Tipo.Equals('r')) || (telefone.NumeroTelefone.Length < 13 && telefone.Tipo.Equals('c'))){
                throw new EntityException("Telefone residencial deve possuir 12 dígitos e celular 13!");
            }

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
            if(id != telefoneDto.TelefoneId){
                throw new EntityException("IDs informados não coincidem!");
            }

            if(telefoneDto.Tipo.Equals("") || telefoneDto.Tipo == null){
                throw new EntityException("Tipo do telefone deve ser informado!");
            }

            if(!telefoneDto.Tipo.Equals('r') && !telefoneDto.Tipo.Equals('c')){
                throw new EntityException("O tipo do telefone deve ser 'r' para residencial ou 'c' para celular");
            }
            
            if((telefoneDto.NumeroTelefone.Length < 12 && telefoneDto.Tipo.Equals('r')) || (telefoneDto.NumeroTelefone.Length < 13 && telefoneDto.Tipo.Equals('c'))){
                throw new EntityException("Telefone residencial deve possuir 12 dígitos e celular 13!");
            }

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
