using System;
using System.Collections.Generic;
using System.Text;

namespace Ascension
{
    class PlayCommand : Command
    {
        // Switches player to GAME state
        public PlayCommand() : base()
        {
            this.Name = "play";
        }
        public override bool Execute(Character player)
        {
            if (!this.HasSecondWord() || this.SecondWord.Equals("game"))
            {
                player.State = States.GAME;
            }
            else
            {
                player.WarningMessage("\nCannot play " + this.SecondWord);
            }
            return false;
        }
    }
}
