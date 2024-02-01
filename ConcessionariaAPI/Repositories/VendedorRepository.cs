using ConcessionariaAPI.Exceptions;
using ConcessionariaAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ConcessionariaAPI.Repositories
{
    public class VendedorRepository : IRepository<Vendedor>
    {
        private ConcessionariaContext _context;
        public VendedorRepository(ConcessionariaContext context)
        {
            _context = context;
        }

        public async Task<Vendedor> Create(Vendedor entity)
        {
            await _context.Vendedor.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async void Delete(int id)
        {
            var entity = await _context.Vendedor.FirstOrDefaultAsync(e => e.VendedorId == id);

            if (entity != null)
            {
                _context.Vendedor.Remove(entity);
                await _context.SaveChangesAsync();
            }
            throw new EntityException("Vendedor não encontrado");
        }

        public async Task<Vendedor> GetById(int id)
        {
            var entity = await _context.Vendedor.FirstOrDefaultAsync(e => e.VendedorId == id);

            if (entity != null)
            {
                return entity;
            }
            throw new EntityException("Vendedor não encontrado");
        }

        public async Task<List<Vendedor>> GetAll()
        {
            return await _context.Vendedor.ToListAsync();
        }

        public async Task<Vendedor> Update(int id, Vendedor vendedor)
        {
            var entity = await _context.Vendedor.FirstOrDefaultAsync(e => e.VendedorId == id);

            if (entity != null)
            {
                _context.Entry(entity).CurrentValues.SetValues(vendedor);
                await _context.SaveChangesAsync();
                return vendedor;
            }
            throw new EntityException("Vendedor não encontrado");
        }
    }
}
