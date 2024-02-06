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
            if(despesa.DespesaID != null){
                throw new EntityException("ID não deve ser informado!");
            }

            if(despesa.Descricao.Length == 0 || despesa.Descricao == ""){
                throw new EntityException("A descrição da despesa deve ser informada!");
            }

            if(despesa.Valor <= 0){
                throw new EntityException("O valor da despesa deve ser informado e deve ser superior a 0!");
            }

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
             if(id != updatedDespesa.DespesaID){
                throw new EntityException("IDs informados não coincidem!");
            }

            if(updatedDespesa.Descricao.Length == 0 || updatedDespesa.Descricao == ""){
                throw new EntityException("A descrição da despesa atualizada deve ser informada!");
            }

            if(updatedDespesa.Valor <= 0){
                throw new EntityException("O valor da despesa atualizada deve ser informado e deve ser superior a 0!");
            }

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
