using System;
using System.Threading.Tasks;
using Hattem.Api;

namespace Hattem.CEP.Transports
{
    public interface ICEPTransportMessageHandler
    {
        Task<ApiResponse<Unit>> Handle(ICEPTransportContext transportContext, ReadOnlyMemory<byte> source);
    }
}
