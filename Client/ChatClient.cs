using System.Net.Sockets;

class ChatClient
{
    private TcpClient tcpClient;
    private StreamReader reader;
    private StreamWriter writer;

    public ChatClient(string ip, int port)
    {
        tcpClient = new TcpClient(ip, port);
        var stream = tcpClient.GetStream();
        reader = new StreamReader(stream);
        writer = new StreamWriter(stream) { AutoFlush = true };
    }
}
