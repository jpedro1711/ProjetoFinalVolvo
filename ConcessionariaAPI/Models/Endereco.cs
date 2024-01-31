using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ConcessionariaAPI.Models
{
    public class Endereco
    {
        [Key]
        public int? EnderecoId { get; set; }
        [StringLength(60)]
        public string Rua { get; set; }
        public int Numero { get; set; }
        [StringLength(60)]
        public string Bairro { get; set; }
        [StringLength(60)]
        public string Cidade { get; set; }
        [JsonIgnore]
        public ICollection<Proprietario>? Proprietarios { get; set; } = new List<Proprietario>();
        [JsonIgnore]
        public ICollection<Vendedor>? Vendedores { get; set; } = new List<Vendedor>();
    }
}
