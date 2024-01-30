using System.ComponentModel.DataAnnotations;

namespace ConcessionariaAPI.Models
{
    public class Telefone
    {
        [Key]
        public int TelefoneId { get; set; }
        
        public char Tipo { get; set; }
        [StringLength(14)]
        public string NumeroTelefone { get; set; }

        public ICollection<Proprietario> Proprietarios { get; set; } = new List<Proprietario>();
        public ICollection<Vendedor> Vendedores { get; set; } = new List<Vendedor>();
    }
}
