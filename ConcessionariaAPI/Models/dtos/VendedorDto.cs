using System.ComponentModel.DataAnnotations;

namespace ConcessionariaAPI.Models.dtos
{
    public class VendedorDto : IDto<Vendedor>
    {
        public int? VendedorId { get; set; }
        [StringLength(60)]
        public string Nome { get; set; }
        [StringLength(60)]
        public string Email { get; set; }
        public decimal SalarioBase { get; set; }
        public DateTime DataNascimento { get; set; }
        public DateTime DataAdmissao { get; set; }
        public ICollection<EnderecoDto>? Enderecos { get; set; } = new List<EnderecoDto>();
        public ICollection<TelefoneDto>? Telefones { get; set; } = new List<TelefoneDto>();

        public Vendedor ToEntity()
        {
            return new Vendedor
            {
                VendedorId = VendedorId,
                Nome = Nome,
                Email = Email,
                SalarioBase = SalarioBase,
                DataNascimento = DataNascimento,
                DataAdmissao = DataAdmissao
            };
        }
    }
}