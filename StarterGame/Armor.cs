using System;
using System.Collections.Generic;
using System.Text;

namespace Ascension
{
   public class Armor : Item
    {
        public int defense;
        public Armor(string name, string description, int value, int weight, int volume, int defense, List<Item> items) : base(name, description, value, weight, volume, items)
        {
            this.defense = defense;
        }
}
    // aaa
}
