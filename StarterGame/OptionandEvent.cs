using System;
using System.Collections.Generic;
using System.Text;

namespace Ascension
{
    //TODO: REFACTOR
    public class OptionandEvent
    {
        public string PlayerOp { get; set; }
        public string DialougeEvent { get; set; }

        public OptionandEvent(string PlayerOp,string DialougeEvent) 
        { 
            this.PlayerOp = PlayerOp;
            this.DialougeEvent= DialougeEvent;
        }
    }
}
