﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Ascension
{
    public class Weapon : Item
    {
        public int damage;
        public Weapon(string name, string description, int value, int weight, int volume, int dmg, List<Item> items) : base(name, description, value, weight, volume, items)
        {
            damage = dmg;
        }
        public override string GetDescription()
        {
            return "Weapon -> " + this.damage + " " + this.Description + " W: " + this.Weight + " V: " + this.Volume + ".";
        }
    }
}
