using System.Threading.Tasks;
using Hattem.Api;
using Hattem.CEP.Transports;

namespace Hattem.CEP.Jobs.Executors
{
    public interface IJobExecutor<in TJob>
        where TJob : class, IJob
    {
        Task<ApiResponse<Unit>> Execute(ICEPTransportContext transportContext, ICEPContext cepContext, TJob job);
    }
}
