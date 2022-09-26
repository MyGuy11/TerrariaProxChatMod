using System.Text;
using System.IO.MemoryMappedFiles;
using System.ComponentModel.DataAnnotations;

using System.Security;
// Author = MyGuy

using System;
using System.IO;
using Terraria.ModLoader;
using Terraria;
// https://github.com/tModLoader/tModLoader/wiki/Update-Migration-Guide

namespace ProxChat
{
    public class ProxChat : Mod
    {
        internal static string FilePath { get; private set; }
        internal static MemoryMappedFile mmf;
        internal static MemoryMappedViewStream stream;
        internal static DataContainer data;

        public ProxChat()
        {
            // Get the path to the ProxChat.dat file
            FilePath = Path.Combine(Path.GetTempPath(), "tModLoaderProxChat.tmp");
            mmf = MemoryMappedFile.CreateFromFile(File.Create(FilePath, 64),
                                                  null,
                                                  64,
                                                  MemoryMappedFileAccess.ReadWrite,
                                                  HandleInheritability.None,
                                                  false
            );

            stream = mmf.CreateViewStream();
            data = new();
        }

        public override void Close()
        {
            mmf.Dispose();
            stream.Dispose();
            File.WriteAllText(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "ProxModLog.txt"), "streams disposed");
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
        /// <summary>The player's proxchat name, 13-32</summary>
        [MaxLength(20)] // Max length of a player's name is 20 in vanilla
        public string Name { get; set; }

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

            return buf;
        }

        internal Span<byte> ToByteSpan()
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

            return buf;
        }
    }
}