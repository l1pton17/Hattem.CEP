using System;
using System.Threading.Tasks;
using Hattem.Api;

namespace Hattem.CEP.Transports
{
    public interface ICEPTransportMessageHandler
    {
        Task<ApiResponse<CEPTransportExecutionResult>> Handle(CEPTransportContext context, ReadOnlyMemory<byte> source);
    }
}
