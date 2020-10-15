namespace Hattem.CEP.Services
{
    public interface ICEPContextFactory
    {
        ICEPContext Create();
    }

    public sealed class CEPContextFactory : ICEPContextFactory
    {
        public ICEPContext Create()
        {
            throw new System.NotImplementedException();
        }
    }
}
