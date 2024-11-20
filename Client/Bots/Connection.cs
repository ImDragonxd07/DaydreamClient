//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net;
//using System.Net.Sockets;
//using System.Text;
//using System.Threading.Tasks;
//using Daydream.Client.Utility;
//using MelonLoader;
//using UnityEngine;

//namespace Daydream.Client.Bots
//{
//    internal class Connection
//    {
//		// Token: 0x060000EF RID: 239 RVA: 0x000067F4 File Offset: 0x000049F4
//		public static void SendCommandToClients(string Command)
//		{
//			try
//			{
//				(from s in Connection.ServerHandlers
//				 where s != null
//				 select s).ToList<Socket>().ForEach(delegate (Socket s)
//				 {
//					 s.Send(Encoding.ASCII.GetBytes(Command));
//				 });
//			}
//			catch (Exception arg)
//			{
//				Utility.Logger.errorlog(string.Format("{0}", arg));
//			}
//		}

//		// Token: 0x060000F0 RID: 240 RVA: 0x0000687C File Offset: 0x00004A7C
//		public static void OnClientReceiveCommand(string Command)
//		{
//			AppBot.ReceiveCommand(Command);
//		}

//		// Token: 0x060000F1 RID: 241 RVA: 0x00006886 File Offset: 0x00004A86
//		public static void ClientToMainCommand(string Command)
//		{
//			AppBot.MainReceiveCommand(Command);
//		}

//		// Token: 0x060000F2 RID: 242 RVA: 0x00006890 File Offset: 0x00004A90
//		public static void StartServer()
//		{
//			Connection.ServerHandlers.Clear();
//			Task.Run(new Action(Connection.HandleServer));
//		}

//		// Token: 0x060000F3 RID: 243 RVA: 0x000068B0 File Offset: 0x00004AB0
//		private static void HandleServer()
//		{
//			IPAddress ipaddress = Dns.GetHostEntry("localhost").AddressList[0];
//			IPEndPoint localEP = new IPEndPoint(ipaddress, 11000);
//			try
//			{
//				Socket socket = new Socket(ipaddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
//				socket.Bind(localEP);
//				socket.Listen(10);
//				Console.WriteLine(string.Format("[Server] Waiting for up to {0} connections...", AppBot.LoginAndProfile.Count));
//				for (int i = 0; i < AppBot.LoginAndProfile.Count; i++)
//				{
//					Connection.ServerHandlers.Add(socket.Accept());
//				}
//			}
//			catch (Exception ex)
//			{
//				Console.WriteLine("[Server] " + ex.ToString());
//			}
//		}

//		// Token: 0x060000F4 RID: 244 RVA: 0x0000697C File Offset: 0x00004B7C
//		public static void Client()
//		{
//			Task.Run(new Action(Connection.HandleClient));
//		}

//		// Token: 0x060000F5 RID: 245 RVA: 0x00006991 File Offset: 0x00004B91
//		internal static IEnumerator RetryConnection()
//		{
//			yield return new WaitForSeconds(5f);
//			Utility.Logger.networklog("Warning, Bot Disconnected From Server... Retrying Connection");
//			Connection.Client();
//			yield break;
//		}

//		// Token: 0x060000F6 RID: 246 RVA: 0x0000699C File Offset: 0x00004B9C
//		private static void HandleClient()
//		{
//			byte[] array = new byte[1024];
//			try
//			{
//				IPAddress ipaddress = Dns.GetHostEntry("localhost").AddressList[0];
//				IPEndPoint remoteEP = new IPEndPoint(ipaddress, 11000);
//				Socket socket = new Socket(ipaddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
//				try
//				{
//					socket.Connect(remoteEP);
//					Console.WriteLine("[Client] Socket connected to {0}", socket.RemoteEndPoint.ToString());
//					for (; ; )
//					{
//						int count = socket.Receive(array);
//						Connection.OnClientReceiveCommand(Encoding.ASCII.GetString(array, 0, count));
//					}
//				}
//				catch (ArgumentNullException ex)
//				{
//					Console.WriteLine("[Client] ArgumentNullException : {0}", ex.ToString());
//				}
//				catch (SocketException ex2)
//				{
//					Console.WriteLine("[Client] SocketException : {0}", ex2.ToString());
//					MelonCoroutines.Start(Connection.RetryConnection());
//				}
//				catch (Exception ex3)
//				{
//					Console.WriteLine("[Client] Unexpected exception : {0}", ex3.ToString());
//				}
//			}
//			catch (Exception ex4)
//			{
//				Console.WriteLine("[Client] " + ex4.ToString());
//			}
//		}

//		// Token: 0x04000080 RID: 128
//		public static List<Socket> ServerHandlers = new List<Socket>();
//	}
//}
