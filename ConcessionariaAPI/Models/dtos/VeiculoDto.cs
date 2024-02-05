using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ConcessionariaAPI.Models.dtos
{
    public class VeiculoDto : IDto<Veiculo>
    {
        public int VeiculoId { get; set; }
        [StringLength(17)]
        public string NumeroChassi { get; set; }
        public decimal Valor { get; set; }
        public string? Modelo { get; set; }
        public int Quilometragem { get; set; }
        [StringLength(30)]
        public string VersaoSistema { get; set; }
        public int? ProprietarioId { get; set; }
        public ICollection<AcessorioDto> acessorios { get; set; }

        public Veiculo ToEntity()
        {
            return new Veiculo
            {
                VeiculoId = VeiculoId,
                NumeroChassi = NumeroChassi,
                Valor = Valor,
                Modelo = Modelo,
                Quilometragem = Quilometragem,
                VersaoSistema = VersaoSistema,
                ProprietarioId = ProprietarioId            };
        }
    }
}
