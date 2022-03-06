namespace HappyCoding.NServiceBusWithSqlServer.Common
{
    public static class Parameters
    {
        public const string CONNECTION_STRING =
            "Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog=HapyCoding_2022_NServiceBusWithSqlServer;Integrated Security=SSPI";

        public const string ENDPOINT_SENDER = "HappyCoding.NServiceBusWithSqlServer.SenderApp";
        public const string ENDPOINT_RECEIVER = "HappyCoding.NServiceBusWithSqlServer.ReceiverApp";
    }
}
