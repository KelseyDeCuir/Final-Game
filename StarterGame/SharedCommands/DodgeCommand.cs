using System;
using System.Collections.Generic;
using System.Text;

namespace Ascension
{
    public class DodgeCommand : Command
    {
        public DodgeCommand() : base()
        {
            this.Name = "dodge";
        }
        public override bool Execute(Character player)
        {
            if (this.HasSecondWord())
            {
                player.WarningMessage("\n Can't use dodge with " + this.SecondWord);
            }
            else
            {
                Player pl = (Player)player;
                pl.DodgeAttack();
            }
            return false;
        }
    }
}
