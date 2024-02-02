using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConcessionariaAPI.Models
{
    public class Veiculo
    {
        [Key]
        public int VeiculoId { get; set; }
        [StringLength(17)]
        public string NumeroChassi { get; set; }
        public decimal Valor { get; set; }
        public string? Modelo { get; set; }
        public int Quilometragem { get; set; }
        [StringLength(30)]
        public string VersaoSistema { get; set; }

        [ForeignKey("ProprietarioId")]
        public int? ProprietarioId { get; set; }
        public Proprietario? Proprietario { get; set; }

        public ICollection<Acessorio>? Acessorios { get; set; } = new List<Acessorio>();
    }
}
