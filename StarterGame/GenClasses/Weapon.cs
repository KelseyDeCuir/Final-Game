﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Ascension
{
    public class Weapon : Item
    {
        public int damage;
        public bool enchanted;
        public Weapon(string name, string description, int value, int weight, int volume, int dmg) : base(name, description, value, weight, volume)
        {
            damage = dmg;
            enchanted = false;
        }
        public override string GetDescription(Character character)
        {
            return "Weapon -> " + this.GetDamage(character) + " "+ this.Name + " " + this.Description + " W: " + this.Weight + " V: " + this.Volume + " E: " + this.enchanted + ".";
        }

        public double GetDamage(Character character)
        {
            double StrengthDamage = (double)character.aptitudeLvl.strength / 10;
            if (enchanted)
            {
                return StrengthDamage * (double)damage * (((double)character.aptitudeLvl.magic / 7.5));
            }
            else
            {
                return StrengthDamage * damage;
            }
        }

        public string Used(Character character, Character target)
        {
            int damageDone = 0;
            double StrengthDamage = (double)character.aptitudeLvl.strength / 10;
            if (enchanted)
            {
                damageDone = target.TakeDamage(character, StrengthDamage * (double)damage * ((double)character.aptitudeLvl.magic/7.5));
                enchanted = false;
            }
            else
            {
                damageDone = target.TakeDamage(character, StrengthDamage * damage);
            }
            return "Damage done: " + damageDone;
        }
    }
}
