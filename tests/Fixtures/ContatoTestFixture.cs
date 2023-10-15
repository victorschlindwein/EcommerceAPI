using API.Entities;
using API.Services;
using Microsoft.EntityFrameworkCore;
using ModuloAPI.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Tests.Fixtures
{
    public class ContatoTestFixture : IDisposable
    {
        public AgendaContext Context { get; private set; }
        public IContatoService ContatoService { get; private set; }
        public ContatoTestFixture() 
        {
            var options = new DbContextOptionsBuilder<AgendaContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            
            Context = new AgendaContext(options);
            ContatoService = new ContatoService(Context);
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
