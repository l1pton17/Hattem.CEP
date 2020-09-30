using System.Threading.Tasks;
using Hattem.Api;

namespace Hattem.CEP.Jobs.Executors
{
    public interface IJobExecutor<in TJob>
        where TJob : class, IJob
    {
        Task<ApiResponse<Unit>> Execute(ICEPContext context, TJob job);
    }
}
