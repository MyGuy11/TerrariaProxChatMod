// Author.name = MyGuy

using System;
using System.Text;

using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

using ProxChat.Buffs;

namespace ProxChat
{
    public class ProxChatPlayer : ModPlayer
    {
        private const int MAX_NAME_LENGTH = 20;

        public override void OnEnterWorld(Player player)
        {
            int arrPos = Array.IndexOf(Main.player, player);
            string temp = string.Concat(Player.name, arrPos);
            int difference = temp.Length - MAX_NAME_LENGTH;
            ProxChat.data.Name = temp.Length > MAX_NAME_LENGTH
            ? string.Concat(temp[..(temp.Length - difference)], arrPos)
            : temp;

            ProxChat.data.NameLen = (byte)ProxChat.data.Name.Length;

            ProxChat.data.WorldName = Main.worldName;
            ProxChat.data.WorldNameLen = (byte)Main.worldName.Length;

            ProxChat.data.InWorld = 1;

            ProxChat.data.PosX = Player.position.X;
            ProxChat.data.PosY = Player.position.Y;
            ProxChat.data.Team = (byte)Player.team;
            ProxChat.data.Dead = 0;
            ProxChat.data.RadioChannel = 0;

            WriteData(-1);

            Main.NewText("Welcome to Terraria ProxChat!");

            //Give the Tracker buff to start update loop
            //int.MaxValue to ensure that the buff never runs out; Had it run out during testing with 2 tick buff and constant updates
            player.AddBuff(ModContent.BuffType<Tracker>(), int.MaxValue);
        }

        public override void Kill(double damage, int hitDirection, bool pvp, PlayerDeathReason damageSource)
        {
            ProxChat.data.Dead = 1;
            WriteData(9);
        }

        public override void OnRespawn(Player player)
        {
            ProxChat.data.Dead = 0;
            WriteData(9);
            player.AddBuff(ModContent.BuffType<Tracker>(), int.MaxValue, false);
        }

        public override void PreUpdate()
        {
            Player.AddBuff(ModContent.BuffType<Tracker>(), int.MaxValue, false);
        }

        public static async void WriteData(int position)
        {
            ProxChat.stream.Position = (position is -1) ? 0 : position;
            switch (position)
            {
                case -1:
                    await ProxChat.stream.WriteAsync(ProxChat.data.ToByteArray());
                    break;

                case 0:
                    await ProxChat.stream.WriteAsync(BitConverter.GetBytes(ProxChat.data.PosX));
                    break;

                case 4:
                    await ProxChat.stream.WriteAsync(BitConverter.GetBytes(ProxChat.data.PosY));
                    break;

                case 8:
                    await ProxChat.stream.WriteAsync(new byte[] { ProxChat.data.Team });
                    break;

                case 9:
                    await ProxChat.stream.WriteAsync(new byte[] { ProxChat.data.Dead });
                    break;

                case 10:
                    await ProxChat.stream.WriteAsync(new byte[] { ProxChat.data.RadioChannel });
                    break;

                case 11:
                    await ProxChat.stream.WriteAsync(new byte[] { ProxChat.data.InWorld });
                    break;

                case 12:
                    await ProxChat.stream.WriteAsync(new byte[] { ProxChat.data.NameLen });
                    break;

                case 13:
                    await ProxChat.stream.WriteAsync(Encoding.UTF8.GetBytes(ProxChat.data.Name));
                    break;

                case 35:
                    await ProxChat.stream.WriteAsync(BitConverter.GetBytes(ProxChat.data.WorldNameLen));
                    break;

                case 36:
                    await ProxChat.stream.WriteAsync(Encoding.UTF8.GetBytes(ProxChat.data.WorldName));
                    break;

                case 127:
                    await ProxChat.stream.WriteAsync(new byte[] { 1 });
                    Main.NewText("Off");
                    break;

                case 1272:
                    await ProxChat.stream.WriteAsync(new byte[] { 0 });
                    Main.NewText("On");
                    break;

                default:
                    Main.NewText(position + " is not a valid position!", 255, 20, 0);
                    break;
            }
        }
    }
}