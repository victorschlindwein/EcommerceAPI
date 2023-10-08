using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModuloAPI.Entities
{
    public class Contato
    {
        private DateTime dataDeCriacao;
        public int Id { get; set; }
        public required string Nome { get; set; }
        public string? Telefone { get; set; }
        public bool Ativo { get; set; }
        public DateTime DataDeCriacao
        {
            get
            {
                return dataDeCriacao;
            }
            set
            {
                dataDeCriacao = DateTime.Now;
            }
        }
    }
}