using System;
using System.Collections.Immutable;
using System.Linq;
using Hattem.CEP.Extensions;
using Hattem.CEP.Jobs;

namespace Hattem.CEP.Services
{
    public interface IJobScanner
    {
        ImmutableArray<JobTypeDefinition> Scan();
    }

    internal sealed class JobScanner : IJobScanner
    {
        private readonly IAssemblyScanner _assemblyScanner;

        public JobScanner(IAssemblyScanner assemblyScanner)
        {
            _assemblyScanner = assemblyScanner ?? throw new ArgumentNullException(nameof(assemblyScanner));
        }

        public ImmutableArray<JobTypeDefinition> Scan()
        {
            return _assemblyScanner
                .Scan()
                .Where(v => !v.IsDynamic)
                .SelectMany(v => v.DefinedTypes)
                .Where(v => !v.IsAbstract)
                .Where(v => !v.IsGenericTypeDefinition)
                .Where(v => typeof(IJob).IsAssignableFromGenericInterface(v))
                .Select(v => new JobTypeDefinition(v))
                .ToImmutableArray();
        }
    }
}
