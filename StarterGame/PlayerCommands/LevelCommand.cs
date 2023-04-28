using System;
using System.Collections.Generic;
using System.Text;

namespace Ascension
{
    class LevelCommand : Command
    {
        public LevelCommand() : base()
        {
            this.Name = "level";
        }
        public override bool Execute(Character player)
        {
            if (this.HasSecondWord())
            {
                Player pl = (Player)player;
                pl.levelup(this.SecondWord);
            }
            else
            {
                player.WarningMessage("Cannot level nothing.");
            }
            return false;
        }
    }
}