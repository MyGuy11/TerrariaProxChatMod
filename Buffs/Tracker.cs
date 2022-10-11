// Author.name = MyGuy

using Terraria;
using Terraria.ModLoader;

namespace ProxChat.Buffs
{
    public class Tracker : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Tracker");
            Description.SetDefault("Tracking your position");
            Main.buffNoTimeDisplay[Type] = true;
            Main.buffNoSave[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            if (ProxChat.data.PosX != player.position.X)
            {
                ProxChat.data.PosX = player.position.X;
                ProxChatPlayer.WriteData(0);
            }

            if (ProxChat.data.PosY != player.position.Y)
            {
                ProxChat.data.PosY = player.position.Y;
                ProxChatPlayer.WriteData(4);
            }

            if (ProxChat.data.Team != (byte)player.team)
            {
                ProxChat.data.Team = (byte)player.team;
                ProxChatPlayer.WriteData(8);
            }

            //if (ProxChat.data.RadioChannel) // Implement later
        }
    }
}