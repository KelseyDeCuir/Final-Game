using System;
using System.Collections.Generic;
using System.Text;

namespace Ascension
{
    class ShopListCommand : Command
    {
        public ShopListCommand() : base()
        {
            this.Name = "list";
        }
        public override bool Execute(Character player)
        {
            if (!this.HasSecondWord())
            {
                player.InfoMessage("The shop contains the following Items:\n" + Elevator.Instance.GetItems());
            }
            else
            {
                player.WarningMessage("Cannot list that.");
            }
            return false;
        }
    }
}
