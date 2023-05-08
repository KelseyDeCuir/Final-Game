using System;
using System.Collections.Generic;
using System.Text;

namespace Ascension
{
    class CAttackCommand : Command
    {
      public override bool Execute(Character character)
        {
            character.AttackPlayer(Player.Instance);
            return false;
        }
    }
}

