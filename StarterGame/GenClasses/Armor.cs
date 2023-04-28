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
        public Armor(string name, string description, int value, int weight, int volume, int defense) : base(name, description, value, weight, volume)
        {
            this.defense = defense;
        }
        public override string GetDescription(Character character)
        {
            return "Armor -> " + this.GetDefense(character) + " " + this.Name + " " + this.Description + " W: " + this.Weight + " V: " + this.Volume + " E: " + this.enchanted + ".";
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
