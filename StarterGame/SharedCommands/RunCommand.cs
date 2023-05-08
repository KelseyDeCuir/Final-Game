using System;
using System.Collections.Generic;
using System.Text;

namespace Ascension
{
    public class RunCommand : Command
    {
        public RunCommand() : base()
        {
            this.Name = "run";
        }
        public override bool Execute(Character character)
        {
            if (this.HasSecondWord())
            {
                character.WarningMessage("\n Can't use run with " + this.SecondWord);
            }
            else
            {
                Player pl = (Player)player;
               // pl.RunFromEnemy();
            }
            return false;
        }
    }
}
