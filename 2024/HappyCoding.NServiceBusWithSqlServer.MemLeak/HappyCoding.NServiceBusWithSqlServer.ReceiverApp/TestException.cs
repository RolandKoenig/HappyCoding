using System;

namespace HappyCoding.NServiceBusWithSqlServer.ReceiverApp
{
    public class TestException : Exception
    {
        private byte[] _bytes = new byte[1024 * 1024 * 4];

        public void Reset()
        {
            _bytes = [];
        }
    }
}