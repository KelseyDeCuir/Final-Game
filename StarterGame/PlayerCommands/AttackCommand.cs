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
                try
                {
                    Player pl = (Player)player;
                    if (pl != null)
                    {
                        pl.AttackEnemy(this.SecondWord);
                    }
                }
                catch (System.InvalidCastException ex)
                {
                    player.AttackEnemy(this.SecondWord);
                }
            }
            else
            {
                try
                {
                    Player pl = (Player)player;
                    if (pl != null)
                    {
                        pl.WarningMessage("\nAttack What?");
                    }
                }
                catch (System.InvalidCastException ex)
                {

                }
            }
            return false;
        }
    }
}
