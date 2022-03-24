using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.DataStructures;
// Author = MyGuy

using ProxChat.Buffs;
using ProxChatAssistant;
using System.Collections.Generic;

namespace ProxChat
{
    public class ProxChatPlayer : ModPlayer
    {
        public static int count { get; set; }
        private readonly List<string> lab = new() { "Dead", "InWorld" };
        private readonly List<dynamic> val = new() { true, true };
        public override void OnEnterWorld(Player player)
        {
            Main.NewText("Welcome to Terraria ProxChat!");

            //Give the Tracker buff to start update loop
            //int.MaxValue to ensure that the buff never runs out; Had it run out during testing with 2 tick buff and constant updates
            player.AddBuff(ModContent.BuffType<Tracker>(), int.MaxValue);
            count = 0;
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
            Console.WriteLine(player.name);

            foreach (var _player in Main.player)
            {
                if (_player.name != " ") { Console.WriteLine(_player.name); }
            }
        }

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