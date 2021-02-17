using System;
using System.Threading.Tasks;
using Hattem.Api;
using Hattem.CEP.Transports;

namespace Hattem.CEP.Jobs.Pipeline
{
    public interface IJobPipelineStep
    {
        Task<ApiResponse<Unit>> Execute<TJob>(
            Func<ICEPTransportContext, ICEPContext, JobPipelineStepContext<TJob>, Task<ApiResponse<Unit>>> next,
            ICEPTransportContext transportContext,
            ICEPContext cepContext,
            JobPipelineStepContext<TJob> jobContext
        )
            where TJob : class, IJob;
    }
}
