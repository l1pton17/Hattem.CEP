using System;
using System.Threading.Tasks;
using Hattem.Api;
using Hattem.CEP.Transports;

namespace Hattem.CEP.Services
{
    internal interface ICEPTransportExecutionResultExecutor
    {
        Task<ApiResponse<Unit>> Execute(ICEPTransportContext transportContext, CEPTransportExecutionResult executionResult);
    }

    internal sealed class CEPTransportExecutionResultExecutor : ICEPTransportExecutionResultExecutor
    {
        private readonly ICEPTransport _transport;

        public CEPTransportExecutionResultExecutor(ICEPTransport transport)
        {
            _transport = transport ?? throw new ArgumentNullException(nameof(transport));
        }

        public Task<ApiResponse<Unit>> Execute(ICEPTransportContext transportContext, CEPTransportExecutionResult executionResult)
        {
            return _transport.Execute(transportContext, executionResult);
        }
    }
}
