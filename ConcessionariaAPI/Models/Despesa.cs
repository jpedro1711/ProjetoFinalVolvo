using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ConcessionariaAPI.Models
{
    public class Despesa
    {
        [Key]
        public int? DespesaID { get; set; }

        [StringLength(50)]
        public string Descricao { get; set; }

        public decimal Valor {get; set;}

    }
}