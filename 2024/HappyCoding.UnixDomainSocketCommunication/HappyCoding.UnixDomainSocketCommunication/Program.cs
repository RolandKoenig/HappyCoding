using System.Net.Sockets;

Console.Write("Enter socket file: ");
var socketFile = Console.ReadLine();
if (string.IsNullOrEmpty(socketFile))
{
    return 0;
}

if (!File.Exists(socketFile))
{
    await using var fileStream = File.Create(socketFile);
    fileStream.Close();
}

Console.WriteLine("Connecting to socket...");
var socketEndPoint = new UnixDomainSocketEndPoint(socketFile);
var socket = new Socket(AddressFamily.Unix, SocketType.Stream, ProtocolType.Unspecified);
await socket.ConnectAsync(
    socketEndPoint, 
    CancellationToken.None);
Console.WriteLine("Successfully conected");

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
    await networkStreamWriter.WriteAsync(nextLine);

    nextLine = Console.ReadLine();
}

return 0;