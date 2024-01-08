using System.Net.Sockets;

Console.Write("Enter socket file: ");
var socketFile = Console.ReadLine();
if (string.IsNullOrEmpty(socketFile))
{
    return 0;
}


Console.WriteLine("Connecting to socket...");

var socket = new Socket(AddressFamily.Unix, SocketType.Stream, ProtocolType.Unspecified);

var socketEndPoint = new UnixDomainSocketEndPoint(socketFile);
await socket.ConnectAsync(socketEndPoint);

Console.WriteLine("Connected");
Console.WriteLine();

var networkStream = new NetworkStream(socket);
var networkStreamWriter = new StreamWriter(networkStream);
var networkStreamReader = new StreamReader(networkStream);
Task.Run(async () =>
{
    try
    {
        var nextReceivedLine = await networkStreamReader.ReadLineAsync();
        while (nextReceivedLine != null)
        {
            Console.WriteLine("Received: " + nextReceivedLine);
            nextReceivedLine = await networkStreamReader.ReadLineAsync();
        }
    }
    catch (Exception)
    {
        // Nothing to do
    }
});


var nextLine = Console.ReadLine();
while (!string.IsNullOrEmpty(nextLine))
{
    await networkStreamWriter.WriteLineAsync(nextLine);
    await networkStreamWriter.FlushAsync();

    nextLine = Console.ReadLine();
}

return 0;