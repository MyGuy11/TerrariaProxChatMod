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
            Main.NewText("X (pixels): " + Tracker.posX);
            Main.NewText("Y (pixels): " + Tracker.posY);
            Main.NewText("X (blocks): " + (Tracker.posX / 16));
            Main.NewText("Y (blocks): " + (Tracker.posY / 16));
            Main.NewText("X (meters): " + ((Tracker.posX / 16) * 2 * 0.3048));
            Main.NewText("Y (meters): " + ((Tracker.posY / 16) * 2 * 0.3048));
        }
    }
}