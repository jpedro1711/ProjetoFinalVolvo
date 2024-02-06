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
            if(venda.VendaId != null){
                throw new EntityException("ID da venda não deve ser informado!");
            }

            var vendedor = await _vendedorService.GetById(venda.VendedorId);
            if(vendedor == null){
                throw new EntityException("ID do vendedor informado não está cadastrado!");
            }

            var carro = await _veiculoService.GetById(venda.VeiculoId);
            if(carro == null){
                throw new EntityException("ID do carro informado não está cadastrado!");
            }

            if (vendedor == null || carro == null)
            {
                throw new EntityException("Erro ao criar venda, dados inválidos");
            }

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
            if(id != updatedVenda.VendaId){
                throw new EntityException("IDs informados não coincidem!");
            }

            Venda existingVenda = await _repository.GetById(id);

            if (existingVenda == null)
            {
                throw new EntityException("Erro ao atualizar venda com id " + id + ", não foi encontrada ou dados inválidos");
            }

            var vendedor = await _vendedorService.GetById(updatedVenda.VendedorId);
            if(vendedor == null){
                throw new EntityException("ID do vendedor informado não está cadastrado!");
            }

            var carro = await _veiculoService.GetById(updatedVenda.VeiculoId);
            if(carro == null){
                throw new EntityException("ID do carro informado não está cadastrado!");
            }

            if (vendedor == null || carro == null)
            {
                throw new EntityException("Erro ao criar venda, dados inválidos de carro ou vendedor");
            }

            existingVenda.Vendedor = vendedor;
            existingVenda.Veiculo = carro;

            var updated = await _repository.Update(id, existingVenda);
            return updated;
        }
    }
}
