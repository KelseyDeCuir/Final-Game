using System;
using System.Collections.Generic;
using System.Text;

namespace Ascension
{
    //TODO: REFACTOR
    public class OptionandEvent
    {
        public string PlayerOp { get; set; }
        public Command DialougeEvent { get; set; }

        public OptionandEvent(string PlayerOp, Command DialougeEvent) 
        { 
            this.PlayerOp = PlayerOp;
            this.DialougeEvent= DialougeEvent;
        }
    }
}
