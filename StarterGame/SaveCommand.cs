﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Ascension
{
    public class SaveCommand : Command
    {
        public SaveCommand() : base()
        {
            this.Name = "save";
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
                pl.Saveinfo();
            }
            return false;
        }
    }
}
