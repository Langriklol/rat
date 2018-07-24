using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace RAT_Server
{
    class Program
    {
        //static byte[] data; 
        //static Socket socket;
        static void Main(string[] args)
        {
            Console.WriteLine("Server started. :-)");
            /*socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Bind(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 6666));

            socket.Listen(sizeof(int));
            while (true)
            {
                Console.WriteLine("Accepting");
                Socket accepteddata = socket.Accept(); 
                data = new byte[accepteddata.SendBufferSize];
            
                int j = accepteddata.Receive(data);
                byte[] adata = new byte[j];
                for (int i = 0; i < j; i++)
                    adata[i] = data[i];
                string dat = Encoding.Default.GetString(adata);
                Console.WriteLine(dat);*/
            Server server = new Server();
            TcpListener listener =  new TcpListener(IPAddress.Any, 666);
            listener.Start();

            while (true)
            {
                if (listener.Pending())
                {
                    TcpClient client = listener.AcceptTcpClient();
                    server.AddClient(client);
                    Console.WriteLine(String.Format("Client {0} connected.", server.GetClients().Count));
                }

                foreach (TcpClient client in server.GetClients())
                {
                    if (client.GetStream().DataAvailable)
                    {
                        string msg = server.Receive(client);
                        if (msg == "shutdown")
                            return;
                        Console.WriteLine(msg);
                    }
                }
            }
        }
    }
}
