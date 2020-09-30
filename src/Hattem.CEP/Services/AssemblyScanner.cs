using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Hattem.CEP.Services
{
    public interface IAssemblyScanner
    {
        IEnumerable<Assembly> Scan();
    }

    internal sealed class AssemblyScanner : IAssemblyScanner
    {
        private readonly IAssembliesProvider _assembliesProvider;
        private readonly IAssemblyScannerPolicy _scannerPolicy;

        public AssemblyScanner(
            IAssemblyScannerPolicy scannerPolicy,
            IAssembliesProvider assembliesProvider
        )
        {
            _scannerPolicy = scannerPolicy ?? throw new ArgumentNullException(nameof(scannerPolicy));
            _assembliesProvider = assembliesProvider ?? throw new ArgumentNullException(nameof(assembliesProvider));
        }

        public IEnumerable<Assembly> Scan()
        {
            return _assembliesProvider
                .Provide()
                .Where(a => _scannerPolicy.IsAssemblyAllowed(a.FullName));
        }
    }
}
