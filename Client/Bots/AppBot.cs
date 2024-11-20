//using System;
//using System.CodeDom.Compiler;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.IO;
//using System.Linq;
//using System.Reflection;
//using Daydream.Client.Classes;
//using Daydream.Client.Utility;
//using Il2CppSystem.Collections;
//using MelonLoader;
//using Microsoft.CSharp;
//using UnhollowerBaseLib;
//using UnityEngine;
//using VRC;
//using VRC.Core;
//using VRC.SDKBase;
//using VRC.UI.Elements.Controls;

//namespace Daydream.Client.Bots
//{
//    internal class AppBot
//    {
//		public static bool showui = false;
//		public static string worldid = "";
//		// Token: 0x060000F9 RID: 249 RVA: 0x00006AE1 File Offset: 0x00004CE1
//		public static void botcloneavatar(string avid)
//        {
//			Connection.SendCommandToClients("botcloneavatar/" + avid);
//		}
//		public static void joinme()
//        {
//			Connection.SendCommandToClients("WRLD/" +   RoomManager.field_Internal_Static_ApiWorld_0.id + ":" + RoomManager.field_Internal_Static_ApiWorldInstance_0.instanceId);
//		}
//		public static void sendcommand(string cmd, bool val)
//		{
//			Connection.SendCommandToClients(cmd + "/" + val);
//		}
//		public static void CreateBot(int Profile)
//		{
//			//"--profile={0} ddprofile:"+ File.ReadAllLines(Path.Combine(Environment.CurrentDirectory, "DaydreamAssets") + "/bots.txt")[Profile] + " --fps=25 --no-vr --melonloader.randomrainbow -batchmode -DDPhotonBot -nolog vrchat://launch?id={1}"
//			int i = 0;
//			foreach (string key in File.ReadAllLines(Path.Combine(Environment.CurrentDirectory, "DaydreamAssets") + "/bots.txt"))
//			{
//				Utility.Logger.log("foundbot " + key + " " + i + " compared " + Profile);
//				string start = "";
//				if(showui == false)
//                {
//					start = "-batchmode";
//				}
//				if(i == Profile)
//                {
//					Process.Start(Directory.GetCurrentDirectory() + "\\VRChat.exe", string.Format("--profile={0} ddprofile:" + key + " --fps=25 --no-vr --melonloader.randomrainbow -DDPhotonBot " + start+" -nolog vrchat://launch?id={1}", 110 + Profile, ApiWorld. RoomManager.field_Internal_Static_ApiWorld_0.id + ":" + RoomManager.field_Internal_Static_ApiWorldInstance_0.instanceId));

//				}
//				i++;
//			}
//		}

//		// Token: 0x060000FA RID: 250 RVA: 0x00006B14 File Offset: 0x00004D14
//		public static void OnStart()
//		{
//			int i = 1;
//			foreach (string key in File.ReadAllLines(Path.Combine(Environment.CurrentDirectory, "DaydreamAssets") + "/bots.txt"))
//			{
//				AppBot.LoginAndProfile.Add(key, i);
//				Utility.Logger.log("addedbot " + key);
//				i++;
//			}

//			bool flag = Environment.GetCommandLineArgs().Contains("-DDPhotonBot");
//			if (flag)
//			{
//				Classes.Daydream.isphotonbot = true;
//				//Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.BelowNormal;
//				Connection.Client();
//				BotFunctions();
//			}
//			else
//			{
//				Classes.Daydream.isphotonbot = false;
//				Connection.StartServer();
//			}
//			foreach (var item in Environment.GetCommandLineArgs())
//			{
//				if (item.Contains("ddprofile"))
//				{

//					string uname = item.Split(':')[1];
//					string pass = item.Split(':')[2];
//					Utility.Logger.networklog(item);
//					Utility.Logger.networklog("Bot login " + uname + "  " + pass);
//					Client.Utility.LoginManager.login(uname, pass);
//					MelonCoroutines.Start(Client.Classes.Daydream.startlogin(uname,pass));

//				}
//			}
//		}
//		private static Action LastActionOnMainThread;
//		public static void OnUpdate()
//		{
//			if (LastActionOnMainThread != null)
//			{
//				LastActionOnMainThread();
//				LastActionOnMainThread = null;
//			}
//		}
//		// Token: 0x060000FB RID: 251 RVA: 0x00006BC0 File Offset: 0x00004DC0
//		public static void BotFunctions()
//        {

//        }

