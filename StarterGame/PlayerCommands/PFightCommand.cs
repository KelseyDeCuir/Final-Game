using System;
using System.Collections.Generic;
using System.Text;

namespace Ascension
{
    //When the fight command is called the player attacks an enemy according to their strength stat
    //An enemy can attack back after the player's turn
    public class PFightCommand : Command
    {
        public PFightCommand() : base()
        {
            this.Name = "fight";
        }
        public override bool Execute(Character player)
        {
            //Skills playerAttack = aptitudeLvl.strength;
            //Skills enemyAttack = 
            
            return false;
        }
    }
}
