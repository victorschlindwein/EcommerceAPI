using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModuloAPI.Context;
using ModuloAPI.Controllers;
using ModuloAPI.Entities;
using Xunit;

namespace ModuloAPI.Tests
{
    public class ContatoControllerTests : IDisposable
    {
        private readonly AgendaContext _context;
        private readonly ContatoController _controller;

        public ContatoControllerTests()
        {
            var options = new DbContextOptionsBuilder<AgendaContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new AgendaContext(options);
            _controller = new ContatoController(_context);
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        private Contato CreateSampleContato(string nome)
        {
            return new Contato { Nome = nome };
        }

        private Contato CreateFullContato(int id, string nome, string telefone, string email)
        {
            return new Contato {ContatoId = id, Nome = nome, Telefone = telefone, Email = email, DataDeCriacao = DateTime.Now };
        }


        [Fact]
        public async Task GetById_ReturnsOkResult_WhenContactExists()
        {
            // Arrange
            int idContato = 1;
            _context.Add(CreateSampleContato("Nome Teste"));
            _context.SaveChanges();

            // Act
            var result = await _controller.GetById(idContato);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetById_ReturnsNoContent_WhenContactDoesNotExist()
        {
            // Arrange

            // Act
            var result = await _controller.GetById(0);

            // Assert
            var noContentResult = Assert.IsType<NoContentResult>(result);
            Assert.Equal(204, noContentResult.StatusCode);
        }

        [Fact]
        public async Task GetByName_ReturnsOkResult_WhenFindName()
        {
            // Arrange
            _context.Add(CreateSampleContato("Nome Teste"));
            _context.SaveChanges();
            // Act
            var result = await _controller.GetByName("Nome Teste");
            // Assert           
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetAllContacts_ReturnsOkAndConctactsList_WhenFind()
        {
            // Arrange
            _context.Add(CreateSampleContato("Senhor teste"));
            _context.Add(CreateSampleContato("Senhor teste 2"));
            _context.SaveChanges();
            // Act
            var result = await _controller.GetAllContacts();
            
            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var contatos = Assert.IsAssignableFrom<List<Contato>>(okResult.Value);
            Assert.Equal(2, contatos.Count());
        }

        [Fact]
        public async Task CreateNewConctact_ReturnsContact_WhenOk()
        {
            // Arrange
            var novoContato = CreateSampleContato("Contato teste");

            // Act
            var result = await _controller.Create(novoContato);
            
            // Assert
            Assert.IsType<CreatedAtActionResult>(result);
        }

        [Fact]
       public async Task EditExistentContact_ReturnsEditedContact_WhenOk()
        {
            // Arrange
            int contatoId = 1;
            var contatoInicial = CreateSampleContato("Contato Teste");
            _context.Add(contatoInicial);
            _context.SaveChanges();

            var contatoNomeEditado = CreateSampleContato("Sample Contact");

            // Act
            var result = await _controller.Update(contatoId, contatoNomeEditado);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var contatoEditadoBanco = Assert.IsType<Contato>(okResult.Value);
            
            Assert.Equal(contatoId, contatoEditadoBanco.ContatoId);

            Assert.Equal("Sample Contact", contatoEditadoBanco.Nome);
       }

        [Fact]
        public async Task EditExistentFullContact_ReturnsEditedContact_WhenOk()
        {
            // Arrange
            int contatoId = 1;
            var contatoInicialSimples = CreateSampleContato("Contato Teste");
            _context.Add(contatoInicialSimples);
            _context.SaveChanges();

            Contato contatoEditado = CreateFullContato(contatoId, "Cadastro Full", "9999999999", "email@teste.com.br");

            // Act
            var result = await _controller.Update(contatoId, contatoEditado);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var contatoEditadoBanco = Assert.IsType<Contato>(okResult.Value);

            Assert.Equal(contatoEditadoBanco.ContatoId, contatoEditado.ContatoId);
            Assert.Equal(contatoEditadoBanco.Nome, contatoEditado.Nome);
            Assert.Equal(contatoEditadoBanco.Telefone, contatoEditado.Telefone);
            Assert.Equal(contatoEditadoBanco.Email, contatoEditado.Email);
        }

        [Fact]
        public async Task DeleteExistentContact_ReturnsOk_WhenDeleted()
        {
            // Arrange
            int contatoId = 1;
            var contatoInicial = CreateSampleContato("Contato Teste");
            _context.Add(contatoInicial);
            _context.SaveChanges();

            // Act
            var result = await _controller.Delete(contatoId);

            // Assert
            Assert.IsType<OkObjectResult>(result);
            var emptyList = _context.Contatos;
            Assert.Empty(emptyList);
        }
    }
}