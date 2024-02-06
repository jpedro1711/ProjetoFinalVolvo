using ConcessionariaAPI.Models;
using ConcessionariaAPI.Repositories;
using ConcessionariaAPI.Exceptions;
using ConcessionariaAPI.Services.interfaces;
using ConcessionariaAPI.Repositories.interfaces;
using ConcessionariaAPI.Models.dtos;

namespace ConcessionariaAPI.Services
{
    public class VendedorService : IVendedorService
    {
        private IVendedorRepository<Vendedor> _repository;
        private ITelefoneService _telefoneService;
        private IEnderecoService _enderecoService;

        public VendedorService(ConcessionariaContext context)
        {
            _repository = new VendedorRepository(context);
            _telefoneService = new TelefoneService(context);
            _enderecoService = new EnderecoService(context);
        }


        public async Task<Vendedor> Create(VendedorDto vendedorDto)        
        {            
            

            Vendedor vendedor = vendedorDto.ToEntity();            

            foreach (TelefoneDto telefone in vendedorDto.Telefones)
            {
                Telefone tel;
                if (telefone.TelefoneId != null)
                {
                    tel = await _telefoneService.GetById((int)telefone.TelefoneId);
                }
                else
                {
                    tel = await _telefoneService.Create(telefone);
                }
                if (tel != null)
                {
                    vendedor.Telefones.Add(tel);
                }
                else
                {
                    throw new EntityException("Erro ao criar vendedor, dados inválidos, telefone não encontrado com id " + telefone.TelefoneId);
                }
            }

            foreach (EnderecoDto endereco in vendedorDto.Enderecos)
            {
                Endereco end;
                if (endereco.EnderecoId != null)
                {
                    end = await _enderecoService.GetById((int)endereco.EnderecoId);
                }
                else
                {
                    end = await _enderecoService.Create(endereco);
                }
                if (end != null)
                {
                    vendedor.Enderecos.Add(end);
                }
                else
                {
                    throw new EntityException("Erro ao criar vendedor, dados inválidos, endereço não encontrado com id " + endereco.EnderecoId);
                }
            }

            var created = await _repository.Create(vendedor);
            return created;
        }

        public async Task Delete(int id)
        {
            await _repository.Delete(id);
        }

        public async Task<List<Vendedor>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<Vendedor> GetById(int id)
        {
            return await _repository.GetById(id);
        }

        public async Task<Vendedor> Update(int id, VendedorDto vendedorDto)
        {           
            Vendedor vendedorAtualizado = await _repository.GetById(id);

            if (vendedorAtualizado == null)
            {
                throw new EntityException("Erro ao atualizar vendedor com id " + id + ", não foi encontrado ou dados inválidos");
            }

            foreach (TelefoneDto telefone in vendedorDto.Telefones)
            {
                Telefone tel;
                if (telefone.TelefoneId != null)
                {

                    tel = await _telefoneService.GetById((int)telefone.TelefoneId);
                }
                else
                {
                    tel = await _telefoneService.Create(telefone);
                }
                if (tel != null)
                {
                    vendedorAtualizado.Telefones.Add(tel);
                }
                else
                {
                    throw new EntityException("Erro ao atualizar vendedor, dados inválidos, telefone não encontrado com id " + telefone.TelefoneId);
                }
            }


            foreach (EnderecoDto endereco in vendedorDto.Enderecos)
            {
                Endereco end;
                if (endereco.EnderecoId != null)
                {
                    end = await _enderecoService.GetById((int)endereco.EnderecoId);
                    
                }
                else
                {
                    end = await _enderecoService.Create(endereco);
                }
                if (end != null)
                {
                    vendedorAtualizado.Enderecos.Add(end);
                }
                else
                {
                    throw new EntityException("Erro ao atualizar vendedor, dados inválidos, endereço não encontrado com id " + endereco.EnderecoId);
                }
            }

            var updated = await _repository.Update(id, vendedorDto.ToEntity());
            return updated;
        }
        
        public async Task<List<Salario>> GetSalarioMesAno(int id, int mes, int ano)
        {            
            return await _repository.GetSalarioMesAno( id, mes, ano);            
        }

        public async Task<List<List<Salario>>> GetSalarioVendedores()
        {
            return await _repository.GetSalarioVendedores();
        }
    }  

}
