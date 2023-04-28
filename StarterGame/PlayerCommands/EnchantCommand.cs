using System;
using System.Collections.Generic;
using System.Text;

namespace Ascension
{
    class EnchantCommand : Command
    {
        public EnchantCommand() : base()
        {
            this.Name = "enchant";
        }
        public override bool Execute(Character player)
        {
            if (this.HasSecondWord())
            {
                Player pl = (Player)player;
                pl.enchant(this.SecondWord);
            }
            else {
                player.WarningMessage("Cannot enchant nothing");
            }
            return false;
        }
    }
}
