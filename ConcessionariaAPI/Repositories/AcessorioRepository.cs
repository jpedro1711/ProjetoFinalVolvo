using ConcessionariaAPI.Exceptions;
using ConcessionariaAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace ConcessionariaAPI.Repositories
{
    public class AcessorioRepository : IRepository<Acessorio>
    {
        private ConcessionariaContext _context;
        private bool disposed = false;
        public AcessorioRepository(ConcessionariaContext context)
        {
            _context = context;
        }

        public async Task<Acessorio> Create(Acessorio entity)
        {
            await _context.Acessorio.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async void Delete(int id)
        {
            var entity = await _context.Acessorio.FirstOrDefaultAsync(e => e.AcessorioID == id);

            if (entity != null)
            {
                _context.Acessorio.Remove(entity);
                await _context.SaveChangesAsync();
            }
            throw new EntityException("Acessório não encontrado");
        }

        public async Task<Acessorio> GetById(int id)
        {
            var entity = await _context.Acessorio.Include("Veiculos").FirstOrDefaultAsync(e => e.AcessorioID == id);

            if (entity != null)
            {
                return entity;
            }
            throw new EntityException("Acessório não encontrado");
        }

        public async Task<List<Acessorio>> GetAll()
        {
            return await _context.Acessorio.Include("Veiculos").ToListAsync();
        }

        public async Task<Acessorio> Update(int id, Acessorio acessorio)
        {
            var entity = await _context.Acessorio.FirstOrDefaultAsync(e => e.AcessorioID == id);

            if (entity != null)
            {
                _context.Entry(entity).CurrentValues.SetValues(acessorio);
                await  _context.SaveChangesAsync();
                return acessorio;
            }
            throw new EntityException("Acessório não encontrado");
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
