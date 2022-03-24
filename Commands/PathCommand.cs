// Author = MyGuy

using Terraria;
using Terraria.ModLoader;


namespace ProxChat.Commands
{
    public class PathCommmand : ModCommand
    {
        public override CommandType Type
            => CommandType.Chat;

        public override string Command 
            => "path";

        public override string Usage 
            => "/path";

        public override string Description
            => "Prints the mod's data path";

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            Main.NewText(ProxChat.AppDataPath);
        }
    }
}
