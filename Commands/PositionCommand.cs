// Author.name = MyGuy

using ProxChat.Buffs;
using Terraria;
using Terraria.ModLoader;

namespace ProxChat.Commands
{
    public class PositionCommand : ModCommand
    {
        public override CommandType Type
            => CommandType.Chat;

        public override string Command
            => "proxPos";

        public override string Description
            => "Prints the player's current position";

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            var x = caller.Player.position.X;
            var y = caller.Player.position.Y;

            Main.NewText("X (pixels): " + x);
            Main.NewText("Y (pixels): " + y);
            Main.NewText("X (meters): " + Tracker.PosToM(x));
            Main.NewText("Y (meters): " + Tracker.PosToM(y));

            Main.NewText("ProxPosX: " + ProxChat.data.PosX);
            Main.NewText("ProxPosY: " + ProxChat.data.PosY);
        }
    }
}