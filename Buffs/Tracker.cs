// Author = MyGuy

using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using ProxChatAssistant;

namespace ProxChat.Buffs
{
    public class Tracker : ModBuff
    {
        private int count;
        private static int errorCount;
        internal static int arrayPos;
        internal static string name; // Print this
        internal static float posX; // Print this
        internal static float posY; // Print this
        internal static int team; // Print this

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Tracker");
            Description.SetDefault("Tracking your position");
            Main.buffNoTimeDisplay[Type] = true;
            Main.buffNoSave[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            // Update the player variables
            if (count == 0) 
            {
                for (int i = Main.player.Length - 1; i >= 0; i--)
                {
                    if (player.name.Equals(Main.player[i])) 
                    {
                        arrayPos = i; 
                        break; 
                    }
                }
                name = player.name + arrayPos;
            }
            count++;

            posX = player.position.X;
            posY = player.position.Y;
            team = player.team;

            // Write the data
            WritePos();
        }


        // Known Bug: Writer operation sometimes gets interrupted on death (at least I believe that's what's happening)
        // causing the writer to void all values other than Dead and InWorld (written in ProxChatPlayer).
        //
        // Potential Suspect: When the player dies, the tracker instance probably disappears, causing the write to
        // be interrupted.
        public static async void WritePos()
        {
            List<string> labels = new() { "Name", "PositionX", "PositionY", 
                                          "Team", "Dead", "InWorld" };
            List<dynamic> values = new() { name, posX, posY, 
                                           team, false, true };

            /*
            for (int i = 0; i < labels.Count; i++)
            {
                Console.WriteLine("i: " + labels[i] + " + " + values[i]);
            }
            */
            try { await ProxWriter.EditValuesAsync(ProxChat.AppDataPath, labels, values); }
            catch
            {
                if (errorCount == 0) 
                { 
                    Main.NewText("Failed to write file! (" + ProxChat.AppDataPath + ") Contact mod creator on github for help.", Convert.ToByte(255), Convert.ToByte(0), Convert.ToByte(0));
                    errorCount++;
                }
            }
        }
    }
}