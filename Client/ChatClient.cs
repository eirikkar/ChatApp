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

    public void StartClient()
    {
        Console.WriteLine("Connected to the server!");
        Console.WriteLine("Enter Message: ");
        var sendThread = new Thread(SendMessage);
        var receiveThread = new Thread(ReceiveMessages);
        sendThread.Start();
        receiveThread.Start();
    }

    public void SendMessage()
    {
        while (true)
        {
            string? message = Console.ReadLine();
            if (string.IsNullOrEmpty(message))
            {
                Console.WriteLine("Message cannot be empty");
            }
            writer.WriteLine(message);
        }
    }

    public void ReceiveMessages()
    {
        while (true)
        {
            try
            {
                string? message = reader.ReadLine();
                if (message != null)
                {
                    Console.WriteLine("Received: " + message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error receiving message: " + ex.Message);
                break;
            }
        }
    }

    public void CloseConnection()
    {
        reader.Close();
        writer.Close();
        tcpClient.Close();
    }
}
