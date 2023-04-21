﻿using System.Collections;
using System.Collections.Generic;
using System;

namespace Ascension
{
    /*
     * Spring 2023
     */
    public class Room
    {
        private string _tag;
        private string _generaldescription;
        public int [] pos { get; set; }
        public string Tag { get { return _tag; } set { _tag = value; } }
        public string GeneralDescription { get { return _generaldescription; } set { _generaldescription = value; } }
        public List<Item> items = new List<Item>();
       
        public Room() : this("No Tag", "No Description", new List<Item>()){}

        // Designated Constructor
        public Room(string tag, string description, List<Item> floorItems)
        {
            this.Tag = tag;
            this.GeneralDescription = description;
            if (floorItems.Count > 0)
            {
                Random rnd = new Random();
                int rem;
                int itemsToGen = rnd.Next(0, Math.DivRem(floorItems.Count, 3, out rem) + 1);
                for (int i = 0; i < itemsToGen; i++)
                {
                    int index = rnd.Next(0, floorItems.Count);
                    this.items.Add(floorItems[index]);
                }
            }
        }
        public string GetExits()
        {
            string exitNames = "Exits:"; // Changed to get the exits based on rooms position on the map because the movement system was changed.
            List<string> exits = new List<string>();
            if (pos[1] != 0)
            {
                exits.Add("north");
            }
            if (pos[0] != 1)
            {
                exits.Add("east");
            }
            if (pos[1] != 2)
            {
                exits.Add("south");
            }
            if (pos[0] != 0)
            {
                exits.Add("west");
            }
            foreach (string exitName in exits)
            {
                exitNames += " " + exitName;
            }
            exitNames += "\n";
            return exitNames;
        }
        public string GetItems() //grabs names of the items in the room
        {
            string itemNames = "Items:";
            if (items.Count > 0)
            {
                foreach (Item item in items)
                {
                    itemNames += " " + item.Name;
                }
                itemNames += ".";
            }
            else
            {
                itemNames = "No items in this room.";
            }
            return itemNames;
        }

        public virtual string Description() //virtual so elevator can override it
        {
            return "You are in " + this.Tag +". " + this.GeneralDescription + ". " + ItemDescription() + "\n\nYou can go through the following exits:\n\n" + GetExits();
        }
        public string BaseDescription()
        {
            return "You are in " + this.Tag + ".\n ";
        }
        public string ItemDescription()
        {
            return "There are currently the following items in this room:\n\n" + GetItems();
        }
    }
}
