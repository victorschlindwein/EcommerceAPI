using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModuloAPI.Context;
using ModuloAPI.Controllers;
using ModuloAPI.Entities;
using Moq;
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
        public void GetById_ReturnsOkResult_WhenContactExists()
        {
            // Arrange
            _context.Add(CreateSampleContato(1, "Nome Teste"));
            _context.SaveChanges();

            // Act
            var result = _controller.GetById(1);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void GetById_ReturnsNoContent_WhenContactDoesNotExist()
        {
            // Arrange

            // Act
            var result = _controller.GetById(0);

            // Assert
            var noContentResult = Assert.IsType<NoContentResult>(result);
            Assert.Equal(204, noContentResult.StatusCode);
        }

        [Fact]
        public void GetByName_ReturnsOkResult_WhenFindName()
        {
            // Arrange
            _context.Add(CreateSampleContato(1, "Nome Teste"));
            _context.SaveChanges();
            // Act
            var result = _controller.GetByName("Nome Teste");
            // Assert           
            Assert.IsType<OkObjectResult>(result);
        }
    }
}