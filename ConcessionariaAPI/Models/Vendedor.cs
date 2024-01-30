using System.ComponentModel.DataAnnotations;

namespace ConcessionariaAPI.Models
{
    public class Vendedor
    {
        [Key]
        public int VendedorId { get; set; }
        [StringLength(60)]
        public string Nome { get; set; }
        [StringLength(60)]
        public string Email { get; set; }
        public decimal SalarioBase { get; set; }
        public DateTime DataNascimento { get; set; }
        public DateTime DataAdmissao { get; set; }

        public ICollection<Endereco> Enderecos { get; set; } = new List<Endereco>();
        public ICollection<Telefone> Telefones { get; set; } = new List<Telefone>();
    }
}
