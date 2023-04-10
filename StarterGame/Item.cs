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

        public Item(string name, string description, int value, int weight, int volume, List <Item> items)
        {
            this.Name = name;
            this.Description = description;
            this.Value = value;
            this.Weight = weight;
            this.Volume = volume;
            items.Add(this);
        }
    }
}
