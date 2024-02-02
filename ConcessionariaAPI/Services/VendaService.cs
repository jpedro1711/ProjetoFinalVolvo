using ConcessionariaAPI.Models;
using ConcessionariaAPI.Repositories;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using ConcessionariaAPI.Exceptions;

namespace ConcessionariaAPI.Services
{
    public class VendaServico : IService<Venda>
    {
        private IRepository<Venda> _repository;       

        public VendaServico(ConcessionariaContext context)
        {
            _repository = new VendaRepository(context);            
        }


        public async Task<Venda> Create(Venda venda)
        {         
            var created = await _repository.Create(venda);

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

        public async Task<Venda> Update(int id, Venda updatedVenda)
        {
            Venda existingVenda = await _repository.GetById(id);

            if (existingVenda != null)
            {
                existingVenda.DataVenda = updatedVenda.DataVenda;
                existingVenda.VeiculoId = updatedVenda.VeiculoId;
                existingVenda.VendedorId = updatedVenda.VendedorId;

                var updated = await _repository.Update(id, existingVenda);
                return updated;
            }
            throw new EntityException("Erro ao atualizar venda com id " + id + ", não foi encontrada ou dados inválidos");                     
        }
    }
}
