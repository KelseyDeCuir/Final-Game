using System;
using System.Collections.Generic;
using System.Text;

namespace Ascension.PlayerCommands
{
    public class ContinueDialougeCommand : Command
    {
        public ContinueDialougeCommand() : base()
        {
            this.Name = "continue";
        }
        public override bool Execute(Character player)
        {

            Player pl = (Player)player;
            if (this.HasSecondWord())
            {
                pl.WarningMessage("Can't continue dialouge with." + SecondWord);
            }
            else
            {
                pl.continuedialouge();
            }
            return false;
        }
    }
}
