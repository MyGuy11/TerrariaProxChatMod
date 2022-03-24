// Author = MyGuy

using Terraria;
using Terraria.ModLoader;
using Terraria.DataStructures;

using ProxChat.Buffs;
using ProxChatAssistant;
using System.Collections.Generic;

namespace ProxChat
{
    public class ProxChatPlayer : ModPlayer
    {
        private readonly List<string> lab = new() { "Dead", "InWorld" }; // Always gonna print the same values, so assigning them beforehand
        private readonly List<dynamic> val = new() { true, true }; // The values for the labels above
        
        public override void OnEnterWorld(Player player)
        {
            Main.NewText("Welcome to Terraria ProxChat!");

            //Give the Tracker buff to start update loop
            //int.MaxValue to ensure that the buff never runs out; Had it run out during testing with 2 tick buff and constant updates
            player.AddBuff(ModContent.BuffType<Tracker>(), int.MaxValue);
        }

        public override void Kill(double damage, int hitDirection, bool pvp, PlayerDeathReason damageSource)
        {
            EditValuesContainer(lab, val);
        }

        public override void OnRespawn(Player player)
        {
            player.AddBuff(ModContent.BuffType<Tracker>(), int.MaxValue);
        }

        public override void PreUpdate()
        {
            Player.AddBuff(ModContent.BuffType<Tracker>(), int.MaxValue);
            
        }

        public override void PreSavePlayer()
        {
            EditContainer("InWorld", false);
        }

        public override void PlayerDisconnect(Player player)
        {
            EditContainer("InWorld", false);
        }

        // These two containers exist so that I can await the ProxWriter methods.
        // This way, it doesn't freeze the game.
        public static async void EditContainer(string label, dynamic data)
        {
            await ProxWriter.EditValueAsync(ProxChat.AppDataPath, label, data);   
        }

        private static async void EditValuesContainer(List<string> labels, List<dynamic> values)
        {
            await ProxWriter.EditValuesAsync(ProxChat.AppDataPath, labels, values);
        }
    }
}
