// Author.name = MyGuy

using Terraria.ModLoader;

namespace ProxChat
{
    public class ProxChatSystem : ModSystem
    {
        public override void OnWorldUnload()
        {
            ProxChat.data.InWorld = 0;
            ProxChatPlayer.WriteData(11);
        }
    }
}