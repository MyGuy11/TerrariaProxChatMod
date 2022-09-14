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
            var x = caller.Player.position.X;
            var y = caller.Player.position.Y;
            Main.NewText("X (pixels): " + x);
            Main.NewText("Y (pixels): " + y);
            Main.NewText("X (blocks): " + (x / 16));
            Main.NewText("Y (blocks): " + (y / 16));
            Main.NewText("X (meters): " + (x / 32 * 0.3048));
            Main.NewText("Y (meters): " + (y / 32 * 0.3048));
        }
    }
}