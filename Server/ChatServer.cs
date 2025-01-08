using System.Net;
using System.Net.Sockets;

public class ChatServer
{
    private TcpListener? tcpListener;
    private List<TcpClient> tcpClients = new List<TcpClient>();

    public void StartServer(int port)
    {
        tcpListener = new TcpListener(IPAddress.Any, port);
        tcpListener.Start();
        Console.WriteLine($"Server started on port {port}...");

        while (true)
        {
            var client = tcpListener.AcceptTcpClient();
            tcpClients.Add(client);
            Console.WriteLine("A new client has connected to the server!");

            var clientThread = new Thread(ClientHandler);
            clientThread.Start(client);
        }
    }

    private void ClientHandler(object clientObject)
    {
        var client = (TcpClient)clientObject;
        var stream = client.GetStream();
        var reader = new StreamReader(stream);

        try
        {
            while (true)
            {
                var message = reader.ReadLine();
                if (message == null)
                {
                    break;
                }
                Console.WriteLine($"Client: {message}");
                WriteMessage(message);
            }
        }
        catch (Exception)
        {
            Console.WriteLine("A client has disconnected!");
        }
        finally
        {
            tcpClients.Remove(client);
            client.Close();
        }
    }

    private void WriteMessage(string message)
    {
        foreach (var client in tcpClients)
        {
            try
            {
                var writer = new StreamWriter(client.GetStream()) { AutoFlush = true };
                writer.WriteLine(message);
            }
            catch { }
        }
    }
}
