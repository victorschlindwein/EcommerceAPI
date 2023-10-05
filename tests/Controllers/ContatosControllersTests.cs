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

        private Contato CreateSampleContato(int id, string nome)
        {
            return new Contato { Id = id, Nome = nome };
        }


        [Fact]
        public async Task GetById_ReturnsOkResult_WhenContactExists()
        {
            // Arrange
            _context.Add(CreateSampleContato(1, "Nome Teste"));
            _context.SaveChanges();

            // Act
            var result = await _controller.GetById(1);

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
            _context.Add(CreateSampleContato(1, "Nome Teste"));
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
            _context.Add(CreateSampleContato(1, "Senhor teste"));
            _context.Add(CreateSampleContato(2, "Senhor teste 2"));
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
            var novoContato = CreateSampleContato(1, "Contato teste");

            // Act
            var result = await _controller.Create(novoContato);
            
            // Assert
            Assert.IsType<CreatedAtActionResult>(result);
        }

        [Fact]
       public async Task EditExistentContact_ReturnsEditedContact_WhenOn()
        {
            // Arrange
            int contatoId = 1;
            var contatoInicial = CreateSampleContato(contatoId, "Contato Teste");
            _context.Add(contatoInicial);
            _context.SaveChanges();

            var contatoNomeEditado = CreateSampleContato(contatoId, "Sample Contact");

            // Act
            var result = await _controller.Update(contatoId, contatoNomeEditado);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var contatoEditadoBanco = Assert.IsType<Contato>(okResult.Value);
            
            Assert.Equal(contatoId, contatoEditadoBanco.Id);

            Assert.Equal("Sample Contact", contatoEditadoBanco.Nome);
        }

        [Fact]
        public async Task DeleteExistentContact_ReturnsOk_WhenDeleted()
        {
            // Arrange
            int contatoId = 1;
            var contatoInicial = CreateSampleContato(contatoId, "Contato Teste");
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