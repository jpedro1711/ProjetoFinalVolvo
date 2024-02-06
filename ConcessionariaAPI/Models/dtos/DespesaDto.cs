using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ConcessionariaAPI.Models.dtos
{
    public class DespesaDto : IDto<Despesa>
    {        
        public int? DespesaID { get; set; }

        [StringLength(50)]
        public string Descricao { get; set; }

        public decimal Valor {get; set;}

         public Despesa ToEntity()
        {
            return new Despesa
            {
                DespesaID = DespesaID,
                Descricao = Descricao,
                Valor = Valor
            };
        }

    }
}