using System;
using System.Collections.Generic;
using System.Text;

namespace Ascension
{
    public class CharNameCommand : Command
    {
        public CharNameCommand() : base()
        {
            this.Name = "name";
        }
        public override bool Execute(Character player)
        {
            if (this.HasSecondWord())
            {
                player.SetName(SecondWord);
                Command reflect = new ReflectCommand(); // Check with Prof Obando
                reflect.Execute(player);
            }
            else
            {
                player.WarningMessage("Cannot enter a blank name");
            }
            return false;
        }
    }
}
