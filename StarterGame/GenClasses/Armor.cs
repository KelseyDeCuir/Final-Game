using System;
using System.Collections.Generic;
using System.Text;

namespace Ascension
{
    //Yay Armor
   public class Armor : Item
    {
        public int defense;
        public bool enchanted;
        public Armor(string name, string description, int value, int weight, int volume, int defense, List<Item> items) : base(name, description, value, weight, volume, items)
        {
            this.defense = defense;
        }
        public override string GetDescription(Character character)
        {
            return "Armor -> " + this.GetDefense(character) + " " + this.Description + " W: " + this.Weight + " V: " + this.Volume + " E: " + this.enchanted + ".";
        }
        public double GetDefense(Character character)
        {
            if (enchanted)
            {
                return (double)defense * (((double)character.aptitudeLvl.intelligence/100) +1);
            }   
            else
            {
                return defense;
            }
        }
    }
}
