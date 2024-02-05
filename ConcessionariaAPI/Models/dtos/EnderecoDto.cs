using System.ComponentModel.DataAnnotations;

namespace ConcessionariaAPI.Models.dtos
{
    public class EnderecoDto : IDto<Endereco>
    {
        public int? EnderecoId { get; set; }
        [StringLength(60)]
        public string Rua { get; set; }
        public int Numero { get; set; }
        [StringLength(60)]
        public string Bairro { get; set; }
        [StringLength(60)]
        public string Cidade { get; set; }

        public Endereco ToEntity()
        {
            return new Endereco
            {
                EnderecoId = EnderecoId,
                Numero = Numero,
                Bairro = Bairro,
                Cidade = Cidade,
                Rua = Rua
            };
        }
    }
}
