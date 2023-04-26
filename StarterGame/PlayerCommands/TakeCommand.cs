using System;
using System.Collections.Generic;
using System.Text;

namespace Ascension
{
    
    class TakeCommand : Command
    {
        public TakeCommand() : base()
        {
            this.Name = "take";
        }
        public override bool Execute(Character player)
        {
            Player pl = (Player)player;
            if (this.HasSecondWord())
            {//Note: I think this is breaking a command as the command class should only execute the actor method
             //we should put this in player
                pl.TakeOne(this.SecondWord);
            }
           else
            {
                pl.TakeAll(this.SecondWord);    
            }
   
            return false;
        }
    }
}
