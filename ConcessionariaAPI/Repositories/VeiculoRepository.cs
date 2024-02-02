﻿using ConcessionariaAPI.Exceptions;
using ConcessionariaAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConcessionariaAPI.Repositories
{
    public class VeiculoRepository : IVeiculoRepository<Veiculo>
    {
        private ConcessionariaContext _context;
        public VeiculoRepository(ConcessionariaContext context)
        {
            _context = context;
        }

        public async Task<Veiculo> Create(Veiculo entity)
        {
            await _context.Veiculo.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async void Delete(int id)
        {
            var entity = await _context.Veiculo.FirstOrDefaultAsync(e => e.VeiculoId == id);

            if (entity != null)
            {
                _context.Veiculo.Remove(entity);
                await _context.SaveChangesAsync();
            }
            throw new EntityException("Veículo não encontrado");
        }

        public async Task<Veiculo> GetById(int id)
        {
            var entity = await _context.Veiculo.FirstOrDefaultAsync(e => e.VeiculoId == id);

            if (entity != null)
            {
                return entity;
            }
            throw new EntityException("Veículo não encontrado");
        }

        public async Task<List<Veiculo>> GetAll()
        {
            return await _context.Veiculo.ToListAsync();
        }

        public async Task<Veiculo> Update(int id, Veiculo veiculo)
        {
            var entity = await _context.Veiculo.FirstOrDefaultAsync(e => e.VeiculoId == id);

            if (entity != null)
            {
                _context.Entry(entity).CurrentValues.SetValues(veiculo);
                await _context.SaveChangesAsync();
                return veiculo;
            }
            throw new EntityException("Veículo não encontrado");
        }

        public async Task<List<Veiculo>> GetVeiculosByKilomers(int km ,string version)
        {
            var cars = await _context.Veiculo
                .Where(c => c.Quilometragem >= km)
                .Where(c => c.VersaoSistema == version)
                .ToListAsync();
            return cars;
        }        
    }
}
