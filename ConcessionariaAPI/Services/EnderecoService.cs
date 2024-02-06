using ConcessionariaAPI.Models;
using ConcessionariaAPI.Repositories;
using Microsoft.EntityFrameworkCore;
using ConcessionariaAPI.Exceptions;
using ConcessionariaAPI.Services.interfaces;
using ConcessionariaAPI.Models.dtos;
using ConcessionariaAPI.Repositories.interfaces;

namespace ConcessionariaAPI.Services
{
    public class EnderecoService : IEnderecoService
    {
        private IRepository<Endereco> _repository;

        public EnderecoService(ConcessionariaContext context)
        {
            _repository = new EnderecoRepository(context);
        }

        public async Task<Endereco> Create(EnderecoDto endereco)
        { 
            if(endereco.EnderecoId != null){
                throw new EntityException("ID não deve ser informado!");
            }

            if(endereco.Rua.Length == 0 || endereco.Rua == ""){
                throw new EntityException("A rua do endereço deve ser informada!");
            }

            if(endereco.Numero <= 0){
                throw new EntityException("O número do complemento deve ser informado e deve ser superior a 0!");
            }

            if(endereco.Bairro.Length == 0 || endereco.Bairro == ""){
                throw new EntityException("O bairro do endereço deve ser informado!");
            }

            if(endereco.Cidade.Length == 0 || endereco.Cidade == ""){
                throw new EntityException("A cidade do endereço deve ser informada!");
            }

            var enderecoCreated = await _repository.Create(endereco.ToEntity());
            return enderecoCreated;
        }

        public async Task Delete(int id)
        {
            await _repository.Delete(id);
        }

        public async Task<List<Endereco>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<Endereco> GetById(int id)
        {
            return await _repository.GetById(id);
        }

        public async Task<Endereco> Update(int id, EnderecoDto updatedEndereco)
        {
            if(id != updatedEndereco.EnderecoId){
                throw new EntityException("IDs informados não coincidem!");
            }

            if(updatedEndereco.Rua.Length == 0 || updatedEndereco.Rua == ""){
                throw new EntityException("A rua do endereço atualizado deve ser informada!");
            }

            if(updatedEndereco.Numero <= 0){
                throw new EntityException("O número do complemento deve ser informado e deve ser superior a 0!");
            }

            if(updatedEndereco.Bairro.Length == 0 || updatedEndereco.Bairro == ""){
                throw new EntityException("O bairro do endereço atualizado deve ser informado!");
            }

            if(updatedEndereco.Cidade.Length == 0 || updatedEndereco.Cidade == ""){
                throw new EntityException("A cidade do endereço atualizado deve ser informada!");
            }

            var existingEndereco = await _repository.GetById(id);

            if (existingEndereco != null)
            {          
                var updated = await _repository.Update(id, updatedEndereco.ToEntity());
                return updated;
            }
            throw new EntityException("Endereço não encontrado", 404, "UPDATE, EnderecoService");
        }
    }
}
