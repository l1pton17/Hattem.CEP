using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using Hattem.Api;

namespace Hattem.CEP.Jobs.Pipeline
{
    internal interface IJobPipelineExecutor
    {
        Task<ApiResponse<Unit>> Execute<TJob>(ICEPContext cepContext, JobPipelineStepContext<TJob> jobContext)
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

        public Task<ApiResponse<Unit>> Execute<TJob>(ICEPContext cepContext, JobPipelineStepContext<TJob> jobContext)
            where TJob : class, IJob
        {
            ExecuteCache<TJob>.EnsureInitialized(_steps);

            return ExecuteCache<TJob>.Pipeline!(cepContext, jobContext);
        }

        private static class ExecuteCache<TJob>
            where TJob : class, IJob
        {
            public static Func<ICEPContext, JobPipelineStepContext<TJob>, Task<ApiResponse<Unit>>>? Pipeline { get; private set; }

            public static void EnsureInitialized(in ImmutableArray<IJobPipelineStep> steps)
            {
                if (Pipeline != null)
                {
                    return;
                }

                Func<ICEPContext, JobPipelineStepContext<TJob>, Task<ApiResponse<Unit>>> pipeline = (cepContext, jobContext)
                    => jobContext.Executor.Execute(cepContext, jobContext.Job);

                foreach (var step in steps.Reverse())
                {
                    var pipelineLocal = pipeline;

                    pipeline = (cepContext, jobContext) => step.Execute(pipelineLocal, cepContext, jobContext);
                }

                Pipeline ??= pipeline;
            }
        }
    }
}
