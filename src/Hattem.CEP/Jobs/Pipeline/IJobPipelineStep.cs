using System;
using System.Threading.Tasks;
using Hattem.Api;

namespace Hattem.CEP.Jobs.Pipeline
{
    public interface IJobPipelineStep
    {
        Task<ApiResponse<Unit>> Execute<TJob>(
            Func<ICEPContext, JobPipelineStepContext<TJob>, Task<ApiResponse<Unit>>> next,
            ICEPContext cepContext,
            JobPipelineStepContext<TJob> jobContext
        )
            where TJob : class, IJob;
    }
}
