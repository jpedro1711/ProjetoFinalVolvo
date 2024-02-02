using ConcessionariaAPI.Exceptions;
using ConcessionariaAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ConcessionariaAPI.Repositories
{
    public class EnderecoRepository : IRepository<Endereco>
    {
        private ConcessionariaContext _context;
        public EnderecoRepository(ConcessionariaContext context)
        {
            _context = context;
        }

        public async Task<Endereco> Create(Endereco entity)
        {
            await _context.Endereco.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async void Delete(int id)
        {
            var entity = await _context.Endereco.FirstOrDefaultAsync(e => e.EnderecoId == id);

            if (entity != null)
            {
                _context.Endereco.Remove(entity);
                await _context.SaveChangesAsync();
            }
            throw new EntityException("Endereço não encontrado");
        }

        public async Task<Endereco> GetById(int id)
        {
            var entity = await _context.Endereco.Include("Proprietarios").Include("Vendedores").FirstOrDefaultAsync(e => e.EnderecoId == id);

            if (entity != null)
            {
                return entity;
            }
            throw new EntityException("Endereço não encontrado");
        }

        public async Task<List<Endereco>> GetAll()
        {
            return await _context.Endereco.Include("Proprietarios").Include("Vendedores").ToListAsync();
        }

        public async Task<Endereco> Update(int id, Endereco endereco)
        {
            var entity = await _context.Endereco.FirstOrDefaultAsync(e => e.EnderecoId == id);

            if (entity != null)
            {
                _context.Entry(entity).CurrentValues.SetValues(endereco);
                await _context.SaveChangesAsync();
                return endereco;
            }
            throw new EntityException("Endereço não encontrado");
        }
    }
}
