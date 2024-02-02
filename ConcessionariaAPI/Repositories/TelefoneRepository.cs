﻿using ConcessionariaAPI.Exceptions;
using ConcessionariaAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ConcessionariaAPI.Repositories
{
    public class TelefoneRepository : IRepository<Telefone>, IDisposable
    {
        private ConcessionariaContext _context;
        private bool disposed = false;
        public TelefoneRepository(ConcessionariaContext context)
        {
            _context = context;
        }

        public async Task<Telefone> Create(Telefone entity)
        {
            await _context.Telefone.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task Delete(int id)
        {
            var entity = await _context.Telefone.FirstOrDefaultAsync(e => e.TelefoneId == id);

            if (entity != null)
            {
                _context.Telefone.Remove(entity);
                await _context.SaveChangesAsync();
            }
            throw new EntityException("Telefone não encontrado", 404, "DELETE, TelefoneRepository");
        }

        public async Task<Telefone> GetById(int id)
        {
            var entity = await _context.Telefone.FirstOrDefaultAsync(e => e.TelefoneId == id);

            if (entity != null)
            {
                return entity;
            }
            throw new EntityException("Telefone não encontrado", 404, "GET BY ID, TelefoneRepository");
        }

        public async Task<List<Telefone>> GetAll()
        {
            return await _context.Telefone.ToListAsync();
        }

        public async Task<Telefone> Update(int id, Telefone telefone)
        {
            var entity = await _context.Telefone.FirstOrDefaultAsync(e => e.TelefoneId == id);

            if (entity != null)
            {
                _context.Entry(entity).CurrentValues.SetValues(telefone);
                await _context.SaveChangesAsync();
                return telefone;
            }
            throw new EntityException("Telefone não encontrado", 404, "UPDATE, TelefoneRepository");
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
