using ConcessionariaAPI.Exceptions;
using ConcessionariaAPI.Models;
using ConcessionariaAPI.Repositories.interfaces;
using Microsoft.EntityFrameworkCore;

namespace ConcessionariaAPI.Repositories
{
    public class ProprietarioRepository : IRepository<Proprietario>, IDisposable
    {
        private ConcessionariaContext _context;
        private bool disposed = false;
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

        public async Task Delete(int id)
        {
            var entity = await _context.Proprietario.FirstOrDefaultAsync(e => e.ProprietarioId == id);

            if (entity == null)
            {
                throw new EntityException("Proprietário não encontrado", 404, "DELETE, ProprietarioRepository");
            }
            _context.Proprietario.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<Proprietario> GetById(int id)
        {
            var entity = await _context.Proprietario.Include("Enderecos").Include("Telefones").FirstOrDefaultAsync(e => e.ProprietarioId == id);

            if (entity != null)
            {
                return entity;
            }
            throw new EntityException("Proprietário não encontrado", 404, "GET BY ID, ProprietarioRepository");
        }

        public async Task<List<Proprietario>> GetAll()
        {
            return await _context.Proprietario.Include("Enderecos").Include("Telefones").ToListAsync();
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
            throw new EntityException("Proprietário não encontrado", 404, "UPDATE, ProprietarioRepository");
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
