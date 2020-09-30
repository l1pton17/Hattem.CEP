using System.Collections.Immutable;
using System.Threading.Tasks;
using Hattem.Api;
using Hattem.CEP.Transports;

namespace Hattem.CEP.Services.Subscribers
{
    public interface IEventSubscriber
    {
        ImmutableArray<QueueBinding> GetBindings();

        Task<ApiResponse<Unit>> RegisterHandlers(ICEPTransport transport);
    }
}
