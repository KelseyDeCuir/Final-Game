using System;
using System.Collections.Generic;
using System.Text;

namespace Ascension.PlayerCommands
{
    public class TalkCommand : Command
    {
        public TalkCommand() : base()
        {
            this.Name = "talk";
        }
        public override bool Execute(Character player)
        {
            if (this.HasSecondWord())
            {
                Player pl = (Player)player;
                pl.talkto(SecondWord);
            }
            else
            {
                player.WarningMessage("\n Can't talk to no one");
            }
            return false;
        }
    }
}
