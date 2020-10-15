using System;

namespace Hattem.CEP
{
    public interface ICEPContext : IDisposable
    {
    }

    internal sealed class CEPContext : ICEPContext
    {
        public void Dispose()
        {
        }
    }
}
