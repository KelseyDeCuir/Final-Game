using System;
using System.Collections.Generic;
using System.Text;

namespace Ascension
{
    class CAttackCommand : Command
    {
        public CAttackCommand() : base()
        {
            this.Name = "characterattack";
        }
        override
      public bool Execute(Character character)
        {
            if (this.HasSecondWord())
            {
                Character ch = (Character)character;
                ch.AttackPlayer(this.SecondWord);

            }
            return false;
        }
    }
}

