// Author = MyGuy

using Terraria;
using Terraria.ModLoader;

namespace ProxChat.Commands
{
    public class RadioChannelCommand : ModCommand
    {
        public override CommandType Type
            => CommandType.Chat;

        public override string Command
            => "proxChannel";

        public override string Description
            => "Prints the player's current radio channel integer";

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            //Main.NewText(RadioBuff.radioChannel);
            Main.NewText("Not implemented yet");
        }


    }
}