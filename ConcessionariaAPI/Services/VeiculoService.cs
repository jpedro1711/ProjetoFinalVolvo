using ConcessionariaAPI.Models;
using ConcessionariaAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using ConcessionariaAPI.Exceptions;
using ConcessionariaAPI.Migrations;
using Microsoft.AspNetCore.Http.HttpResults;
using ConcessionariaAPI.Services.interfaces;
using ConcessionariaAPI.Repositories.interfaces;

namespace ConcessionariaAPI.Services
{
    public class VeiculoService : IVeiculoService<Veiculo>
    {
        private IVeiculoRepository<Veiculo> _repository;
        private IRepository<Acessorio> _acessorioRepository;

        public VeiculoService(ConcessionariaContext context)
        {
            _repository = new VeiculoRepository(context);  
            _acessorioRepository = new AcessorioRepository(context);          
        }

        public async Task<Veiculo> Create(Veiculo veiculo)
        {
            List<Acessorio> acessorios = new List<Acessorio>();
            foreach (var acessorio in veiculo.Acessorios)
            {
                if (acessorio.AcessorioID != null)
                {
                    Acessorio result = await _acessorioRepository.GetById((int)acessorio.AcessorioID);

                    if (result != null)
                    {
                        acessorios.Add(result);
                    }
                }
                else
                {
                    Acessorio result = await _acessorioRepository.Create(acessorio);
                    acessorios.Add(result);
                }
            }

            veiculo.Acessorios = acessorios;                                  

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

        public async Task<Veiculo> Update(int id, Veiculo uptadedVeiculo)
        {
            Veiculo existingVeiculo = await _repository.GetById(id);

            if (existingVeiculo != null)
            {
                existingVeiculo.NumeroChassi = uptadedVeiculo.NumeroChassi;
                existingVeiculo.Valor = uptadedVeiculo.Valor;
                existingVeiculo.Quilometragem = uptadedVeiculo.Quilometragem;
                existingVeiculo.VersaoSistema = uptadedVeiculo.VersaoSistema;
                existingVeiculo.ProprietarioId = uptadedVeiculo.ProprietarioId;
                existingVeiculo.Proprietario = uptadedVeiculo.Proprietario;
                existingVeiculo.Modelo = uptadedVeiculo.Modelo;

                ICollection<Acessorio> acessorios = new List<Acessorio>();

                foreach (var acessorio in uptadedVeiculo.Acessorios)
                {
                    if (acessorio.AcessorioID != null)
                    {
                        var existingAcessorio = await _acessorioRepository.GetById((int)acessorio.AcessorioID);
                        if (existingAcessorio != null)
                        {
                            existingAcessorio.Descricao = acessorio.Descricao;                            
                            acessorios.Add(existingAcessorio);
                        }
                    }
                    else
                    {
                        Acessorio result = await _acessorioRepository.Create(acessorio);
                        acessorios.Add(result);
                    }
                }

                existingVeiculo.Acessorios = acessorios;                

                var updated = await _repository.Update(id, existingVeiculo);
                return updated;
            }
            throw new EntityException("Erro ao atualizar veículo com id " + id + ", não foi encontrado ou dados inválidos");                       
        }

        public async Task<List<Veiculo>> GetVeiculosByKilomers(int km, string system)
        {
            return await _repository.GetVeiculosByKilomers(km, system);              
        }
    }
}
