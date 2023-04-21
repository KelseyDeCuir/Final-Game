using System;
using System.Collections.Generic;
using System.Text;

namespace Ascension
{
    public class Weapon : Item
    {
        public int damage;
        public bool enchanted;
        public Character wielder;
        public Weapon(string name, string description, int value, int weight, int volume, int dmg, List<Item> items) : base(name, description, value, weight, volume, items)
        {
            damage = dmg;
            enchanted = false;
        }
        public override string GetDescription()
        {
            return "Weapon -> " + this.GetDamage() + " " + this.Description + " W: " + this.Weight + " V: " + this.Volume + " E: " + this.enchanted + ".";
        }

        public void SetWielder(Character character)
        {
            wielder = character;
        }

        public double GetDamage()
        {
            if (enchanted)
            {
                return (double)damage * (((double)wielder.aptitudeLvl.magic / 100) + 1);
            }
            else
            {
                return damage;
            }
        }

        public string Used(Character target)
        {
            int damageDone = 0;
            if (enchanted)
            {
                damageDone = target.TakeDamage((double)damage * ((double)wielder.aptitudeLvl.magic/100 + 1));
                enchanted = false;
            }
            else
            {
                damageDone = target.TakeDamage(damage);
            }
            return "Damage done: " + damageDone;
        }
    }
}
