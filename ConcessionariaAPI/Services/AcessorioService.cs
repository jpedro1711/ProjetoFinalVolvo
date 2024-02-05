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
    public class AcessorioService : IAcessorioService
    {
        private IRepository<Acessorio> _repository;
        private IVeiculoRepository<Veiculo> _veiculoRepository;
        public AcessorioService(ConcessionariaContext context) 
        {
            _repository = new AcessorioRepository(context);
            _veiculoRepository = new VeiculoRepository(context);
        }

        public async Task<Acessorio> Create(AcessorioDto acessorio)
        {

            var created = await _repository.Create(acessorio.ToEntity());

            return created;
        }

        public async Task Delete(int id)
        {
            await _repository.Delete(id);
        }

        public async Task<List<Acessorio>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<Acessorio> GetById(int id)
        {
            return await _repository.GetById(id);
        }

        public async Task<Acessorio> Update(int id, AcessorioDto updatedAcessorio)
        {
            var existingAcessorio = await _repository.GetById(id);

            if (existingAcessorio != null)
            {
                existingAcessorio.Descricao = updatedAcessorio.Descricao;

                await _repository.Update(id, existingAcessorio);
                return existingAcessorio;
            }
            throw new EntityException("Acessório não encontrado com id " + id);
        }
    }
}
