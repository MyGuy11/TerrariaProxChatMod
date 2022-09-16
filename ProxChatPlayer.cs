// Author = MyGuy

using System.Collections.Generic;

using Terraria;
using Terraria.ModLoader;
using Terraria.DataStructures;

using ProxChat.Buffs;
using System;

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

            unsafe
            {
                fixed (float* temp = &player.position.X) { Pointers.posX = temp; }
                fixed (float* temp = &player.position.Y) { Pointers.posY = temp; }
                fixed (int* temp = &player.team) { Pointers.team = temp; }
                fixed (bool* temp = &player.dead) { Pointers.dead = temp; }
                fixed (bool* temp = &ProxChat.inWorld) { Pointers.inWorldptr = temp; *temp = true; }
            }
        }

        public override void Kill(double damage, int hitDirection, bool pvp, PlayerDeathReason damageSource)
        {
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
            if (Array.IndexOf(Main.player, Player.name) == -1) { ProxChat.inWorld = false; }
        }
    }
}
