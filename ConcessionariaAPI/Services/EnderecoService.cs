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

            if(endereco.Rua.Length == 0 || endereco.Rua == "" || endereco.Rua.Length > 60){
                throw new EntityException("A rua do endereço deve ser informada e deve posuir no máximo 60 carácteres!");
            }

            if(endereco.Numero <= 0){
                throw new EntityException("O número do complemento deve ser informado e deve ser superior a 0!");
            }

            if(endereco.Bairro.Length == 0 || endereco.Bairro == "" || endereco.Bairro.Length > 60){
                throw new EntityException("O bairro do endereço deve ser informado e deve posuir no máximo 60 carácteres!");
            }

            if(endereco.Cidade.Length == 0 || endereco.Cidade == "" || endereco.Cidade.Length > 60){
                throw new EntityException("A cidade do endereço deve ser informada e deve posuir no máximo 60 carácteres!");
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

            if(updatedEndereco.Rua.Length == 0 || updatedEndereco.Rua == "" || updatedEndereco.Rua.Length > 60){
                throw new EntityException("A rua do endereço deve ser informada e deve posuir no máximo 60 carácteres!");
            }

            if(updatedEndereco.Numero <= 0){
                throw new EntityException("O número do complemento deve ser informado e deve ser superior a 0!");
            }

            if(updatedEndereco.Bairro.Length == 0 || updatedEndereco.Bairro == "" || updatedEndereco.Bairro.Length > 60){
                throw new EntityException("O bairro do endereço deve ser informado e deve posuir no máximo 60 carácteres!");
            }

            if(updatedEndereco.Cidade.Length == 0 || updatedEndereco.Cidade == "" || updatedEndereco.Cidade.Length > 60){
                throw new EntityException("A cidade do endereço deve ser informada e deve posuir no máximo 60 carácteres!");
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
