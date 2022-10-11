// Author.name = MyGuy

using System;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Text;

using Terraria.ModLoader;
// https://github.com/tModLoader/tModLoader/wiki/Update-Migration-Guide

namespace ProxChat
{
    public class ProxChat : Mod
    {
        internal static string FilePath { get; private set; }
        internal static MemoryMappedFile mmf;
        internal static MemoryMappedViewStream stream;
        internal static DataContainer data;
        internal static int pid;

        // Bytes 60-62 of mmf are reserved for the Process ID
        // Byte 63 is reserved for debugging tool

        public ProxChat()
        {
            // Get the path of the file & map into memory
            FilePath = Path.Combine(Path.GetTempPath(), "tModLoaderProxChat.tmp");
            mmf = MemoryMappedFile.CreateFromFile(File.Create(FilePath, 64),
                                                  null,
                                                  64,
                                                  MemoryMappedFileAccess.ReadWrite,
                                                  HandleInheritability.None,
                                                  false
            );

            stream = mmf.CreateViewStream();
            stream.Position = 60;
            stream.Write(BitConverter.GetBytes(Environment.ProcessId), 0, 3);

            data = new();
        }

        public override void Close()
        {
            mmf.Dispose();
            stream.Dispose();
            base.Close();
        }
    }

    /// <summary>Summaries indicate starting byte</summary>
    internal struct DataContainer
    {
        /// <summary>The X pos of the player, 0 - 3</summary>
        public float PosX { get; set; }
        /// <summary>The Y pos of the player, 4 - 7</summary>
        public float PosY { get; set; }
        /// <summary>The team the player is on, 8</summary>
        public byte Team { get; set; }
        /// <summary>Whether or not the player is dead, 9</summary>
        public byte Dead { get; set; }
        /// <summary>The radio channel the player is on, 10</summary>
        public byte RadioChannel { get; set; }
        /// <summary>Whether or not the player is in a world, 11</summary>
        public byte InWorld { get; set; }
        /// <summary>The length of the player's proxchat name, 12</summary>
        public byte NameLen { get; set; }
        /// <summary>The player's proxchat name, 13 - 32</summary>
        public string Name { get; set; }
        /// <summary>The length of the world's name, 33</summary>
        public byte WorldNameLen { get; set; }
        /// <summary>The world's name, 34 - 60</summary>
        public string WorldName { get; set; }

        internal byte[] ToByteArray()
        {
            byte[] buf = new byte[64];

            BitConverter.GetBytes(PosX).CopyTo(buf, 0);
            BitConverter.GetBytes(PosY).CopyTo(buf, 4);
            buf[8] = Team;
            buf[9] = Dead;
            buf[10] = RadioChannel;
            buf[11] = InWorld;
            buf[12] = NameLen;
            Encoding.UTF8.GetBytes(Name).CopyTo(buf, 13);
            buf[33] = WorldNameLen;
            Encoding.UTF8.GetBytes(WorldName).CopyTo(buf, 34);

            return buf;
        }
    }
}