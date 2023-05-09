using System;
using System.Collections.Generic;
using System.Text;

namespace Ascension
{
    //A turn based combat system 
    public class CombatSystem
    {
        //TODO: Write character dialogue 
        //TODO: Add observer for dodge, boolean character has attacked
        private int playerDamageTaken;
        private int characterDamageTaken;
        private double damage = 0;
        private bool dodgeflag;
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

            Notification notification = new Notification("DamageTaken", player);
            NotificationCenter.Instance.PostNotification(notification);
            if (player.EquippedWeapon != null)
            {
                player.InfoMessage(player.Name + " did " + player.EquippedWeapon.Used(player, character) + " damage");
            }
            else
            {
                player.InfoMessage(player.Name + " did " + player.WeaponlessAttack(player, character) + " damage");
            }
            if (player.CurrentHealth == 0 || character.CurrentHealth == 0)
            {
                player.State = States.GAME;
                character.State = States.GAME;
            }



        }
        //TODO: Make character able to attack player
        public void AttackPlayer(Player player)
        {
            if (dodgeflag == true)
            {
                Random rnd = new Random();
                Double dodgeChance = rnd.NextDouble();
                dodgeflag = false;
                if (dodgeChance < (player.aptitudeLvl.speed * 2) / 100)
                {
                    player.NormalMessage("You successfully dodged the enemy attack. No damage taken.");
                    return;
                }
            }
            Notification notification = new Notification("DamageTaken", character);
            NotificationCenter.Instance.PostNotification(notification);
            if (character.EquippedWeapon != null)
            {
                character.InfoMessage(character.Name + " did " + character.EquippedWeapon.Used(character, player) + " damage");
            }
            else
            {
                character.InfoMessage(character.Name + " did " + character.WeaponlessAttack(character, player) + " damage");
            }
            if (player.CurrentHealth == 0 || character.CurrentHealth == 0)
            {
                player.State = States.GAME;
                character.State = States.GAME;
            }
        }

        //TODO: Implement unescapable option
        //Player can flee battle so long as the battle is escapable
        public void Run()
        {
            if (player.aptitudeLvl.speed >= character.aptitudeLvl.speed)
            {
                character.NormalMessage("You flee the battle field immediately!");
                player.State = States.GAME;
                character.State = States.GAME;

            }
            else if (character.bossDelegate != null)
            {
                character.ErrorMessage("You cannot flee battle field! You are in the middle of a Boss Fight!");
            }
            else
            {
                character.ErrorMessage("You cannot flee the battle field. Your speed is not high enough.");
            }
            if (player.CurrentHealth == 0 || character.CurrentHealth == 0)
            {
                player.State = States.GAME;
                character.State = States.GAME;
            }
        }

        //Player can dodge an enemy attack if their speed is higher or equal to an enemy's
        public void Dodge()
        {
            dodgeflag = true;
        }
        /* Player can enchant themselves or their weapon damage dealt will change depending on which option
         * is chosen
         */
        public void Enchant(String SecondWord)
        {

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

            if (player.CurrentHealth == 0 || character.CurrentHealth == 0)
            {
                player.State = States.GAME;
                character.State = States.GAME;
            }

        }
    }
}

