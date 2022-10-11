// Author = MyGuy

using Terraria;
using Terraria.ModLoader;

namespace ProxChat.Commands
{
    public class FieldsCommand : ModCommand
    {
        public override CommandType Type
            => CommandType.Chat;

        public override string Command
            => "proxData";

        public override string Description
            => "Prints the values of the fields in the data container";

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            foreach (var value in typeof(DataContainer).GetProperties())
            {
                Main.NewText(value.Name + ": " + value.GetValue(ProxChat.data));
            }
        }
    }
}