namespace Hattem.CEP.Transports
{
    public interface ICEPTransport
    {
        void RegisterHandler(string payloadType, ICEPTransportMessageHandler handler);
    }
}
