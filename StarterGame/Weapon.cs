using System;
using System.Collections.Generic;
using System.Text;

namespace Ascension
{
    public class Weapon : Item
    {
        int damage;
        public Weapon(string name, string description, int value, int weight, int volume, int dmg, List<Item> items) : base(name, description, value, weight, volume, items)
        {
            damage = dmg;
        }
    }
}
