using ConcessionariaAPI.Models;
using ConcessionariaAPI.Repositories;
using ConcessionariaAPI.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace ConcessionariaAPI.Services
{
    public class AcessorioService : IService<Acessorio>
    {
        private IRepository<Acessorio> _repository;
        private IVeiculoRepository<Veiculo> _veiculoRepository;
        public AcessorioService(ConcessionariaContext context) 
        {
            _repository = new AcessorioRepository(context);
            _veiculoRepository = new VeiculoRepository(context);
        }

        public async Task<Acessorio> Create(Acessorio acessorio)
        {
            ICollection<Veiculo> veiculos = new List<Veiculo>();

            foreach (var veiculo in acessorio.Veiculos)
            {
                if (veiculo.VeiculoId != null)
                {
                    var result = await _veiculoRepository.GetById((int)acessorio.AcessorioID);

                    if (result != null)
                    {
                        veiculos.Add(result);
                    }
                }
                else
                {
                    var created = await _veiculoRepository.Create(veiculo);
                    veiculos.Add(created);
                }
            }
            acessorio.Veiculos = veiculos;

            var acessory = await _repository.Create(acessorio);

            return acessorio;
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }

        public async Task<List<Acessorio>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<Acessorio> GetById(int id)
        {
            return await _repository.GetById(id);
        }

        public async Task<Acessorio> Update(int id, Acessorio updatedAcessorio)
        {
            var existingAcessorio = await _repository.GetById(id);

            if (existingAcessorio != null)
            {
                existingAcessorio.Descricao = updatedAcessorio.Descricao;

                ICollection<Veiculo> veiculos = new List<Veiculo>();
                foreach (var veiculo in updatedAcessorio.Veiculos)
                {
                    if (veiculo.VeiculoId != null)
                    {
                        var existingVeiculo = await _veiculoRepository.GetById(veiculo.VeiculoId);
                        if (existingVeiculo != null)
                        {
                            existingVeiculo.NumeroChassi = veiculo.NumeroChassi;
                            existingVeiculo.Valor = veiculo.Valor;
                            existingVeiculo.Quilometragem = veiculo.Quilometragem;
                            existingVeiculo.VersaoSistema = veiculo.VersaoSistema;
                            existingVeiculo.ProprietarioId = veiculo.ProprietarioId;
                            existingVeiculo.Proprietario = veiculo.Proprietario;
                            existingVeiculo.Acessorios.Add(updatedAcessorio);
                            veiculos.Add(existingVeiculo);
                        }
                    }
                    else
                    {
                        var created = await _veiculoRepository.Create(veiculo);
                        veiculos.Add(created);
                    }
                }

                existingAcessorio.Veiculos = veiculos;

                await _repository.Update(id, existingAcessorio);
                return existingAcessorio;
            }
            throw new EntityException("Acessório não encontrado com id " + id);
        }
    }
}
