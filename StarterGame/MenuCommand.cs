using System;
using System.Collections.Generic;
using System.Text;

namespace Ascension
{
    class MenuCommand : Command
    {
        public MenuCommand() : base()
        {
            this.Name = "menu";
        }
        public override bool Execute(Character player)
        {
            if (!this.HasSecondWord())
            {
                player.State = States.MENU;
            }
            else
            {
                player.WarningMessage("\nCannot menu " + this.SecondWord);
            }
            return false;
        }
    }
}