//		public static void orbitme(string userid, bool val)
//        {
//			Connection.SendCommandToClients("Orbit/" + userid + ":" + val);
//        }
//		public static void copyik(string userid, bool val)
//		{
//			Connection.SendCommandToClients("copyik/" + userid + ":" + val);
//		}
//		public static void ReceiveCommand(string Command)
//		{
//			if (Classes.Daydream.isphotonbot == true) {
//				CSharpCodeProvider codeProvider = new CSharpCodeProvider();
//				CompilerParameters parameters = new CompilerParameters();
//				parameters.GenerateInMemory = true;
//				parameters.GenerateExecutable = false;
//				parameters.ReferencedAssemblies.Add("system.dll");
//				string cmdval = Command.Split('/')[1];
//				string cmdname = Command.Split('/')[0];
//				Utility.Logger.networklog("Recieved command " + Command + " with val " + cmdval);
//				switch (cmdname)
//				{
//					//Hacks.CopyIK.Toggle(state)
//					case "WRLD":
//						Utility.Logger.networklog("WorldID: " + cmdval);
//						HandleActionOnMainThread(delegate
//						{
//							Networking.GoToRoom(cmdval);

//						});
//						break;
//					case "Orbit":
//						string data = cmdval.Split(':')[0];
//						bool val = bool.Parse(cmdval.Split(':')[1]);
//						Utility.Logger.networklog("Orbit: " + data + " " + val);
//						HandleActionOnMainThread(delegate
//						{
//							Hacks.Orbit.orbitenabled = val;
//							if (val == true)
//							{
//								Utility.Logger.log("orbit starting");
//								Utility.Logger.log("orbit player " + Utility.Extentions.getPlayerFromPlayerlist(data)._playerNet.name);
//								MelonCoroutines.Start(Hacks.Orbit.OrbitEnu(Utility.Extentions.getPlayerFromPlayerlist(data)));
//							}
//						});
//						break;
//					case "copyik":
//						string data2 = cmdval.Split(':')[0];
//						bool val2 = bool.Parse(cmdval.Split(':')[1]);
//						Utility.Logger.log("copyik " + data2 + " " + val2);
//						HandleActionOnMainThread(delegate
//						{
//							Hacks.CopyIK.Toggle(val2, Utility.Extentions.getPlayerFromPlayerlist(data2)._vrcplayer);

//						});
//						break;
//					case "mictoggle":
//						Utility.Logger.networklog("Toggled mic");
//						HandleActionOnMainThread(delegate
//						{
//							GameObject.Find("UserInterface/Canvas_QuickMenu(Clone)/Container/Window/MicButton").GetComponent<ToggleBinding>().field_Private_Toggle_0.isOn = bool.Parse(cmdval);

//						});
//						break;
//					case "botcloneavatar":
//						Utility.Logger.networklog("Toggled remote change");
//						HandleActionOnMainThread(delegate
//						{
//							Utility.Extentions.ChangeToAvatar(VRCPlayer.field_Internal_Static_VRCPlayer_0, cmdval);

//						});
//						break;
//					case "event1":
//						Utility.Logger.networklog("Toggled remote hack");
//						HandleActionOnMainThread(delegate
//						{
//							Hacks.event1.enable(bool.Parse(cmdval));

//						});
//						break;
//					case "Quest":
//						Utility.Logger.networklog("Toggled remote hack");
//						HandleActionOnMainThread(delegate
//						{
//							Hacks.Quest_Lag.enable(bool.Parse(cmdval));

//						});
//						break;
//					case "event6":
//						Utility.Logger.networklog("Toggled remote hack");
//						HandleActionOnMainThread(delegate
//						{
//							Hacks.event6.enable(bool.Parse(cmdval));

//						});
//						break;
//					case "event9":
//						Utility.Logger.networklog("Toggled remote hack");
//						HandleActionOnMainThread(delegate
//						{
//							Hacks.event9.enable(bool.Parse(cmdval));

//						}); break;
//					default:
//						Utility.Logger.errorlog("Error toggleing remote hack (" + cmdname + ")");
//						break;
//				}
//			}
//		}
//		public static void HandleActionOnMainThread(Action action)
//		{
//			LastActionOnMainThread = action;
//		}
//		// Token: 0x060000FC RID: 252 RVA: 0x00006C00 File Offset: 0x00004E00
//		public static void MainReceiveCommand(string Command)
//		{
//			Utility.Logger.networklog("Main recieved command " + Command);
//			string cmd = Command.Split('/')[0];
//			string data = Command.Split('/')[1];
//			HandleActionOnMainThread(delegate
//			{
//				switch (cmd)
//				{
//					case "event9":
//						Utility.Logger.networklog("Got remote command");
//						HandleActionOnMainThread(delegate
//						{
//							Utility.Logger.log(data);
//						}); break;
//					default:
//						Utility.Logger.errorlog("Error receving command (" + cmd + ")");
//						break;
//				}
//			});
//		}

//		// Token: 0x04000081 RID: 129
//		public static Dictionary<string, int> LoginAndProfile = new Dictionary<string, int>();
//	}
//}
