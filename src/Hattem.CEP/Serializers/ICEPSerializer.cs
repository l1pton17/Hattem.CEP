using System;
using Hattem.Api;

namespace Hattem.CEP.Serializers
{
    public interface ICEPSerializer
    {
        ApiResponse<T> Deserialize<T>(ReadOnlyMemory<byte> source);

        ApiResponse<string> Serialize<T>(T source);
    }
}
