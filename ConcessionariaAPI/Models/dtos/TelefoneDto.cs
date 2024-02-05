using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ConcessionariaAPI.Models.dtos
{
    public class TelefoneDto : IDto<Telefone>
    {
        public int? TelefoneId { get; set; }

        public char Tipo { get; set; }
        [StringLength(14)]
        public string NumeroTelefone { get; set; }

        public Telefone ToEntity()
        {
            return new Telefone
            {
                TelefoneId = TelefoneId,
                Tipo = Tipo,
                NumeroTelefone = NumeroTelefone
            };
        }
    }
}
