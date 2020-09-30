using System.Collections.Generic;
using System.Collections.Immutable;
using System.Reflection;

namespace Hattem.CEP.Services
{
    internal interface IAssembliesProvider
    {
        ImmutableArray<Assembly> Provide();
    }

    internal sealed class AssembliesProvider : IAssembliesProvider
    {
        private readonly ImmutableArray<Assembly> _assemblies;

        public AssembliesProvider(ImmutableArray<Assembly> assemblies)
        {
            _assemblies = assemblies;
        }

        public ImmutableArray<Assembly> Provide()
        {
            return _assemblies;
        }
    }
}
