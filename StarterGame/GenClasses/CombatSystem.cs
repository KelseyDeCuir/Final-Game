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
        private double damage = 0;
        public Player player { get; }
        public Character character { get; }

        public CombatSystem()
        {
            NotificationCenter.Instance.AddObserver("CharacterTurn", CharacterTurn);
        }
        public CombatSystem(Player player, Character character)
        {
            this.player = player;
            this.character = character;
        }

        public void CharacterTurn(Notification notification)
        {
            Player player = (Player)notification.Object;
            if (player != null)
            {
                character.State = States.COMBAT;
            }
        }
        public void Attack(Character character)
        {
            //Player and enemy both attack one another
            while (combatOngoing)
            {
                player.State = States.COMBAT;
                Notification notification = new Notification("DamageTaken", player);
                NotificationCenter.Instance.PostNotification(notification);
                if (player.EquippedWeapon != null)
                {
                    player.EquippedWeapon.Used(player, character);
                    player.InfoMessage(player.Name + " did " + player.EquippedWeapon.GetDamage(player) + " damage");
                }
                else
                {
                    player.WeaponlessAttack(player, character);
                    player.InfoMessage(player.Name + " did " + player.WeaponlessAttack(player, character) + " damage");
                }
                notification = new Notification("CharacterTurn", this);
                NotificationCenter.Instance.PostNotification(notification);
                if (playerHealth == 0)
                {
                    combatOngoing = false;
                }
            }
            
        }
        public void AttackPlayer(Player player)
        {
            while (combatOngoing)
            {
                Notification notification = new Notification("DamageTaken", character);
                NotificationCenter.Instance.PostNotification(notification);
                if (character.EquippedWeapon != null)
                {
                    character.EquippedWeapon.Used(character, player);
                    character.InfoMessage(character.Name + " did " + character.EquippedWeapon.GetDamage(character) + " damage");
                }
                else
                {
                    character.WeaponlessAttack(character, player);
                    character.InfoMessage(character.Name + " did " + character.WeaponlessAttack(character, player) + " damage");
                }
                if (characterHealth == 0)
                {
                    combatOngoing = false;
                }
            }
        }
        //TODO: Implement unescapable option
        //Player can flee battle so long as the battle is escapable
        public void Run()
        {
            while (combatOngoing)
            {
                character.State = States.COMBAT;
                if (player.aptitudeLvl.speed >= character.aptitudeLvl.speed)
                {
                    character.NormalMessage("You flee the battle field immediately!");
                    combatOngoing = false;
                }
                else if (character.bossDelegate != null)
                {
                    character.ErrorMessage("You cannot flee battle field! You are in the middle of a Boss Fight!");
                }
                else
                {
                    character.ErrorMessage("You cannot flee the battle field. Your speed is not high enough.");
                }
                
            }
            /*
            if (playerHealth == 0 || characterHealth == 0)
            {
                combatOngoing = false;
            }
            */
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
            }
            /*
            if (playerHealth == 0 || characterHealth == 0)
            {
                combatOngoing = false;
            }
            */
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
               
            }
            /*
            if (playerHealth == 0 || characterHealth == 0)
            {
                combatOngoing = false;
            }
            */

        }


    }

}
