using System;
using System.Collections.Generic;
using System.Text;

namespace Ascension
{
    public class BackCommand : Command
    {

        // Designated Constructor
        public BackCommand() : base()
        {
            this.Name = "back";
        }

        override
        public bool Execute(Character player)
        {
            if (this.HasSecondWord())
            {
                player.WarningMessage("\n Error");
            }
            else
            {
                Player pl = (Player)player;
                pl.Backto();
            }
            return false;
        }
    }
}
