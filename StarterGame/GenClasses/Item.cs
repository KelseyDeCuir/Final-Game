﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Ascension
{
    public class Item : iItems
    {
        public string Name { set; get; }
        public string Description { set; get; }
        public int Value { set; get; }
        public int Weight { set; get; }
        public int Volume { set; get; }

        private bool _found;
        public bool Found { get { return _found; } set { _found = value; } }

        public Item(string name, string description, int value, int weight, int volume)
        {
            this.Name = name;
            this.Description = description;
            this.Value = value;
            this.Weight = weight;
            this.Volume = volume;
            this._found = false;
          
        }
        public virtual string GetDescription(Character character)
        {
            return this.Description + " W: " + this.Weight + " V: " + this.Volume + " Eyriskel: " + this.Value + ".";
        }
    }
}
