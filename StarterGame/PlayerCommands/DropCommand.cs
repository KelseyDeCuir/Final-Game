using System;
using System.Collections.Generic;
using System.Text;

namespace Ascension
{
    class DropCommand : Command
    {
        public DropCommand() : base()
        {
            this.Name = "drop";
        }
        public override bool Execute(Character player)
        {

            Player pl = (Player)player;
            if (this.HasSecondWord())
            {
                pl.Drop(this.SecondWord);
            }
            else
            {
                pl.WarningMessage("Cannot drop nothing.");
            }
            return false;
        }
    }
}
