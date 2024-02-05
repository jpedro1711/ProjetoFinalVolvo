using System.ComponentModel.DataAnnotations.Schema;

namespace ConcessionariaAPI.Models.dtos
{
    public class VendaDto : IDto<Venda>
    {
        public int? VendaId { get; set; }
        public int VeiculoId { get; set; }
        public int VendedorId { get; set; }

        public Venda ToEntity()
        {
            return new Venda
            {
                VendaId = VendaId,
                VeiculoId = VeiculoId,
                VendedorId = VendedorId
            };
        }
    }
}
