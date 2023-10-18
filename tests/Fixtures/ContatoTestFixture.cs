using API.Repositories;
using API.Services;
using Microsoft.EntityFrameworkCore;
using ModuloAPI.Context;

namespace API.Tests.Fixtures
{
    public class ContatoTestFixture : IDisposable
    {
        public AgendaContext Context { get; private set; }
        public IContatoService ContatoService { get; private set; }
        public IContatoRepository ContatoRepository { get; private set; }
        public ContatoTestFixture()
        {
            var options = new DbContextOptionsBuilder<AgendaContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            Context = new AgendaContext(options);

            ContatoRepository = new ContatoRepository(Context);
            ContatoService = new ContatoService(ContatoRepository);
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
