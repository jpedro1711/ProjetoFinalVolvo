using ConcessionariaAPI.Models;
using ConcessionariaAPI.Repositories;
using ConcessionariaAPI.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using ConcessionariaAPI.Models.dtos;
using ConcessionariaAPI.Services.interfaces;
using ConcessionariaAPI.Repositories.interfaces;

namespace ConcessionariaAPI.Services
{
    public class DespesaService : IDespesaService
    {
        private IRepository<Despesa> _repository;
        public DespesaService(ConcessionariaContext context) 
        {
            _repository = new DespesaRepository(context);
        }

        public async Task<Despesa> Create(DespesaDto despesa)
        {

            var created = await _repository.Create(despesa.ToEntity());

            return created;
        }

        public async Task Delete(int id)
        {
            await _repository.Delete(id);
        }

        public async Task<List<Despesa>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<Despesa> GetById(int id)
        {
            return await _repository.GetById(id);
        }

        public async Task<Despesa> Update(int id, DespesaDto updatedDespesa)
        {
            var existingDespesa = await _repository.GetById(id);

            if (existingDespesa != null)
            {
                existingDespesa.Descricao = updatedDespesa.Descricao;
                existingDespesa.Valor = updatedDespesa.Valor;

                await _repository.Update(id, existingDespesa);
                return existingDespesa;
            }
            throw new EntityException("Despesa não encontrada com id " + id);
        }
    }
}
