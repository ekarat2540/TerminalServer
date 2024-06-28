using System.Net;
using System.Net.Sockets;

class Server
{
    static async Task Main(string[] args)
    {
        try
        {
            TcpListener server = new TcpListener(IPAddress.Any, 5713);
            server.Start();
            Console.WriteLine("Server is started on port 5713");
            while (true)
            {
                TcpClient client = await server.AcceptTcpClientAsync();
               _ = GetSender(client);
            }
        }catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
    static async Task GetSender(TcpClient client)
    {
        NetworkStream stream = client.GetStream();
        using (StreamReader reader = new StreamReader(stream))
        {
            string message;
            while ((message = await reader.ReadLineAsync()) != null)
            {
                Console.WriteLine($"Sender : {message}");
                await ToSender(message);
            }
        }
            
       client.Close();
    }
    
    static async Task ToSender(string msg)
    {
        try
        {
            TcpClient senderClient = new TcpClient("192.168.1.106",5713);
            NetworkStream senderStram = senderClient.GetStream();
            StreamWriter receiveWriter = new StreamWriter(senderStram);

            await receiveWriter.WriteLineAsync(msg);
            await receiveWriter.FlushAsync();

            receiveWriter.Close();
            senderStram.Close();
            senderClient.Close();
        }catch(Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

}
