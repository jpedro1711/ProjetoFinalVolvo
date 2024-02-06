using ConcessionariaAPI.Models;
using ConcessionariaAPI.Repositories;
using ConcessionariaAPI.Exceptions;
using ConcessionariaAPI.Services.interfaces;
using ConcessionariaAPI.Repositories.interfaces;
using ConcessionariaAPI.Models.dtos;

namespace ConcessionariaAPI.Services
{
    public class ProprietarioService : IProprietarioService
    {
        private IRepository<Proprietario> _repository;
        private ITelefoneService _telefoneService;
        private IEnderecoService _enderecoService;

        public ProprietarioService(ConcessionariaContext context)
        {
            _repository = new ProprietarioRepository(context);
            _telefoneService = new TelefoneService(context);
            _enderecoService = new EnderecoService(context);
        }

        public async Task<Proprietario> Create(ProprietarioDto proprietarioDto)
        {                       
            if(proprietarioDto.ProprietarioId != null){
                throw new EntityException("ID não deve ser informado!");
            }

            if(proprietarioDto.Nome.Length == 0 || proprietarioDto.Nome == "" || proprietarioDto.Nome.Length > 60){
                throw new EntityException("O nome do proprietário deve ser informado e deve possuir no máximo 60 carácteres!");
            }

            if(proprietarioDto.Email.Length == 0 || proprietarioDto.Email == "" || proprietarioDto.Email.Length > 60){
                throw new EntityException("O email do proprietário deve ser informado e deve possuir no máximo 60 carácteres!");
            }            

            if(proprietarioDto.CPF.Length == 0 && proprietarioDto.CNPJ.Length == 0){
                throw new EntityException("Proprietário deve ser pessoa física ou empresa!");
            }

            if(proprietarioDto.CPF.Length != 0 && proprietarioDto.CNPJ.Length != 0){
                throw new EntityException("Proprietário não pode ser pessoa física e empresa ao mesmo tempo!");
            }

            if(proprietarioDto.CPF.Length != 0 && (proprietarioDto.CPF.Length < 11 || proprietarioDto.CPF.Length > 11)){
                throw new EntityException("CPF deve conter os 11 digitos!");
            }            

            if(proprietarioDto.CNPJ.Length != 0 && (proprietarioDto.CNPJ.Length < 14 || proprietarioDto.CNPJ.Length > 14)){
                throw new EntityException("CNPJ deve conter os 14 digitos!");
            }

            if(proprietarioDto.CNPJ.Length != 0 && proprietarioDto.DataNascimento != null){
                 throw new EntityException("Empresa não deve possuir data de nascimento!");
            }

            if(proprietarioDto.CPF.Length != 0 && proprietarioDto.DataNascimento == null){
                 throw new EntityException("Data de nascimento do proprietário deve ser informada!");
            }

            if(proprietarioDto.DataNascimento != null && (DateTime.Now.Year - proprietarioDto.DataNascimento.Value.Year) < 18){
                throw new EntityException("Proprietário deve ser maior de idade!");
            }

            Proprietario proprietario = proprietarioDto.ToEntity();

            foreach (TelefoneDto telefone in proprietarioDto.Telefones)
            {
                Telefone tel;
                if (telefone.TelefoneId != null)
                {
                    // Telefone já existe
                    tel = await _telefoneService.GetById((int)telefone.TelefoneId);
                }
                else
                {
                    tel = await _telefoneService.Create(telefone);
                }
                if (tel != null)
                {
                    proprietario.Telefones.Add(tel);
                }
                else
                {
                    throw new EntityException("Erro ao atualizar proprietário, dados inválidos, telefone não encontrado com id " +  telefone.TelefoneId);
                }
            }

            foreach (EnderecoDto endereco in proprietarioDto.Enderecos)
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
                    proprietario.Enderecos.Add(end);
                }
                else
                {
                    throw new EntityException("Erro ao atualizar proprietário, dados inválidos, endereço não encontrado com id " + end.EnderecoId);
                }
            }

            var created = await _repository.Create(proprietario);
            return created;
        }

        public async Task Delete(int id)
        {
            await _repository.Delete(id);
        }

        public async Task<List<Proprietario>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<Proprietario> GetById(int id)
        {
            return await _repository.GetById(id);
        }

        public async Task<Proprietario> Update(int id, ProprietarioDto proprietarioDto)        
        {                        
            if(id != proprietarioDto.ProprietarioId){
                throw new EntityException("IDs informados não coincidem!");
            }

            if(proprietarioDto.Nome.Length == 0 || proprietarioDto.Nome == "" || proprietarioDto.Nome.Length > 60){
                throw new EntityException("O nome do proprietário deve ser informado e deve possuir no máximo 60 carácteres!");
            }

            if(proprietarioDto.Email.Length == 0 || proprietarioDto.Email == "" || proprietarioDto.Email.Length > 60){
                throw new EntityException("O email do proprietário deve ser informado e deve possuir no máximo 60 carácteres!");
            }            

            if(proprietarioDto.CPF.Length == 0 && proprietarioDto.CNPJ.Length == 0){
                throw new EntityException("Proprietário deve ser pessoa física ou empresa!");
            }

            if(proprietarioDto.CPF.Length != 0 && proprietarioDto.CNPJ.Length != 0){
                throw new EntityException("Proprietário não pode ser pessoa física e empresa ao mesmo tempo!");
            }

            if(proprietarioDto.CPF.Length != 0 && (proprietarioDto.CPF.Length < 11 || proprietarioDto.CPF.Length > 11)){
                throw new EntityException("CPF deve conter os 11 digitos!");
            }            

            if(proprietarioDto.CNPJ.Length != 0 && (proprietarioDto.CNPJ.Length < 14 || proprietarioDto.CNPJ.Length > 14)){
                throw new EntityException("CNPJ deve conter os 14 digitos!");
            }

            if(proprietarioDto.CNPJ.Length != 0 && proprietarioDto.DataNascimento != null){
                 throw new EntityException("Empresa não deve possuir data de nascimento!");
            }

            if(proprietarioDto.CPF.Length != 0 && proprietarioDto.DataNascimento == null){
                 throw new EntityException("Data de nascimento do proprietário deve ser informada!");
            }

            if(proprietarioDto.DataNascimento != null && (DateTime.Now.Year - proprietarioDto.DataNascimento.Value.Year) < 18){
                throw new EntityException("Proprietário deve ser maior de idade!");
            }


            Proprietario proprietarioAtualizado = await _repository.GetById(id);

            if (proprietarioAtualizado == null)
            {
                throw new EntityException("Erro ao atualizar proprietário com id " + id + ", não foi encontrado ou dados inválidos");
            }

            foreach (TelefoneDto telefone in proprietarioDto.Telefones)
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
                    proprietarioAtualizado.Telefones.Add(tel);
                }
                else
                {
                    throw new EntityException("Erro ao criar proprietário, dados inválidos, telefone não encontrado com id " + telefone.TelefoneId);
                }
            }


            foreach (EnderecoDto endereco in proprietarioDto.Enderecos)
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
                    proprietarioAtualizado.Enderecos.Add(end);
                }
                else
                {
                    throw new EntityException("Erro ao criar proprietário, dados inválidos, endereço não encontrado com id " + end.EnderecoId);
                }
            }

            var updated = await _repository.Update(id, proprietarioDto.ToEntity());
            return updated;

        }
    }
}
