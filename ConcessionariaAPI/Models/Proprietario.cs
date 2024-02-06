using System.ComponentModel.DataAnnotations;

namespace ConcessionariaAPI.Models
{
    public class Proprietario
    {
        [Key]
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

        public ICollection<Endereco>? Enderecos { get; set; } = new List<Endereco>();
        public ICollection<Telefone>? Telefones { get; set; } = new List<Telefone>();
    }
}