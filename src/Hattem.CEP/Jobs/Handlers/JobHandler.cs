using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading.Tasks;
using Hattem.Api;
using Hattem.CEP.Jobs.Pipeline;
using Hattem.CEP.Serializers;
using Hattem.CEP.Transports;

namespace Hattem.CEP.Jobs.Handlers
{
    internal sealed class JobHandler<TJob> : ICEPTransportMessageHandler
        where TJob : class, IJob
    {
        private readonly ImmutableArray<IJobPipelineStep> _steps;
        private readonly ICEPSerializer _serializer;

        public JobHandler(
            IEnumerable<IJobPipelineStep> steps,
            IPipelineStepCoordinator<IJobPipelineStep> stepCoordinator,
            ICEPSerializer serializer
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

            _serializer = serializer ?? throw new ArgumentNullException(nameof(serializer));

            _steps = stepCoordinator.Build(steps);
        }

        public Task<ApiResponse<CEPTransportExecutionResult>> Handle(
            CEPTransportContext context,
            ReadOnlyMemory<byte> source
        )
        {
            return _serializer
                .Deserialize<TJob>(source);
        }
    }
}
