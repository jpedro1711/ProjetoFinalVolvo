using System.ComponentModel.DataAnnotations;

namespace ConcessionariaAPI.Models.dtos
{
    public class ProprietarioDto :IDto<Proprietario>
    {
        public int? ProprietarioId { get; set; }
        [StringLength(60)]
        public string Nome { get; set; }
        [StringLength(60)]
        public string Email { get; set; }
        [StringLength(11)]
        public string? CPF { get; set; }
        [StringLength(14)]
        public string? CNPJ { get; set; }
        public DateTime? DataNascimento { get; set; }

        public ICollection<EnderecoDto>? Enderecos { get; set; } = new List<EnderecoDto>();
        public ICollection<TelefoneDto>? Telefones { get; set; } = new List<TelefoneDto>();

        public Proprietario ToEntity()
        {
            return new Proprietario
            {
                ProprietarioId = ProprietarioId,
                Nome = Nome,
                Email = Email,
                CPF = CPF,
                CNPJ = CNPJ,
                DataNascimento = DataNascimento
            };
        }
    }
}
