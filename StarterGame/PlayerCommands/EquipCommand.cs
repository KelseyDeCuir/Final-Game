using System;
using System.Collections.Generic;
using System.Text;

namespace Ascension
{
    class EquipCommand : Command
    {
        public EquipCommand () : base()
        {
            this.Name = "equip";
        }
        public override bool Execute(Character player)
        {
            if (this.HasSecondWord()) {
                Player pl = (Player)player;
                pl.equip(this.SecondWord);
            }
            else
            {
                player.WarningMessage("Cannot equip the concept of nothing.");
            }
            return false;
        }
    }
}
