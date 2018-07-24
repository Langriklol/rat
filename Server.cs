using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace RAT_Server
{
    class Server
    {
        private List<TcpClient> clients;

	    public Server()
	    {
		    this.clients = new List<TcpClient>();
	    }
	    
		public string Receive(TcpClient client)
 		{
			List<int> buffer = new List<int>();
			NetworkStream stream = client.GetStream();
			int readByte;
			 while ((readByte = stream.ReadByte()) != 0)
			 {
				 buffer.Add(readByte);
			 }

			 return Encoding.UTF8.GetString(buffer.Select<int, byte>(b => (byte) b).ToArray(), 0, buffer.Count);
		}

	    public void AddClient(TcpClient client)
	    {
		    this.clients.Add(client);
	    }

	    public List<TcpClient> GetClients()
	    {
		    return this.clients;
	    }
	    
    }
}
