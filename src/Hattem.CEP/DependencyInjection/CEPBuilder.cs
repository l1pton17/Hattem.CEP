using System;
using System.Collections.Immutable;
using System.Reflection;
using Hattem.CEP.Services;
using Hattem.CQRS.Containers;

namespace Hattem.CEP.DependencyInjection
{
    public interface ICEPBuilder
    {
        ICEPBuilder AddAssembly(Assembly assembly);
    }

    internal sealed class CEPBuilder : ICEPBuilder
    {
        private readonly IContainerConfigurator _container;
        private readonly ImmutableArray<Assembly>.Builder _assembliesBuilder;

        public CEPBuilder(IContainerConfigurator container)
        {
            _container = container ?? throw new ArgumentNullException(nameof(container));
            _assembliesBuilder = ImmutableArray.CreateBuilder<Assembly>();
        }

        public ICEPBuilder AddAssembly(Assembly assembly)
        {
            if (assembly == null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            if (assembly == null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            _assembliesBuilder.Add(assembly);

            return this;
        }

        public ICEPBuilder Done()
        {
            var assemblies = _assembliesBuilder.ToImmutable();

            _container.AddSingletonInstance(typeof(IAssembliesProvider), new AssembliesProvider(assemblies));

            return this;
        }
    }
}
