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
            if(vendedorDto.VendedorId != null){
                throw new EntityException("ID não deve ser informado!");
            }           
            
            if(vendedorDto.Nome.Length == 0 || vendedorDto.Nome == "" || vendedorDto.Nome.Length > 60){
                throw new EntityException("O nome do vendedor deve ser informado e deve possuir no máximo 60 carácteres!");
            }

            if(vendedorDto.Email.Length == 0 || vendedorDto.Email == "" || vendedorDto.Email.Length > 60){
                throw new EntityException("O email do vendedor deve ser informado e deve possuir no máximo 60 carácteres!");
            }            

            if(vendedorDto.SalarioBase <= 0){
                throw new EntityException("O salário do vendedor deve ser superior a 0!");
            }

            if(vendedorDto.DataNascimento != null && (DateTime.Now.Year - vendedorDto.DataNascimento.Year) < 18){
                throw new EntityException("Vendedor deve ser maior de idade!");
            }            

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
            if(id != vendedorDto.VendedorId){
                throw new EntityException("IDs informados não coincidem!");
            }          
            
            if(vendedorDto.Nome.Length == 0 || vendedorDto.Nome == "" || vendedorDto.Nome.Length > 60){
                throw new EntityException("O nome do vendedor deve ser informado e deve possuir no máximo 60 carácteres!");
            }

            if(vendedorDto.Email.Length == 0 || vendedorDto.Email == "" || vendedorDto.Email.Length > 60){
                throw new EntityException("O email do vendedor deve ser informado e deve possuir no máximo 60 carácteres!");
            }            

            if(vendedorDto.SalarioBase <= 0){
                throw new EntityException("O salário do vendedor deve ser superior a 0!");
            }

            if(vendedorDto.DataNascimento != null && (DateTime.Now.Year - vendedorDto.DataNascimento.Year) < 18){
                throw new EntityException("Vendedor deve ser maior de idade!");
            }

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
