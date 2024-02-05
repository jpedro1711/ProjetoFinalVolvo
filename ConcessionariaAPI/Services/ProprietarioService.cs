using ConcessionariaAPI.Models;
using ConcessionariaAPI.Repositories;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
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
                    tel = await _telefoneService.GetById((int)telefone.TelefoneId);
                }
                else
                {
                    tel = await _telefoneService.Create(telefone);
                }
                if (tel != null) proprietario.Telefones.Add(tel);
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
                proprietario.Enderecos.Add(end);
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
                    // Se o telefone existir
                    if (tel != null)
                    {
                        await _telefoneService.Update((int)telefone.TelefoneId, telefone);
                    }
                }
                else
                {
                    var created = await _telefoneService.Create(telefone);
                    proprietarioAtualizado.Telefones.Add(created);
                }
            }


            foreach (EnderecoDto endereco in proprietarioDto.Enderecos)
            {
                Endereco end;
                if (endereco.EnderecoId != null)
                {
                    end = await _enderecoService.GetById((int)endereco.EnderecoId);
                    // Se o telefone existir
                    if (end != null)
                    {
                        await _enderecoService.Update((int)endereco.EnderecoId, endereco);                
                    }
                }
                else
                {
                    var created = await _enderecoService.Create(endereco);
                    proprietarioAtualizado.Enderecos.Add(created);
                }
            }

            var updated = await _repository.Update(id, proprietarioDto.ToEntity());
            return updated;

        }
    }
}
