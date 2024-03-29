// Author.name = MyGuy

using Terraria;
using Terraria.ModLoader;

namespace ProxChat.Commands
{
    public class NameCommand : ModCommand
    {
        public override CommandType Type
            => CommandType.Chat;

        public override string Command
            => "proxName";

        public override string Description
            => "Prints the player's name and identifier";

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            Main.NewText(ProxChat.data.Name);
        }
    }
}