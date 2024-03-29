// Author.name = MyGuy

using Terraria.ModLoader;

namespace ProxChat.Commands
{
    public class DebugCommand : ModCommand
    {
        public override CommandType Type
            => CommandType.Chat;

        public override string Command
            => "proxDebug";

        public override string Usage
            => "/proxDebug {on/off}";

        public override string Description
            => "Exits the intermediary script [Debug Only]";

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            if (args[0].Equals("off", System.StringComparison.OrdinalIgnoreCase)) { ProxChatPlayer.WriteData(127); }
            if (args[0].Equals("on", System.StringComparison.OrdinalIgnoreCase)) { ProxChatPlayer.WriteData(1272); }
        }
    }
}