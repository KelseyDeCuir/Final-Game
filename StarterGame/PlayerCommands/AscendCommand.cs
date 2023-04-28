using System;
using System.Collections.Generic;
using System.Text;

namespace Ascension
{
    class AscendCommand : Command
    {
        public AscendCommand() : base()
        {
            this.Name = "ascend";
        }
        public override bool Execute(Character player)
        {
            if (this.HasSecondWord())
            {
                player.WarningMessage("Cannot ascend with" + this.SecondWord);
            }
            else
            {
                Player pl = (Player)player;
                pl.Ascend();
            }
            return false;
        }
    }
}
