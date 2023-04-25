using System;
using System.Collections.Generic;
using System.Text;

namespace Ascension
{
    class SellCommand : Command
    {
        public SellCommand() : base()
        {
            this.Name = "sell";
        }
        public override bool Execute(Character player)
        {
            if (this.HasSecondWord())
            {
                if (player.Inventory.Exists(item => item.Name.ToLower().Equals(this.SecondWord)))
                {
                    Item item = player.Inventory.Find(item => item.Name.ToLower().Equals(this.SecondWord));
                    Elevator.Instance.items.Add(item);
                    player.Eyriskel += item.Value;
                    player.Inventory.Remove(item);
                    player.InfoMessage(string.Format("You sold {0} for {1} Eyriskel!", item.Name, item.Value));
                }
                else
                {
                    player.WarningMessage("Cannot find a " + this.SecondWord + " in your inventory to sell.");
                }
            }
            else
            {
                player.WarningMessage("Cannot sell nothing");
            }
            return false;
        }
    }
}
