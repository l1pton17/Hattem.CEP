using System;
using Hattem.CEP.Jobs.Executors;

namespace Hattem.CEP.Jobs.Pipeline
{
    public readonly struct JobPipelineStepContext<TJob>
        where TJob : class, IJob
    {
        public TJob Job { get; }

        public IJobExecutor<TJob> Executor { get; }

        public JobPipelineStepContext(
            TJob job,
            IJobExecutor<TJob> executor
        )
        {
            Job = job ?? throw new ArgumentNullException(nameof(job));
            Executor = executor ?? throw new ArgumentNullException(nameof(executor));
        }
    }

    public static class JobPipelineStepContext
    {
        public static JobPipelineStepContext<TJob> Create<TJob>(TJob job, IJobExecutor<TJob> executor)
            where TJob : class, IJob
        {
            return new JobPipelineStepContext<TJob>(job, executor);
        }
    }
}
