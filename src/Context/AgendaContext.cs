using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;
using Microsoft.EntityFrameworkCore;
using ModuloAPI.Entities;

namespace ModuloAPI.Context
{
    public class AgendaContext : DbContext
    {
        public AgendaContext(DbContextOptions<AgendaContext> options) : base(options)
        {
        }

        public DbSet<Contato> Contatos { get; set; }

        public DbSet<Endereco> Enderecos { get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Contato>()
                .HasMany(e => e.Enderecos)
                .WithOne()
                .HasForeignKey(e => e.ContatoId);
        }
    }
}