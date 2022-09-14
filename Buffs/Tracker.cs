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
        private int count; // Used to make the name assignment happen once
        private static int errorCount; // Used to make the error message appear once
        internal static int arrayPos; // The player's position in Main.player
        internal static string name; // Print this
        internal static float posX;
        internal static float posY;
        internal static int team;


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
                // Adds the player's Main.player array position to prevent
                // an issue with duplicate player names.
                name = player.name + arrayPos;
            }
            count++;

            // Write the data
            //WritePos();
        }

        
        /* Known Bug: Writer operation sometimes gets interrupted on death (at least I believe that's what's happening)
         * causing the writer to void all values other than Dead and InWorld (written in ProxChatPlayer).
         *
         * Potential Suspect: When the player dies, the tracker instance probably disappears, causing the write to
         * be interrupted.
         * 
         * This actually isn't an issue, as I write the "dead" value in the ProxChatPlayer class,
         * so it still writes to the file, allowing the plugin to know not to fetch values.
         * The mod automatically writes the position values again when the player respawns.
        */
 
        public static async void WritePos()
        {
            List<string> labels = new() { "Name", "PositionX", "PositionY", 
                                          "Team", "Dead", "InWorld" };
            List<dynamic> values = new() { name, //posX, posY, 
                                           /*team,*/ false, true };

            // debug stuff
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