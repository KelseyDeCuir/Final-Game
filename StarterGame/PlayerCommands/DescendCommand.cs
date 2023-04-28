using System;
using System.Collections.Generic;
using System.Text;

namespace Ascension
{
    class DescendCommand : Command
    {
        public DescendCommand() : base()
        {
            this.Name = "descend";
        }

        //TODO: Refactor Descend Command
        public override bool Execute(Character player)
        {
            if (this.HasSecondWord())
            {
                player.WarningMessage("Cannot descend with" + this.SecondWord);
            }
            else
            {
                Player pl = (Player)player;
                pl.Descend();
            }
            return false;
        }
    }
}
