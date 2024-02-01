using ConcessionariaAPI.Exceptions;
using ConcessionariaAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ConcessionariaAPI.Repositories
{
    public class VendaRepository : IRepository<Venda>
    {
        private ConcessionariaContext _context;
        public VendaRepository(ConcessionariaContext context)
        {
            _context = context;
        }

        public async Task<Venda> Create(Venda entity)
        {
            await _context.Venda.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async void Delete(int id)
        {
            var entity = await _context.Venda.FirstOrDefaultAsync(e => e.VendaId == id);

            if (entity != null)
            {
                _context.Venda.Remove(entity);
                await _context.SaveChangesAsync();
            }
            throw new EntityException("Venda não encontrado");
        }

        public async Task<Venda> GetById(int id)
        {
            var entity = await _context.Venda.FirstOrDefaultAsync(e => e.VendaId == id);

            if (entity != null)
            {
                return entity;
            }
            throw new EntityException("Venda não encontrado");
        }

        public async Task<List<Venda>> GetAll()
        {
            return await _context.Venda.ToListAsync();
        }

        public async Task<Venda> Update(int id, Venda venda)
        {
            var entity = await _context.Venda.FirstOrDefaultAsync(e => e.VendaId == id);

            if (entity != null)
            {
                _context.Entry(entity).CurrentValues.SetValues(venda);
                await _context.SaveChangesAsync();
                return venda;
            }
            throw new EntityException("Venda não encontrado");
        }
    }
}
