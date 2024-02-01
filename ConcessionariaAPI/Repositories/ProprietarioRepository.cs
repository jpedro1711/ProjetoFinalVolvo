using ConcessionariaAPI.Exceptions;
using ConcessionariaAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ConcessionariaAPI.Repositories
{
    public class ProprietarioRepository : IRepository<Proprietario>
    {
        private ConcessionariaContext _context;
        public ProprietarioRepository(ConcessionariaContext context)
        {
            _context = context;
        }

        public async Task<Proprietario> Create(Proprietario entity)
        {
            await _context.Proprietario.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async void Delete(int id)
        {
            var entity = await _context.Proprietario.FirstOrDefaultAsync(e => e.ProprietarioId == id);

            if (entity != null)
            {
                _context.Proprietario.Remove(entity);
                await _context.SaveChangesAsync();
            }
            throw new EntityException("Proprietário não encontrado");
        }

        public async Task<Proprietario> GetById(int id)
        {
            var entity = await _context.Proprietario.FirstOrDefaultAsync(e => e.ProprietarioId == id);

            if (entity != null)
            {
                return entity;
            }
            throw new EntityException("Proprietário não encontrado");
        }

        public async Task<List<Proprietario>> GetAll()
        {
            return await _context.Proprietario.ToListAsync();
        }

        public async Task<Proprietario> Update(int id, Proprietario proprietario)
        {
            var entity = await _context.Proprietario.FirstOrDefaultAsync(e => e.ProprietarioId == id);

            if (entity != null)
            {
                _context.Entry(entity).CurrentValues.SetValues(proprietario);
                await _context.SaveChangesAsync();
                return proprietario;
            }
            throw new EntityException("Proprietário não encontrado");
        }
    }
}
