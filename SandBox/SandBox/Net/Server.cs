using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows; 

namespace SandBox.Net
{
	public class Server
	{
		#region "Define Variables"
			public String ip;
			protected IPAddress _ip;

			public bool isWaiting;
			private TcpListener tcpListener;

			private TcpClient tcpClient;
			private BinaryReader reader;
			private BinaryWriter writer;
		#endregion

		public Server()
		{
			IPHostEntry ipe = Dns.GetHostEntry( Dns.GetHostName() );
			ip = ipe.AddressList[ipe.AddressList.Length - 1].ToString();
			_ip = ipe.AddressList[ipe.AddressList.Length - 1];
		}

		public Server(string str)
		{
			ip = str;
			_ip = IPAddress.Parse( ip );
		}

		public void Start()
		{
			tcpListener = new TcpListener(_ip, Int32.Parse("1324"));
			tcpListener.Start();

			isWaiting = true; // Allow new connection
			Thread thread = new Thread(new ThreadStart(ListenerProccess));
			thread.Start();
		}

		private void ListenerProccess()
		{
			Thread.Sleep(1000);
			while (isWaiting)
			{
				if ( tcpListener.Pending() )
				{
					tcpClient = tcpListener.AcceptTcpClient();
					NetworkStream networkStream = tcpClient.GetStream();
					reader = new BinaryReader(networkStream);
					writer = new BinaryWriter(networkStream);
				}
				Thread.Sleep(200);
			}
		}

	}
}
