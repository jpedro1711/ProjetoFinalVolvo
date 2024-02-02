using ConcessionariaAPI.Exceptions;
using ConcessionariaAPI.Models;
using Microsoft.EntityFrameworkCore;
using ConcessionariaAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ConcessionariaAPI.Repositories.Dto;
using System.Net;

namespace ConcessionariaAPI.Repositories
{
    public class VendedorRepository : IVendedorRepository<Vendedor>
    {
        private ConcessionariaContext _context;
        private bool disposed = false;
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
            throw new EntityException("Venda não encontrada", 404, "DELETE, VendedorRepository");
        }

        public async Task<Vendedor> GetById(int id)
        {
            var entity = await _context.Vendedor.FirstOrDefaultAsync(e => e.VendedorId == id);

            if (entity != null)
            {
                return entity;
            }
            throw new EntityException("Venda não encontrada", 404, "GET BY ID, VendedorRepository");
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
            throw new EntityException("Venda não encontrada", 404, "UPDATE, VendedorRepository");
        }

        public async Task<List<Salario>> GetSalarioMesAno(int id, int mes, int ano){
            var resultado = _context.Venda
                .Join(_context.Vendedor,
                    venda => venda.VendedorId,
                    vendedor => vendedor.VendedorId,
                    (venda, vendedor) => new { Venda = venda, Vendedor = vendedor })
                .Join(_context.Veiculo,
                    venda => venda.Venda.VeiculoId,
                    veiculo => veiculo.VeiculoId,
                    (venda, veiculo) => new { Venda = venda.Venda, Vendedor = venda.Vendedor, Veiculo = veiculo })
                .Where(x => x.Venda.DataVenda.Month == mes && x.Venda.DataVenda.Year == ano)
                .GroupBy(result => new
                {
                    result.Vendedor.VendedorId,
                    result.Vendedor.Nome,
                    result.Vendedor.SalarioBase,
                    Mes = result.Venda.DataVenda.Month,
                    Ano = result.Venda.DataVenda.Year
                })
                .Select(groupedResult => new
                {
                    ID = groupedResult.Key.VendedorId,
                    Nome = groupedResult.Key.Nome,
                    SalarioCalculado = (double)groupedResult.Key.SalarioBase + ((double)groupedResult.Sum(v => v.Veiculo.Valor) * 0.01),
                    Mes = groupedResult.Key.Mes,
                    Ano = groupedResult.Key.Ano
                })
                .ToList();

            List<Salario> salarios = new List<Salario>();
            foreach (var r in resultado){
                Salario salario = new Salario(r.ID, r.Nome, r.SalarioCalculado, r.Mes, r.Ano);
                salarios.Add(salario);
            }
            
            return salarios;
        }
    }
}