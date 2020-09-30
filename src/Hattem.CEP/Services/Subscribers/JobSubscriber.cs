using System;
using System.Collections.Immutable;
using System.Threading.Tasks;
using Hattem.Api;
using Hattem.CEP.Transports;

namespace Hattem.CEP.Services.Subscribers
{
    internal sealed class JobSubscriber : IEventSubscriber
    {
        private readonly IJobScanner _jobScanner;

        public JobSubscriber(IJobScanner jobScanner)
        {
            _jobScanner = jobScanner ?? throw new ArgumentNullException(nameof(jobScanner));
        }

        public ImmutableArray<QueueBinding> GetBindings()
        {
            throw new System.NotImplementedException();
        }

        public Task<ApiResponse<Unit>> RegisterHandlers(ICEPTransport transport)
        {
            throw new System.NotImplementedException();
        }
    }
}
