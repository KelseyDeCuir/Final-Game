using System;
using System.Collections.Generic;
using System.Text;

namespace Ascension.PlayerCommands
{
     class EndDialougeCommand : Command
    {
        public EndDialougeCommand() : base()
        {
            this.Name = "end";
        }
        public override bool Execute(Character player)
        {

            Player pl = (Player)player;
            if (this.HasSecondWord())
            {
                pl.WarningMessage("Can't end dialouge with." + SecondWord);
            }
            else
            {
                pl.Enddialouge();
            }
            return false;
        }
    }
}
