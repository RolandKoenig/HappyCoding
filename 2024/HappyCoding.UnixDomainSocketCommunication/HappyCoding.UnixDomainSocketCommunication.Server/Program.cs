using System.Net.Sockets;

Console.Write("Enter socket file: ");
var socketFile = Console.ReadLine();
if (string.IsNullOrEmpty(socketFile))
{
    return 0;
}

if (File.Exists(socketFile))
{
    File.Delete(socketFile);
}

Console.WriteLine("Connecting to socket...");

var socket = new Socket(AddressFamily.Unix, SocketType.Stream, ProtocolType.Unspecified);

var socketEndPoint = new UnixDomainSocketEndPoint(socketFile);
socket.Bind(socketEndPoint);
socket.Listen();

var remoteSocket = await socket.AcceptAsync();
Console.WriteLine("Connected");
Console.WriteLine();


var networkStream = new NetworkStream(remoteSocket);
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
    catch (Exception ex)
    {
        Console.WriteLine("Error while listening: " + ex.ToString());
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