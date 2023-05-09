using System;
using System.Collections.Generic;
using System.Text;

namespace Ascension.PlayerCommands
{
    public class FightCommand : Command
    {
        public FightCommand() : base()
        {
            this.Name = "fight";
        }
        override
      public bool Execute(Character player)
        {
            if (this.HasSecondWord())
            {
                Player pl = (Player)player;
                if (pl != null)
                {
                    pl.Fight(this.SecondWord);
                }
            }
            else
            {
                Player pl = (Player)player;
                if (pl != null)
                {
                    pl.WarningMessage("\nAttack What?");
                }

            }
            return false;
        }
    }
}

