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
