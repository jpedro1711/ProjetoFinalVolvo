using System.ComponentModel.DataAnnotations;

namespace ConcessionariaAPI.Models
{
    public class Endereco
    {
        [Key]
        public int EnderecoId { get; set; }
        [StringLength(60)]
        public string Rua { get; set; }
        public int Numero { get; set; }
        [StringLength(60)]
        public string Bairro { get; set; }
        [StringLength(60)]
        public string Cidade { get; set; }
    }
}
