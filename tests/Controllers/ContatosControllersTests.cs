using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using DotNet.Testcontainers.Builders;
using ModuloAPI.Context;
using ModuloAPI.Controllers;
using ModuloAPI.Entities;
using Moq;

namespace ModuloAPI.Controllers.Tests.Controllers
{
    public class ContatosControllersTests
    {
        private ContatoController contatoController;

        public ContatosControllersTests()
        {
            contatoController = new ContatoController(new Mock<AgendaContext>().Object);
        }

        [Fact]
        public void Post_Create()
        {
            Assert.True(true);
        }
    }
}