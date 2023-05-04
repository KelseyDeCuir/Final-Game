using System;
using System.Collections.Generic;
using System.Text;

namespace Ascension
{
    //When the fight command is called the player attacks an enemy according to their strength stat
    //An enemy can attack back after the player's turn
    public class AttackCommand : Command
    {
        public AttackCommand() : base()
        {
            this.Name = "attack";
        }
        override
      public bool Execute(Character player)
        {
            if (this.HasSecondWord())
            {
                Player pl = (Player)player;
                pl.AttackEnemy(this.SecondWord);

            }
            return false;
        }
    }
}
