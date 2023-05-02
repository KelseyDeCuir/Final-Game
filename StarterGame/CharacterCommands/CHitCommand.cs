using System;
using System.Collections.Generic;
using System.Text;

namespace Ascension
{
    class CHitCommand : Command
    {
        public override bool Execute(Character player)
        {
            player.HitMonster();
            return false;
        }
    }
    }
