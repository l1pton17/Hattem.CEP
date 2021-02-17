using System;
using System.Threading;
using System.Threading.Tasks;
using Hattem.CQRS.Containers;

namespace Hattem.CEP.Transports
{
    internal sealed class TransportLifetimeService : ILifetimeService
    {
        private readonly ICEPTransport _transport;

        public TransportLifetimeService(ICEPTransport transport)
        {
            _transport = transport ?? throw new ArgumentNullException(nameof(transport));
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await _transport.InitializeAsync();
            throw new System.NotImplementedException();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
