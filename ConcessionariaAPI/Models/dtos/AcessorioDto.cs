using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ConcessionariaAPI.Models.dtos
{
    public class AcessorioDto : IDto<Acessorio>
    {
        public int? AcessorioID { get; set; }
        [StringLength(50)]
        public string? Descricao { get; set; }
        
        public Acessorio ToEntity()
        {
            return new Acessorio
            {
                AcessorioID = AcessorioID,
                Descricao = Descricao
            };
        }
    }
}
