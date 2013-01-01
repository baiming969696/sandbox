using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows; 

namespace SandBox.Net
{
	public class TCPServer
	{
		#region Define Variables
			public String ip;
			private IPAddress _ip;
			private IPEndPoint point;

			private TcpListener tcpLister;
			private TcpClient tcpClient;
			private BinaryReader reader;
			private BinaryWriter writer;
		#endregion

		public void test()
		{

		}

		public TCPServer(string str) { ip = str; _ip = IPAddress.Parse(ip); }

		public void Server_Setup()
		{
			tcpLister = new TcpListener(_ip, Int32.Parse("1324"));
			tcpLister.Start();
			Thread thread = new Thread(new ThreadStart(Server_Proccess));
			thread.Start();
		}

		private void Server_Proccess()
		{
			Thread.Sleep(1000);
			while(true)
			{
				tcpClient = tcpLister.AcceptTcpClient();
				if (tcpLister != null)
				{
					NetworkStream networkStream = tcpClient.GetStream();
					reader = new BinaryReader(networkStream);
					writer = new BinaryWriter(networkStream);
				}
			}
		}

		public void Client_Connect()
		{

		}
	}
}
