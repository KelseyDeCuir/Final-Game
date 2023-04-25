using System;
using System.Collections.Generic;
using System.Text;

namespace Ascension
{
    public class Weapon : Item
    {
        public int damage;
        public bool enchanted;
        public Weapon(string name, string description, int value, int weight, int volume, int dmg, List<Item> items) : base(name, description, value, weight, volume, items)
        {
            damage = dmg;
            enchanted = false;
        }
        public override string GetDescription(Character character)
        {
            return "Weapon -> " + this.GetDamage(character) + " " + this.Description + " W: " + this.Weight + " V: " + this.Volume + " E: " + this.enchanted + ".";
        }

        public double GetDamage(Character character)
        {
            if (enchanted)
            {
                return (double)damage * (((double)character.aptitudeLvl.magic / 100) + 1);
            }
            else
            {
                return damage;
            }
        }

        public string Used(Character character, Character target)
        {
            int damageDone = 0;
            if (enchanted)
            {
                damageDone = target.TakeDamage(character, (double)damage * ((double)character.aptitudeLvl.magic/100 + 1));
                enchanted = false;
            }
            else
            {
                damageDone = target.TakeDamage(character,damage);
            }
            return "Damage done: " + damageDone;
        }
    }
}
