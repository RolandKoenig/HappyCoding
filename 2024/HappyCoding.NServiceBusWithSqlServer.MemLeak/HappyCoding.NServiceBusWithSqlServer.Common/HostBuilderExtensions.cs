using System.Collections.Concurrent;
using System.Data.SqlClient;
using HappyCoding.NServiceBusWithSqlServer.Common.Util;
using Microsoft.Extensions.Hosting;

namespace HappyCoding.NServiceBusWithSqlServer.Common
{
    public static class HostBuilderExtensions
    {
        public static IHostBuilder EnsureNServiceBusQueuesCreated(
            this IHostBuilder hostBuilder,
            string connectionString,
            string endpointName)
        {
            DBUtil.EnsureDBExistsAsync(connectionString).Wait();

            using var connection = new SqlConnection(connectionString);
            connection.Open();
            QueueCreateUtils.CreateQueuesForEndpoint(connection, "dbo", endpointName);

            return hostBuilder;
        }
    }
}
