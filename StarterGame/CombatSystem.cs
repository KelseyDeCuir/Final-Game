using System;
using System.Collections.Generic;
using System.Text;

namespace Ascension
{
    //A turn based combat system 
    public class CombatSystem
    {
        private bool combatOngoing = true;
        private int playerHealth;
        private int enemyHealth;
        public void fight()
        {
            while(combatOngoing)
            {
                Console.WriteLine("Your avaliable commands are: Fight, Defend, Run, Dodge, Enchant");

            }
        }
        
    }
}
