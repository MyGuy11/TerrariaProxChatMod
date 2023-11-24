// Author.name = MyGuy

using Terraria;
using Terraria.ModLoader;

namespace ProxChat.Buffs
{
    public class Tracker : ModBuff
    {
        // 1 block = 2x2 ft
        // 1 block = 8x8 pixels
        // 1 ft = 4 pixels
        private const float FT_TO_M = 1f / 3.281f;
        private const float PIX_TO_FT = 1f / 4f;
        public static float PosToM(float pos) => pos * PIX_TO_FT * FT_TO_M;
        private float tempX;
        private float tempY;
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Tracker");
            // Description.SetDefault("Tracking your position");
            Main.buffNoTimeDisplay[Type] = true;
            Main.buffNoSave[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            tempX = PosToM(player.position.X);
            tempY = PosToM(player.position.Y);
            if (ProxChat.data.PosX != tempX)
            {
                ProxChat.data.PosX = tempX;
                ProxChatPlayer.WriteData(0);
            }

            if (ProxChat.data.PosY != tempY)
            {
                ProxChat.data.PosY = tempY;
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