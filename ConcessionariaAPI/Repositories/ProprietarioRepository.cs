﻿using ConcessionariaAPI.Exceptions;
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

            Proprietario valCPF = null;
            if(entity.CPF != ""){
                valCPF = await _context.Proprietario.FirstOrDefaultAsync(e => e.CPF == entity.CPF);
                if(valCPF != null){
                    throw new EntityException($"CPF:{entity.CPF} já está cadastrado no sistema!");
                }
            }

            Proprietario valCNPJ = null;
            if(entity.CNPJ != ""){
                valCNPJ = await _context.Proprietario.FirstOrDefaultAsync(e => e.CNPJ == entity.CNPJ);
                if(valCNPJ != null){
                    throw new EntityException($"CNPJ:{entity.CNPJ} já está cadastrado no sistema!");
                }
            }

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
            Proprietario valCPF = null;
            if(proprietario.CPF != ""){
                valCPF = await _context.Proprietario.FirstOrDefaultAsync(e => e.CPF == proprietario.CPF);
                if(valCPF != null && valCPF.ProprietarioId != proprietario.ProprietarioId){
                    throw new EntityException($"CPF:{proprietario.CPF} já está cadastrado no sistema!");
                }
            }

            Proprietario valCNPJ = null;
            if(proprietario.CNPJ != ""){
                valCNPJ = await _context.Proprietario.FirstOrDefaultAsync(e => e.CNPJ == proprietario.CNPJ);
                if(valCNPJ != null && valCNPJ.ProprietarioId != proprietario.ProprietarioId){
                    throw new EntityException($"CNPJ:{proprietario.CNPJ} já está cadastrado no sistema!");
                }
            }

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
