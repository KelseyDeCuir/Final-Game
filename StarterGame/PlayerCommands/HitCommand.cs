using System;
using System.Collections.Generic;
using System.Text;

namespace Ascension
{
    class HitCommand : Command
    {
        public HitCommand() : base()
        {
            this.Name = "hit-monster";
        }
        public override bool Execute(Character player)
        {
            Player pl = (Player)player;
            pl.HitMonster();
            return false;
        }
    }
}
