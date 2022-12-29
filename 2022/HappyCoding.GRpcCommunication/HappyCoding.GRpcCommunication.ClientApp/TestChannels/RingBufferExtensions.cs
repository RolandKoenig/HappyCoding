using System.Collections.Generic;
using RolandK.Utils.Collections;

namespace HappyCoding.GRpcCommunication.ClientApp.TestChannels;

internal static class RingBufferExtensions
{
    public static IEnumerable<T> GetFullEnumerable<T>(this RingBuffer<T>[] ringBuffers)
    {
        foreach (var actRingBuffer in ringBuffers)
        {
            foreach (var actElement in actRingBuffer)
            {
                yield return actElement;
            }
        }
    }
}
