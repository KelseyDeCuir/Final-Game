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
            {
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
