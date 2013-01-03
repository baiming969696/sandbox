using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows; 

namespace SandBox.Net
{
	public class Client
	{
		#region DefineVariables
			public String hostIP;
			protected IPAddress _hostIP;

			private TcpClient tcpClient;
			public BinaryReader reader;
			public BinaryWriter writer;
		#endregion

		public Client(string str)
		{
			hostIP = str;
			_hostIP = IPAddress.Parse(hostIP);
			tcpClient = new TcpClient();
		}

		public void Connect()
		{
			Thread connectThread = new Thread(ConnectToServer);
			connectThread.Start();
		}

		private void ConnectToServer()
		{
			tcpClient.Connect(_hostIP, 1324);
			Thread.Sleep(100);
			if (tcpClient != null)
			{
				NetworkStream networkStream = tcpClient.GetStream();
				reader = new BinaryReader(networkStream);
				writer = new BinaryWriter(networkStream);
			}
		}

		public void Disconnect()
		{
			reader.Close();
			writer.Close();
			tcpClient.Close();
		}
		
	}
}
