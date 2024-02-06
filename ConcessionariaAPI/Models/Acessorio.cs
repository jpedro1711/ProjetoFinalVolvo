using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ConcessionariaAPI.Models
{
    public class Acessorio
    {
        
        [Key]        
        public int? AcessorioID { get; set; }

        [StringLength(50)]
        public string Descricao { get; set; }
        [JsonIgnore]
        public ICollection<Veiculo>? Veiculos { get; set; } = new List<Veiculo>();
    }
}
