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
            player.NormalMessage("\n" + player.CurrentRoom.Description());
            return false;
        }
    }
}
