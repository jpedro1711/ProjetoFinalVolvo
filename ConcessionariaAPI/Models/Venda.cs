using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConcessionariaAPI.Models
{
    public class Venda
    {
        [Key]
        public int? VendaId { get; set; }
        public DateTime DataVenda { get; set; } = DateTime.Now;
        [ForeignKey("VeiculoId")]
        public int VeiculoId { get; set; }
        public Veiculo? Veiculo { get; set; }

        [ForeignKey("VendedorId")]
        public int VendedorId { get; set; }
        public Vendedor? Vendedor { get; set; }        
    }
}
