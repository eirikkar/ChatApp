namespace Server;

class Program
{
    static void Main(string[] args)
    {
        ChatServer chatServer = new ChatServer();
        chatServer.StartServer(4444);
    }
}
