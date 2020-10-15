using System;
using System.Threading.Tasks;
using Hattem.Api;
using Hattem.Api.Fluent;
using Hattem.CEP.Jobs.Executors;
using Hattem.CEP.Jobs.Pipeline;
using Hattem.CEP.Serializers;
using Hattem.CEP.Services;
using Hattem.CEP.Transports;

namespace Hattem.CEP.Jobs.Handlers
{
    internal sealed class JobHandler<TJob> : ICEPTransportMessageHandler
        where TJob : class, IJob
    {
        private readonly ICEPSerializer _serializer;
        private readonly IJobPipelineExecutor _jobPipelineExecutor;
        private readonly ICEPContextFactory _cepContextFactory;
        private readonly IJobExecutor<TJob> _jobExecutor;

        public JobHandler(
            ICEPSerializer serializer,
            IJobPipelineExecutor jobPipelineExecutor,
            ICEPContextFactory cepContextFactory,
            IJobExecutor<TJob> jobExecutor
        )
        {
            _serializer = serializer ?? throw new ArgumentNullException(nameof(serializer));
            _jobPipelineExecutor = jobPipelineExecutor ?? throw new ArgumentNullException(nameof(jobPipelineExecutor));
            _cepContextFactory = cepContextFactory ?? throw new ArgumentNullException(nameof(cepContextFactory));
            _jobExecutor = jobExecutor ?? throw new ArgumentNullException(nameof(jobExecutor));
        }

        public async Task<ApiResponse<CEPTransportExecutionResult>> Handle(
            CEPTransportContext context,
            ReadOnlyMemory<byte> source
        )
        {
            using var cepContext = _cepContextFactory.Create();

            return await _serializer
                .Deserialize<TJob>(source)
                .Then(job => _jobPipelineExecutor.Execute(cepContext, JobPipelineStepContext.Create(job, _jobExecutor)))
                .Return(CEPTransportExecutionResult.Ack())
                .Catch()
                .IfError(ErrorPredicate.Any(), _ => ApiResponse.Ok(CEPTransportExecutionResult.Requeue()));
        }
    }
}
