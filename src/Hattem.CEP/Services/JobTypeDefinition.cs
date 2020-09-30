using System;
using System.Linq;
using System.Reflection;
using Hattem.CEP.Extensions;
using Hattem.CEP.Helpers;
using Hattem.CEP.Jobs;

namespace Hattem.CEP.Services
{
    public readonly struct JobTypeDefinition
    {
        public Type DataType { get; }

        public Type JobType { get; }

        public bool IsPersistent { get; }

        public bool IsProgressive { get; }

        public JobTypeDefinition(Type jobType)
        {
            JobType = jobType ?? throw new ArgumentNullException(nameof(jobType));
            IsPersistent = typeof(IPersistentJob<>).IsAssignableFromGenericInterface(jobType);
            IsProgressive = typeof(IProgressiveJob).IsAssignableFromGenericInterface(jobType);

            DataType = jobType
                .GetTypeInfo()
                .ImplementedInterfaces
                .Where(v => v.IsGenericType)
                .First(v => v.GetGenericTypeDefinition() == typeof(IJob<>))
                .GetGenericArguments()[0];
        }

        public override string ToString()
        {
            return $"Job: [{FriendlyTypeNameHelper.GetFriendlyName(JobType)}] Data: [{FriendlyTypeNameHelper.GetFriendlyName(DataType)}] Persistent: [{IsPersistent}] Progressive: [{IsProgressive}]";
        }
    }
}
