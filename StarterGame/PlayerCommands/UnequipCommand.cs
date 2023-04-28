using System;
using System.Collections.Generic;
using System.Text;

namespace Ascension
{
    class UnequipCommand : Command
    {
        public UnequipCommand() : base()
        {
            this.Name = "unequip";
        }
        public override bool Execute(Character player)
        {
            if (this.HasSecondWord())
            {
                Player pl = (Player)player;
                pl.unequip(this.SecondWord);

            }
            else
            {
                player.WarningMessage("Cannot Unequip the emptyness you feel...");
            }
            return false;
        }
    }
}
