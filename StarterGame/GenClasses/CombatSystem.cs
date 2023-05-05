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
        private int playerDamageTaken;
        private int characterHealth;
        private int characterDamageTaken;
        public Player player { get; }
        public Character character { get; }

        public CombatSystem()
        {

        }
        public CombatSystem(Player player, Character character)
        {
            this.player = player;
            this.character = character;
        }
        public void Attack(Character character)
        {
            //Player and enemy both attack one another
            while (combatOngoing)
            {
                player.State = States.COMBAT;
                player.NormalMessage("Your health is currently at: " + playerHealth);
                player.NormalMessage("The enemy's health is currently at: " + characterHealth);
                if (player.EquippedWeapon == null)
                {
                    characterDamageTaken = player.aptitudeLvl.strength / 10;
                }
                player.EquippedWeapon.Used(player, character);
                player.NormalMessage("Enemy health remaining " + characterHealth);
                character.EquippedWeapon.Used(character, player);
                player.NormalMessage("Player health remaining " + playerHealth);
                if (playerHealth == 0)
                {
                    combatOngoing = false;
                    player.Die(character);
                }
                else if (characterHealth == 0)
                {
                    combatOngoing = false;
                    character.Die(player);
                    player.State = States.GAME;
                }
            }
        }
        //TODO: Implement unescapable option
        //Player can flee battle so long as the battle is escapable
        public void Run()
        {
            while (combatOngoing)
            {
                player.State = States.COMBAT;
                if (player.aptitudeLvl.speed >= character.aptitudeLvl.speed)
                {
                    player.NormalMessage("You flee the battle field immediately!");
                    combatOngoing = false;
                }
                else if (character.bossDelegate != null)
                {
                    player.ErrorMessage("You cannot flee battle field! You are in the middle of a Boss Fight!");
                }
                else
                {
                    player.ErrorMessage("You cannot flee the battle field. Your speed is not high enough.");
                }
            }
        }
        //Player can dodge an enemy attack if their speed is higher or equal to an enemy's
        public void Dodge()
        {
            while (combatOngoing)
            {
                Random rnd = new Random();
                Double dodgeChance = rnd.NextDouble();
                player.State = States.COMBAT;
                if (dodgeChance < (player.aptitudeLvl.speed * 2) / 100)
                {
                    playerDamageTaken = 0;
                    player.NormalMessage("You successfully dodged the enemy attack. No damage taken.");
                }
                else
                {
                    player.ErrorMessage("You failed to dodge the enemy attack. Damage taken " + character.aptitudeLvl.strength);
                }

                if (playerHealth == 0)
                {
                    combatOngoing = false;
                    player.Die(character);
                }
                else if (characterHealth == 0)
                {
                    combatOngoing = false;
                    character.Die(player);
                    player.State = States.GAME;
                }
            }
        }
        /* Player can enchant themselves or their weapon damage dealt will change depending on which option
         * is chosen
         */
        public void Enchant(String SecondWord)
        {
            while (combatOngoing)
            {
                player.State = States.COMBAT;
                double armorEnchantDamage = player.EquippedArmor.GetDefense(player);
                double weaponEnchantDamage = player.EquippedWeapon.GetDamage(player);
                if (SecondWord.Equals("weapon"))
                {
                    player.enchant(SecondWord);
                }
                else if (SecondWord.Equals("armor"))
                {
                    player.enchant(SecondWord);
                }

                if (playerHealth == 0)
                {
                    combatOngoing = false;
                    player.Die(character);
                }
                else if (characterHealth == 0)
                {
                    combatOngoing = false;
                    character.Die(player);
                    player.State = States.GAME;
                }
            }

        }


    }

}
