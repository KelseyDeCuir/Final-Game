using System;
using System.Collections.Generic;
using System.Text;

namespace Ascension
{
    //Yay Armor
   public class Armor : Item
    {
        public int defense;
        public Armor(string name, string description, int value, int weight, int volume, int defense, List<Item> items) : base(name, description, value, weight, volume, items)
        {
            this.defense = defense;
        }
        public override string GetDescription()
        {
            return "Armor -> " + this.defense + " " + this.Description + "W: " + this.Weight + " V: " + this.Volume + ".";
        }
    }
    // aaa
}
