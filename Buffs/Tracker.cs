// Author = MyGuy

using Terraria;
using Terraria.ModLoader;

namespace ProxChat.Buffs
{
    public class Tracker : ModBuff
    {
        private int count; // Used to make the name assignment happen once
        internal static char[] name; // Print this
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
                string tempString = player.name;
                for (int i = Main.player.Length - 1; i >= 0; i--)
                {
                    if (player.name.Equals(Main.player[i]))
                    {
                        tempString += i;
                        break;
                    }
                }
                // Adds the player's Main.player array position to prevent
                // an issue with duplicate player names.
                name = tempString.ToCharArray();
                unsafe
                {
                    fixed (char* temp = &name[0]) { Pointers.nameStr = temp; };
                    Pointers.nameStrLen = name.Length;
                }
            }
            count++;

            // Write the data
            //WritePos();
        }
    }
}