using System;
using System.Collections.Generic;
using System.Text;

namespace Ascension
{
    //Singleton design pattern
    //The elevator class needs to be a singleton because it is a constant throughout the entire game
    //Should elevator store all floors?
    //Elevator needs GoUp command
    public class Elevator : Room
    {
        public int floorLvl;
        private static Elevator _instance = null;
        public static Elevator Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Elevator("The Elevator", "Elevator Description");
                }
                return _instance;
            }
        }
        public Elevator(string name, string description) : base(name, description, new List<Item>())
        {
            pos = new int[] { 0, 0 };
            floorLvl = 1;
        }
        public override string Description() //overrides because there are never any items in the elevator save for the shop
        {
            return "You are in " + this.Tag + ". " + this.GeneralDescription + ". " + "\n\nYou can go through the following exits\n" + GetExits();
        }
        public override string GetItems() //grabs names of the items in the room
        {
            string itemNames = "Items:";
            if (items.Count > 0)
            {
                foreach (Item item in items)
                {
                    itemNames += "\n " + item.Name + " Eyriskel: " + (item.Value+5);
                }
                itemNames += ".";
            }
            else
            {
                itemNames = "No items in the shop.";
            }
            return itemNames;
        }
    }
}
