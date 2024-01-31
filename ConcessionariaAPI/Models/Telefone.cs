using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ConcessionariaAPI.Models
{
    public class Telefone
    {
        [Key]
        public int? TelefoneId { get; set; }
        
        public char Tipo { get; set; }
        [StringLength(14)]
        public string NumeroTelefone { get; set; }
        [JsonIgnore]
        public ICollection<Proprietario>? Proprietarios { get; set; } = new List<Proprietario>();
        [JsonIgnore]
        public ICollection<Vendedor>? Vendedores { get; set; } = new List<Vendedor>();
    }
}
