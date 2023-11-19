// Author.name = MyGuy

using Terraria;
using Terraria.ModLoader;

namespace ProxChat.Buffs
{
    public class Tracker : ModBuff
    {
        private const float FT_TO_M = 1f / 3.281f;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Tracker");
            Description.SetDefault("Tracking your position");
            Main.buffNoTimeDisplay[Type] = true;
            Main.buffNoSave[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            if (ProxChat.data.PosX != player.position.X * FT_TO_M)
            {
                ProxChat.data.PosX = player.position.X * FT_TO_M;
                ProxChatPlayer.WriteData(0);
            }

            if (ProxChat.data.PosY != player.position.Y * FT_TO_M)
            {
                ProxChat.data.PosY = player.position.Y * FT_TO_M;
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