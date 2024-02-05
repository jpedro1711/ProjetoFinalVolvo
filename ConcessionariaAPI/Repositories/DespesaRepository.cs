using ConcessionariaAPI.Exceptions;
using ConcessionariaAPI.Models;
using ConcessionariaAPI.Repositories.interfaces;
using Microsoft.EntityFrameworkCore;

namespace ConcessionariaAPI.Repositories
{
    public class DespesaRepository : IRepository<Despesa>, IDisposable
    {
        private ConcessionariaContext _context;
        private bool disposed = false;
        public DespesaRepository(ConcessionariaContext context)
        {
            _context = context;
        }

        public async Task<Despesa> Create(Despesa entity)
        {
            await _context.Despesa.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task Delete(int id)
        {
            var entity = await _context.Despesa.FirstOrDefaultAsync(e => e.DespesaID == id);

            if (entity == null)
            {
                throw new EntityException("Despesa não encontrada com id " + id, 404, "DELETE, DespesaRepository");
            }
            _context.Despesa.Remove(entity);
            await _context.SaveChangesAsync();

        }

        public async Task<Despesa> GetById(int id)
        {
            var entity = await _context.Despesa.FirstOrDefaultAsync(e => e.DespesaID == id);

            if (entity != null)
            {
                return entity;
            }
            throw new EntityException("Despesa não encontrada", 404, "GET BY ID, DespesaRepository");
        }

        public async Task<List<Despesa>> GetAll()
        {
            return await _context.Despesa.ToListAsync();
        }

        public async Task<Despesa> Update(int id, Despesa despesa)
        {
            var entity = await _context.Despesa.FirstOrDefaultAsync(e => e.DespesaID == id);

            if (entity != null)
            {
                _context.Entry(entity).CurrentValues.SetValues(despesa);
                await _context.SaveChangesAsync();
                return despesa;
            }
            throw new EntityException("Despesa não encontrada", 404, "UPDATE DespesaRepository");
        }        

        protected async virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    await _context.DisposeAsync();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
