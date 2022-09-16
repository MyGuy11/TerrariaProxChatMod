// Author = MyGuy

using Terraria;
using Terraria.ModLoader;


namespace ProxChat.Commands
{
    public class PointerCommand : ModCommand
    {
        public override CommandType Type
            => CommandType.Chat;

        public override string Command
            => "proxPtr";

        public override string Description
            => "Prints the address of the value pointers";

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            foreach (string line in Pointers.GetAddresses()) { Main.NewText(line); }
        }


    }
}