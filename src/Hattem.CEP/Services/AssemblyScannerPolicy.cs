using System.Collections.Generic;
using System.Reflection;

namespace Hattem.CEP.Services
{
    public interface IAssemblyScannerPolicy
    {
        bool IsAssemblyAllowed(string assemblyFullName);

        void AddExcludedAssembly(string assemblyFullName);
    }

    internal sealed class AssemblyScannerPolicy : IAssemblyScannerPolicy
    {
        private readonly HashSet<string> _excludedAssemblies = new HashSet<string>();

        public bool IsAssemblyAllowed(string assemblyFullName)
        {
            return !_excludedAssemblies.Contains(assemblyFullName.Split(',')[0]);
        }

        public void AddExcludedAssembly(string assemblyFullName)
        {
            _excludedAssemblies.Add(assemblyFullName);
        }
    }
}
