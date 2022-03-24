// Author = MyGuy

using Terraria;
using Terraria.ModLoader;
using ProxChat.Buffs;


namespace ProxChat.Commands
{
    public class PositionCommand : ModCommand
    {
        public override CommandType Type
            => CommandType.Chat;

        public override string Command 
            => "proxPos";

        public override string Usage 
            => "/proxPos";

        public override string Description
            => "Prints the player's current position";

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            Main.NewText("X: " + Tracker.posX);
            Main.NewText("Y: " + Tracker.posY);
        }
    }
}