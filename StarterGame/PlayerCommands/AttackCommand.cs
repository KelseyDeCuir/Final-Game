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
                Player pl = player as Player;
                if (pl != null)
                {
                    pl.AttackEnemy(this.SecondWord);
                }
                else
                {
                    player.AttackEnemy(this.SecondWord);

                }

            }
            else
            { 
                Player pl = player as Player;
                if (pl != null)
                {
                    pl.WarningMessage("\nAttack What?");
                }
                else
                {
                    player.AttackEnemy(Player.Instance.Name);
                }

            }
            return false;
        }
    }
}
