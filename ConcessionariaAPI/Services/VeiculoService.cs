using ConcessionariaAPI.Models;
using ConcessionariaAPI.Repositories;
using ConcessionariaAPI.Exceptions;
using ConcessionariaAPI.Migrations;
using ConcessionariaAPI.Services.interfaces;
using ConcessionariaAPI.Repositories.interfaces;
using ConcessionariaAPI.Models.dtos;
using Microsoft.AspNetCore.Http.HttpResults;

namespace ConcessionariaAPI.Services
{
    public class VeiculoService : IVeiculoService
    {
        private IVeiculoRepository<Veiculo> _repository;
        private IAcessorioService _acessorioService;
        private IProprietarioService _proprietarioService;

        public VeiculoService(ConcessionariaContext context)
        {
            _repository = new VeiculoRepository(context);  
            _acessorioService = new AcessorioService(context);
            _proprietarioService = new ProprietarioService(context);
        }

        public async Task<Veiculo> Create(VeiculoDto veiculoDto)
        {
            Veiculo veiculo = veiculoDto.ToEntity();

            if (veiculoDto.ProprietarioId != null)
            {
                Proprietario p = await _proprietarioService.GetById((int)veiculoDto.ProprietarioId);
                veiculo.Proprietario = p;
            }
            
            

            foreach (AcessorioDto acessorio in veiculoDto.acessorios)
            {
                Acessorio ac;
                if (acessorio.AcessorioID != null)
                {
                    ac = await _acessorioService.GetById((int)acessorio.AcessorioID);
                }
                else
                {
                    ac = await _acessorioService.Create(acessorio);
                }
                if (ac != null)
                {
                    veiculo.Acessorios.Add(ac);
                }
                else
                {
                    throw new EntityException("Acessório não encontrado com id " + acessorio.AcessorioID + " ao criar veículo");
                }
            }

            var created = await _repository.Create(veiculo);

            return created;
        }

        public async Task Delete(int id)
        {
            await _repository.Delete(id);
        }

        public async Task<List<Veiculo>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<Veiculo> GetById(int id)
        {
            return await _repository.GetById(id);
        }

        public async Task<Veiculo> Update(int id, VeiculoDto veiculoDto)
        {
            Veiculo veiculoAtualizado = await _repository.GetById(id);

            if (veiculoAtualizado == null)
            {
                throw new EntityException("Erro ao atualizar veículo com id " + id + ", não foi encontrado ou dados inválidos");
            }

            foreach (AcessorioDto acessorio in veiculoDto.acessorios)
            {
                Acessorio ac;
                if (acessorio.AcessorioID != null)
                {
                    ac = await _acessorioService.GetById((int)acessorio.AcessorioID);
                }
                else
                {
                    ac = await _acessorioService.Create(acessorio);
                }
                if (ac != null)
                {
                    veiculoAtualizado.Acessorios.Add(ac);
                }
                else
                {
                    throw new EntityException("Acessório não encontrado com id " + acessorio.AcessorioID + " ao atualizar veículo");
                }
            }

            var updated = await _repository.Update(id, veiculoDto.ToEntity());
            return updated;
        }

        public async Task<List<Veiculo>> GetVeiculosByKilomers(int km, string system)
        {
            return await _repository.GetVeiculosByKilomers(km, system);              
        }
    }
}
