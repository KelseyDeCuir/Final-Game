using System;
using System.Collections.Generic;
using System.Text;

namespace Ascension
{
    public class ReflectCommand : Command
    {
        // Allows the player to appraise themselves, getting name, inventory, equipped items, and general description
        public ReflectCommand() : base()
        {
            this.Name = "reflect";
        }
        public override bool Execute(Character player)
        {
            player.InfoMessage("You are " + player.Name + "\nYou look like " + player.Description + "\nYou currently have equipped:\n" + player.GetEquipped() + "\nYour Inventory currently contains:\n" + player.GetInventory());
            return false;
        }
    }
}
