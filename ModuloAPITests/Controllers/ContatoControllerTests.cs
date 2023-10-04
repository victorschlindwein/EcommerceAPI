using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModuloAPI.Context;
using ModuloAPI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuloAPI.Controllers.Tests
{
    [TestClass()]
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
    {
        [TestMethod()]
        public void ContatoControllerTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void GetByIdTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void GetByNameTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void GetAllContactsTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void CreateTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void UpdateTest()
        {
            throw new NotImplementedException();
        }

        [TestMethod()]
        public void DeleteTest()
        {
            throw new NotImplementedException();
        }
    }
}