using System.ComponentModel.DataAnnotations;

namespace ConcessionariaAPI.Models
{
    public class Acessorio
    {
        [Key]
        public int AcessorioID { get; set; }

        [StringLength(50)]
        public string? Descricao { get; set; }

        public ICollection<Veiculo> Veiculos { get; set; } = new List<Veiculo>();
    }
}
