using System.Threading.Tasks;
using Hattem.Api;

namespace Hattem.CEP.Transports
{
    public interface ICEPTransport
    {
        Task InitializeAsync();

        void RegisterHandler(string payloadType, ICEPTransportMessageHandler handler);

        Task<ApiResponse<Unit>> Execute(ICEPTransportContext transportContext, CEPTransportExecutionResult executionResult);
    }
}
