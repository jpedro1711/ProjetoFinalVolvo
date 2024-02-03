using ConcessionariaAPI.Exceptions;
using ConcessionariaAPI.Models;
using Microsoft.EntityFrameworkCore;
using ConcessionariaAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ConcessionariaAPI.Repositories.Dto;
using System.Net;
using System.Reflection.Metadata.Ecma335;

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

        public async Task Delete(int id)
        {
            var entity = await _context.Vendedor.FirstOrDefaultAsync(e => e.VendedorId == id);

            if (entity != null)
            {
                _context.Vendedor.Remove(entity);
                await _context.SaveChangesAsync();
            }
            throw new EntityException("Venda não encontrada com id " + id, 404, "DELETE, VendedorRepository");
        }

        public async Task<Vendedor> GetById(int id)
        {
            var entity = await _context.Vendedor.Include("Telefones").Include("Enderecos").FirstOrDefaultAsync(e => e.VendedorId == id);

            if (entity != null)
            {
                return entity;
            }
            throw new EntityException("Venda não encontrada", 404, "GET BY ID, VendedorRepository");
        }

        public async Task<List<Vendedor>> GetAll()
        {
            return await _context.Vendedor.Include("Telefones").Include("Enderecos").ToListAsync();
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

            DateTime DataAdmissao = _context.Vendedor
                .Where(v => v.VendedorId == id)
                .Select(v => v.DataAdmissao)
                .FirstOrDefault();

            if((DataAdmissao.Month <= mes && DataAdmissao.Year == ano) || (DataAdmissao.Year < ano)){
                var resultado = _context.Venda
                .Join(
                    _context.Vendedor,
                    venda => venda.VendedorId,
                    vendedor => vendedor.VendedorId,
                    (venda, vendedor) => new { Venda = venda, Vendedor = vendedor }
                )
                .Join(
                    _context.Veiculo,
                    v => v.Venda.VeiculoId,
                    veiculo => veiculo.VeiculoId,
                    (v, veiculo) => new { V = v, Veiculo = veiculo }
                )
                .Where(v => v.V.Venda.DataVenda.Month == mes && v.V.Venda.DataVenda.Year == ano)
                .GroupBy(
                    v => new
                    {
                        v.V.Vendedor.VendedorId,
                        v.V.Vendedor.Nome,
                        v.V.Vendedor.SalarioBase,
                        Mes = v.V.Venda.DataVenda.Month,
                        Ano = v.V.Venda.DataVenda.Year
                    }
                )
                .Select(g => new Salario
                {
                    ID = g.Key.VendedorId,
                    Nome = g.Key.Nome,
                    SalarioCalculado = (double)g.Key.SalarioBase + ((double)g.Sum(v => v.Veiculo.Valor) * 0.01),
                    Mes = g.Key.Mes,
                    Ano = g.Key.Ano
                })
                .ToList();
            
                if(resultado.Count == 0){
                    resultado = _context.Vendedor
                        .Where(v => v.VendedorId == id)
                        .Select(g => new Salario
                        {
                            ID = g.VendedorId,
                            Nome = g.Nome,
                            SalarioCalculado = (double)g.SalarioBase,
                            Mes = mes,
                            Ano = ano
                        })
                        .ToList();
                }
                
                return resultado;        
            }else{
                throw new EntityException($"Não foi possível calcula o salário do vendedor:{id} no mês:{mes} do ano:{ano}, pois o mesmo não estava contratado!");
            }            
            return null;
        }

        
    }
}