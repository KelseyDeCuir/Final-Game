using System;
using System.Collections.Generic;
using System.Text;

namespace Ascension
{
    public class Weapon : Item
    {
        public Weapon(string name, string description, int value, int weight, int volume, List<Item> items) : base(name, description, value, weight, volume, items)
        {

        }
    }
}
