using System;
using System.Threading.Tasks;
using NServiceBus.Pipeline;

namespace HappyCoding.NServiceBusWithSqlServer.ReceiverApp;

public class ClearLargeExceptionsBehavior : Behavior<IIncomingPhysicalMessageContext>
{
    public override async Task Invoke(IIncomingPhysicalMessageContext context, Func<Task> next)
    {
        try
        {
            await next();
        }
        catch (TestException testException)
        {
            Console.WriteLine("Resetting exception");
            testException.Reset();
            
            throw;
        }
    } 
}