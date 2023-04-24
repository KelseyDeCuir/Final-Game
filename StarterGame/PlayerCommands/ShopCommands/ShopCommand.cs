using System;
using System.Collections.Generic;
using System.Text;

namespace Ascension
{
    class ShopCommand : Command
    {
        public ShopCommand() : base()
        {
            this.Name = "shop";
        }
        public override bool Execute(Character player)
        {
            player.State = States.SHOP;
            player.InfoMessage("Welcome to the shop! You can buy or sell items here.");
            return false;
        }
    }
}
