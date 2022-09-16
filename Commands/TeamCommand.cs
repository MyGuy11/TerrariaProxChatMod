// Author = MyGuy

using Terraria;
using Terraria.ModLoader;
using ProxChat.Buffs;

namespace ProxChat.Commands
{
    public class TeamCommand : ModCommand
    {
        public override CommandType Type
            => CommandType.Chat;

        public override string Command
            => "proxTeam";

        public override string Description
            => "Prints the player's current team integer";

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            Main.NewText(Tracker.team);
            Main.NewText(ProxChat.inWorld);
        }
    }
}