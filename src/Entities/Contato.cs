using API.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ModuloAPI.Entities
{
    public class Contato
    {
        private DateTime dataDeCriacao;
        
        [Key]
        public int ContatoId { get; set; }
        public required string Nome { get; set; }
        public string? Telefone { get; set; }
        [EmailAddress]
        public string? Email {  get; set; }
        public bool Ativo { get; set; }
        [DataType(DataType.DateTime)]
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
        public List<Endereco>? Enderecos { get; set; }
        public Contato()
        {
            Enderecos = new List<Endereco>();
        }
    }
}