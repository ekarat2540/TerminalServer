using System.Net;
using System.Net.Sockets;

class Server
{
    static void Main(string[] args)
    {
        try
        {
            TcpListener server = TcpListener(IPAddress.Any, 5713);
            server.Start();
            Console.WriteLine("Server is started on port 5713");
        }catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
}
