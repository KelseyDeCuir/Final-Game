using System;
using System.Collections.Generic;
using System.Text;

namespace Ascension
{
    class LookCommand : Command
    {
        /*
         * Shows the player the room they are in so they don't have to scroll up 
         */
        public LookCommand() : base()
        {
            this.Name = "look";
        }
        public override bool Execute(Character player)
        {
            if (this.HasThirdWord())
            {
                Player pl = (Player)player;
                pl.lookSpecfic(SecondWord, Thirdword);

            }
            else if (this.HasSecondWord()) {
                Player pl = (Player)player;
                pl.lookgeneral(SecondWord);
            }
            else
            {
                player.NormalMessage("\n" + player.CurrentRoom.Description());
            }
            return false;
        }
    }
}
