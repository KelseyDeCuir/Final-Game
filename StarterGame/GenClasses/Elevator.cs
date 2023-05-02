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
            Weapon sword = new Weapon("sword", "A plain sword.", 7, 3, 2, 10);
            Armor shield = new Armor("shield", "A small shield.", 8, 2, 4, 7);
            Weapon staff = new Weapon("staff", "A heavy staff.", 12, 6, 3, 14);
            Armor helm = new Armor("helm", "How this is better than a shield, that's best not to ask.", 13, 4, 2, 10);
            Weapon clarent = new Weapon("clarent", "Arthur's sword of peace was not made for war... but you have no choice.", 95, 10, 10, 30);
            items.Add(sword);
            items.Add(shield);
            items.Add(staff);
            items.Add(helm);
            items.Add(clarent);
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
