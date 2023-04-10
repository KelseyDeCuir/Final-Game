using System.Collections;
using System.Collections.Generic;
using System;

namespace Ascension
{
    /*
     * Spring 2023
     */
    public class Room
    {
        private Dictionary<string, Room> _exits;
        private string _tag;
        private string _generaldescription;
        public string Tag { get { return _tag; } set { _tag = value; } }
        public string GeneralDescription { get { return _generaldescription; } set { _generaldescription = value; } }
        public List<Item> items = new List<Item>();
        public Room() : this("No Tag", "No Description", null){}

        // Designated Constructor
        public Room(string tag, string description, List<Item> floorItems)
        {
            _exits = new Dictionary<string, Room>();
            this.Tag = tag;
            this.GeneralDescription = description;
            Random rnd = new Random();
            int index = rnd.Next(0, floorItems.Count);
            this.items.Add(floorItems[index]);
        }

        public void SetExit(string exitName, Room room)
        {
            _exits[exitName] = room;
        }

        public Room GetExit(string exitName)
        {
            Room room = null;
            _exits.TryGetValue(exitName, out room);
            return room;
        }

        public string GetExits()
        {
            string exitNames = "Exits: ";
            Dictionary<string, Room>.KeyCollection keys = _exits.Keys;
            foreach (string exitName in keys)
            {
                exitNames += " " + exitName;
            }

            return exitNames;
        }
        public string GetItems()
        {
            string itemNames = "Items:";
            foreach( Item item in items)
            {
                itemNames += " " + item.Name; 
            }
            return itemNames;
        }

        public string Description()
        {
            return "You are in " + this.Tag +". " + this.GeneralDescription + ". " + ItemDescription() + ".";
        }
        public string BaseDescription()
        {
            return "You are in " + this.Tag + ".\n ";
        }
        public string ItemDescription()
        {
            return "There are currently the following items in this room " + GetItems();
        }
    }
}
