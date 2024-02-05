using ConcessionariaAPI.Models;
using ConcessionariaAPI.Repositories;
using ConcessionariaAPI.Exceptions;
using ConcessionariaAPI.Services.interfaces;
using ConcessionariaAPI.Repositories.interfaces;
using ConcessionariaAPI.Models.dtos;

namespace ConcessionariaAPI.Services
{
    public class VendaServico : IVendaService
    {
        private IRepository<Venda> _repository;
        private IVendedorService _vendedorService;
        private IVeiculoService _veiculoService;

        public VendaServico(ConcessionariaContext context)
        {
            _repository = new VendaRepository(context);     
            _vendedorService = new VendedorService(context);    
            _veiculoService = new VeiculoService(context);
        }


        public async Task<Venda> Create(VendaDto venda)
        {
            var vendedor = await _vendedorService.GetById(venda.VendedorId);
            var carro = await _veiculoService.GetById(venda.VeiculoId);

            Venda newVenda = new Venda();
            newVenda.Vendedor = vendedor;
            newVenda.Veiculo = carro;

            var created = await _repository.Create(newVenda);

            return created;
        }

        public async Task Delete(int id)
        {
            await _repository.Delete(id);
        }

        public async Task<List<Venda>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<Venda> GetById(int id)
        {
            return await _repository.GetById(id);
        }

        public async Task<Venda> Update(int id, VendaDto updatedVenda)
        {
            Venda existingVenda = await _repository.GetById(id);

            if (existingVenda == null)
            {
                throw new EntityException("Erro ao atualizar venda com id " + id + ", não foi encontrada ou dados inválidos");
            }

            var vendedor = await _vendedorService.GetById(updatedVenda.VendedorId);
            var carro = await _veiculoService.GetById(updatedVenda.VeiculoId);

            existingVenda.Vendedor = vendedor;
            existingVenda.Veiculo = carro;

            var updated = await _repository.Update(id, existingVenda);
            return updated;
        }
    }
}
