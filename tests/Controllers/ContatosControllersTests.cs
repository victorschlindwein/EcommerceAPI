using System.Collections.Generic;
using System.Linq;
using API.Entities;
using API.Tests.Fixtures;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ModuloAPI.Context;
using ModuloAPI.Controllers;
using ModuloAPI.Entities;
using Xunit;

namespace ModuloAPI.Tests
{
    public class ContatoControllerTests : IClassFixture<ContatoTestFixture>
    {
        private readonly AgendaContext _context;
        private readonly IContatoService _contatoService;

        public ContatoControllerTests(ContatoTestFixture fixture)
        {
            _context = fixture.Context;
            _contatoService = fixture.ContatoService;
        }

        private Contato CreateSampleContato(string nome)
        {
            return new Contato { Nome = nome };
        }

        private Contato CreateFullContato(int id, string nome, string telefone, string email)
        {
            return new Contato { Nome = nome, Telefone = telefone, Email = email, DataDeCriacao = DateTime.Now };
        }


        [Fact]
        public async Task GetById_ReturnsOkResult_WhenContactExists()
        {
            // Arrange
            int idContato = 1;
            _context.Add(CreateSampleContato("Nome Teste"));
            _context.SaveChanges();

            // Act
            Contato result = await _contatoService.GetById(idContato);

            // Assert
            Assert.Equal(idContato, result.ContatoId);
        }

        [Fact]
        public async Task GetById_ReturnsNoContent_WhenContactDoesNotExists()
        {
            // Arrange

            // Act
            var result = await _contatoService.GetById(-1);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetByName_ReturnsOkResult_WhenFindName()
        {
            string name = "Nome Teste";
            // Arrange
            _context.Add(CreateSampleContato(name));
            _context.SaveChanges();
            // Act
            var contatos = await _contatoService.GetByName("Nome Teste");

            // Assert
            Assert.NotNull(contatos);

        }

        [Fact]
        public async Task GetAllContacts_ReturnsOkAndConctactsList_WhenFind()
        {
            // Arrange
            _context.Add(CreateSampleContato("Senhor teste"));
            _context.Add(CreateSampleContato("Senhor teste 2"));
            _context.SaveChanges();
            // Act
            var result = await _contatoService.GetAllContacts();

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task CreateNewConctact_ReturnsContact_WhenOk()
        {
            // Arrange
            var novoContato = CreateSampleContato("Contato teste");

            // Act
            var result = await _contatoService.Create(novoContato);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task DeleteExistentContact_ReturnsOk_WhenDeleted()
        {
            // Arrange
            int contatoId = 2;
            var contatoInicial = CreateSampleContato("Contato Teste");
            var contatoInicial2 = CreateSampleContato("Contato Teste");
            _context.Add(contatoInicial2);
            _context.SaveChanges();

            // Act
            var result = await _contatoService.Delete(contatoId);

            // Assert
            Assert.True(result);
        }
    }
}