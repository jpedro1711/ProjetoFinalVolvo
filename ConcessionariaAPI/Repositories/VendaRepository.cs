﻿using ConcessionariaAPI.Exceptions;
using ConcessionariaAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ConcessionariaAPI.Repositories
{
    public class VendaRepository : IRepository<Venda>, IDisposable
    {
        private ConcessionariaContext _context;
        private bool disposed = false;
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
            throw new EntityException("Venda não encontrada", 404, "DELETE, VendaRepository");
        }

        public async Task<Venda> GetById(int id)
        {
            var entity = await _context.Venda
                    .Include(v => v.Veiculo)
                    .ThenInclude(veic => veic.Acessorios)
                    .Include(v => v.Vendedor)
                    .FirstOrDefaultAsync(e => e.VendaId == id);

            if (entity != null)
            {
                return entity;
            }
            throw new EntityException("Venda não encontrada", 404, "GET BY ID, VendaRepository");
        }

        public async Task<List<Venda>> GetAll()
        {
            return await _context.Venda.Include("Veiculo").Include("Vendedor").ToListAsync();
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
            throw new EntityException("Venda não encontrada", 404, "UPDATE VendaRepository");
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
