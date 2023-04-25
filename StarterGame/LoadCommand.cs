using System;
using System.Collections.Generic;
using System.Text;

namespace Ascension
{
    public class LoadCommand : Command  
    {
        public LoadCommand() : base()
        {
            this.Name = "load";
        }
        public override bool Execute(Character player)
        {
            if (this.HasSecondWord())
            {
                player.WarningMessage("\n Can't use save with " + this.SecondWord);
            }
            else
            {
                Player pl = (Player)player;
                pl.Loadinfo();
            }
            return false;
        }
    }
}
