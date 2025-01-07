namespace Client;

class Program
{
    static void Main(string[] args)
    {
        ChatClient client = new ChatClient("127.0.0.1", 4444);
        client.StartClient();
    }
}
