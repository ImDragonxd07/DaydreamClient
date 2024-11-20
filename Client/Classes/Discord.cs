
using DiscordRPC;
using DiscordRPC.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using VRC.Integrations;

namespace Daydream.Client.Classes
{
    internal class Discord
    {
        //https://github.com/Lachee/discord-rpc-csharp/tree/415be201af088723a13f9b00ee5cb11a7d4563df
        private static int discordPipe = -1;

        //Called when your application first starts.
        //For example, just before your main loop, on OnEnable for unity.
        public void Initialize()
		{
            System.Diagnostics.Process proc = System.Diagnostics.Process.GetCurrentProcess();
            foreach (System.Diagnostics.ProcessModule module in proc.Modules)
            {
                if (module.FileName.Contains("discord-rpc"))
                   Utility.Logger.log("FOUND discord-rpc!");
            }
            /*
			Create a Discord client
			NOTE: 	If you are using Unity3D, you must use the full constructor and define
					 the pipe connection.
			*/
            //Set the logger
            Utility.Logger.discordlog("Loaded");
            var client = new DiscordRPC.DiscordRpcClient(
                "989285225953652816",                                  //The Discord Application ID            
                pipe: (int)-1,                          //The target pipe to connect too
                logger: new DiscordRPC.Logging.ConsoleLogger(DiscordRPC.Logging.LogLevel.Warning, true),                                 //The logger,
                autoEvents: false,                              //WE will manually invoke events
                client: new UnityNamedPipe()   //The client for the pipe to use. Unity MUST use a NativeNamedPipeClient since its managed client is broken.
            );
            // == Subscribe to some events
            client.OnReady += (sender, msg) =>
            {
                //Create some events so we know things are happening
                Console.WriteLine("Connected to discord with user {0}", msg.User.Username);
            };

            client.OnPresenceUpdate += (sender, msg) =>
            {
                //The presence has updated
                Console.WriteLine("Presence has been updated! ");
            };

            // == Initialize
            client.Initialize();
            Utility.Logger.discordlog("Setting");
            // == Set the presence
            client.SetPresence(new RichPresence()
            {
                Details = VRCPlayer.field_Internal_Static_VRCPlayer_0.prop_VRCPlayerApi_0.displayName,
                State = "The free vrchat client",
                Timestamps = Timestamps.Now,
                Buttons = new Button[]
                {
                    new Button() { Label = "Website", Url = "https://www.daydream.tk" }
                }
            }); ;
            // == Do the rest of your program.
            //Simulated by a Console.ReadKey
            // etc...
            Utility.Logger.discordlog("Continue");
            //Console.ReadKey();
        }
        public void Update()
        {
            Utility.Logger.discordlog("update");
        }
    }
}
