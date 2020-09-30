using System;
using Hattem.CEP.Jobs.Pipeline;
using Hattem.CQRS.Containers;
using Hattem.CQRS.Extensions;

namespace Hattem.CEP.Jobs
{
    public interface IJobExecutionPipelineBuilder
    {
        IJobExecutionPipelineBuilder Use<TPipelineStep>()
            where TPipelineStep : class, IJobPipelineStep;
    }

    internal sealed class JobExecutionPipelineBuilder : IJobExecutionPipelineBuilder
    {
        private readonly IContainerConfigurator _container;
        private readonly IPipelineStepCoordinator<IJobPipelineStep> _stepCoordinator;

        public JobExecutionPipelineBuilder(IContainerConfigurator container)
        {
            _container = container ?? throw new ArgumentNullException(nameof(container));
            _stepCoordinator = new PipelineStepCoordinator<IJobPipelineStep>();
        }

        public IJobExecutionPipelineBuilder Use<TPipelineStep>()
            where TPipelineStep : class, IJobPipelineStep
        {
            _stepCoordinator.Add<TPipelineStep>();
            _container.AddSingleton<IJobPipelineStep, TPipelineStep>();

            return this;
        }

        public void Build()
        {
            _container.AddSingletonInstance(typeof(IPipelineStepCoordinator<IJobPipelineStep>), _stepCoordinator);
        }
    }
}
