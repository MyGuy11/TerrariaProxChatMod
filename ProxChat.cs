// Author = MyGuy

using System;
using System.IO;
using Terraria.ModLoader;
// https://github.com/tModLoader/tModLoader/wiki/Update-Migration-Guide

namespace ProxChat
{
	public class ProxChat : Mod
	{
		public static string AppDataPath { get; private set; }

		public ProxChat()
		{
            // Get the path to the ProxChat.dat file
            AppDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "tModLoader");
            if (!Directory.Exists(AppDataPath)) { Directory.CreateDirectory(AppDataPath); }
            AppDataPath = Path.Combine(AppDataPath, "ProxChat.dat");
            
        }
    }
}