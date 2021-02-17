using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using Hattem.Api;
using Hattem.CEP.Transports;

namespace Hattem.CEP.Jobs.Pipeline
{
    internal interface IJobPipelineExecutor
    {
        Task<ApiResponse<Unit>> Execute<TJob>(
            ICEPTransportContext transportContext,
            ICEPContext cepContext,
            JobPipelineStepContext<TJob> jobContext
        )
            where TJob : class, IJob;
    }

    internal sealed class JobPipelineExecutor : IJobPipelineExecutor
    {
        private readonly ImmutableArray<IJobPipelineStep> _steps;

        public JobPipelineExecutor(
            IEnumerable<IJobPipelineStep> steps,
            IPipelineStepCoordinator<IJobPipelineStep> stepCoordinator
        )
        {
            if (steps == null)
            {
                throw new ArgumentNullException(nameof(steps));
            }

            if (stepCoordinator == null)
            {
                throw new ArgumentNullException(nameof(stepCoordinator));
            }

            _steps = stepCoordinator.Build(steps);
        }

        public Task<ApiResponse<Unit>> Execute<TJob>(
            ICEPTransportContext transportContext,
            ICEPContext cepContext,
            JobPipelineStepContext<TJob> jobContext
        )
            where TJob : class, IJob
        {
            ExecuteCache<TJob>.EnsureInitialized(_steps);

            return ExecuteCache<TJob>.Pipeline!(transportContext, cepContext, jobContext);
        }

        private static class ExecuteCache<TJob>
            where TJob : class, IJob
        {
            public static Func<ICEPTransportContext, ICEPContext, JobPipelineStepContext<TJob>, Task<ApiResponse<Unit>>>? Pipeline { get; private set; }

            public static void EnsureInitialized(in ImmutableArray<IJobPipelineStep> steps)
            {
                if (Pipeline != null)
                {
                    return;
                }

                Func<ICEPTransportContext, ICEPContext, JobPipelineStepContext<TJob>, Task<ApiResponse<Unit>>> pipeline = (transportContext, cepContext, jobContext)
                    => jobContext.Executor.Execute(transportContext, cepContext, jobContext.Job);

                foreach (var step in steps.Reverse())
                {
                    var pipelineLocal = pipeline;

                    pipeline = (transportContext, cepContext, jobContext) => step.Execute(pipelineLocal, transportContext, cepContext, jobContext);
                }

                Pipeline ??= pipeline;
            }
        }
    }
}
