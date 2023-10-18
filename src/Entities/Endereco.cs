using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    public class Endereco
    {
        [Key]
        public int EnderecoId { get; set; }
        public string ApelidoEndereco { get; set; } = string.Empty;
        public string Logradouro { get; set; } = string.Empty;
        public bool Ativo { get; set; }
        public bool EnderecoPrincipal { get; set; }
        [ForeignKey("ContatoId")]
        public int ContatoId { get; set; }
    }
}
