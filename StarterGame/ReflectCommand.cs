using System;
using System.Collections.Generic;
using System.Text;

namespace Ascension
{
    public class ReflectCommand : Command
    {
        public ReflectCommand() : base()
        {
            this.Name = "reflect";
        }
        public override bool Execute(Character player)
        {
            player.InfoMessage("You are " + player.Name + "\nYou look like " + player.Description + "\nYour Inventory currently contains:\n" + player.GetInventory());
            return false;
        }
    }
}
