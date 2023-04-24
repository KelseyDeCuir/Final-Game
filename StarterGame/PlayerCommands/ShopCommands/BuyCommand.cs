using System;
using System.Collections.Generic;
using System.Text;

namespace Ascension
{
    class BuyCommand : Command
    {
        public BuyCommand() : base()
        {
            this.Name = "buy";
        }
        public override bool Execute(Character player)
        {
            if (this.HasSecondWord())
            {
                if (Elevator.Instance.items.Exists(item => item.Name.ToLower().Equals(this.SecondWord)))
                {
                    Item item = Elevator.Instance.items.Find(item => item.Name.ToLower().Equals(this.SecondWord));
                    if (player.Eyriskel >= item.Value + 5)
                    {
                        player.Inventory.Add(item);
                        player.Eyriskel -= (item.Value + 5);
                        Elevator.Instance.items.Remove(item);
                        player.InfoMessage(string.Format("You bought {0} for {1} Eyriskel!", item.Name, item.Value + 5));
                    }
                    else
                    {
                        player.WarningMessage("You do not have " + (item.Value + 5) + " Eyriskel");
                    }
                }
                else
                {
                    player.WarningMessage("Cannot find a " + this.SecondWord + " in the shop to buy.");
                }
            }
            else
            {
                player.WarningMessage("Cannot buy nothing");
            }
            return false;
        }
    }
}
