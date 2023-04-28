using System.Collections;
using System.Collections.Generic;
using System;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Ascension
{
    /*
     * Spring 2023
     */
    public class Room
    {
        private string _tag;
        private string _generaldescription;
        public int[] pos { get; set; }
        public string Tag { get { return _tag; } set { _tag = value; } }
        public string GeneralDescription { get { return _generaldescription; } set { _generaldescription = value; } }
        public List<Item> items = new List<Item>();
        public string reqItemName;
        public int type= 0;
        [Newtonsoft.Json.JsonIgnore]
        public Conditional conditional;
        public MonsterBuilder monsterBuilder;
        public RoomMonster monster;
        [Newtonsoft.Json.JsonIgnore]
        public RoomCondition Condition;

        [System.Runtime.Serialization.OnDeserialized]
        void OnDeserialized(System.Runtime.Serialization.StreamingContext context)
        {
            switch (type)
            {
                case 1:
                    conditional = new BossRoom(reqItemName);
                    break;
                case 2:
                    conditional = new LockedRoom(reqItemName);
                    break;
                default:
                    break;
            }
            if(this.conditional != null)
            {
                this.Condition = this.conditional.TryEnter;
            }
        }

        public Room() : this("No Tag", "No Description", new List<Item>()) { }

        // Designated Constructor
        public Room(string tag, string description, List<Item> floorItems)
        {
            this.Tag = tag;
            this.GeneralDescription = description;
            if (floorItems.Count > 0)
            {
                Random rnd = new Random();
                int rem;
                int itemsToGen = rnd.Next(1, 3);
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
        public virtual string GetItems() //grabs names of the items in the room
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
        { // has a chance of generating a monster anytime the player gets the description of the room (usually on entrance but can spam look to force spawns)
            Random rnd = new Random();
            double chance = rnd.NextDouble();
            if (monster == null && chance > .75)
            {
                monster = new RoomMonster(monsterBuilder);
                monster.BuildMonster();
            }
            if (monster != null)
            {
                return "You are in " + this.Tag + " on floor " + Elevator.Instance.floorLvl + ". " + this.GeneralDescription + " There is " + this.monster.GetMonster().name + " in this room!!! " + ItemDescription() + "\n\nYou can go through the following exits:\n\n" + GetExits();
            }
            else
            {
                return "You are in " + this.Tag + " on floor " + Elevator.Instance.floorLvl + ". " + this.GeneralDescription + " " + ItemDescription() + "\n\nYou can go through the following exits:\n\n" + GetExits();
            }
        }
        public string BaseDescription()
        {
            return "You are in " + this.Tag + ".\n ";
        }
        public string ItemDescription()
        {
            return "There are currently the following items in this room:\n\n" + GetItems();
        }

        public void MakeBossRoom(string reqItem)
        {
            conditional = new BossRoom(reqItem);
            Condition = conditional.TryEnter;
            type = 1;
            reqItemName = reqItem;
        }
        public void MakeLockedRoom(string reqItem)
        {
            type = 2;
            conditional = new LockedRoom(reqItem);
            Condition = conditional.TryEnter;
            reqItemName = reqItem;
        }

        public void MonsterAttack(Player player)
        {
            if (monster != null)
            {
                player.InfoMessage(String.Format("You took {0} damage from a monster!", player.TakeDamage(player, monster.GetMonster().MonsterAttacks())));
            }
        }
    }

    public delegate bool RoomCondition(Player player);
    public abstract class Conditional
    {
        public string _reqItemName;
        public Conditional(string reqItemName)
        {
            _reqItemName = reqItemName;
        }
        public abstract bool TryEnter(Player player);
    }
    public partial class BossRoom : Conditional
    {
        public BossRoom(string reqItemName) : base(reqItemName)
        {
        }
        public override bool TryEnter(Player player)
        {
            if(player.Inventory.Exists(item => item.Name.ToLower().Equals(this._reqItemName))){
                return true;
            }
            else
            {
                player.ErrorMessage("You cannot enter the boss room without " + _reqItemName + " in your inventory.");
                return false;
            }
        }
    }

    public partial class LockedRoom : Conditional
    {
        bool locked;
        public LockedRoom(string reqItemName) : base(reqItemName)
        {
            locked = true;
        }
        public override bool TryEnter(Player player)
        {
            if (locked)
            {
                if (player.Inventory.Exists(item => item.Name.ToLower().Equals(this._reqItemName)))
                {
                    player.InfoMessage("You unlocked the room and entered.");
                    this.locked = false;
                    return true;
                }
                else
                {
                    player.ErrorMessage("You cannot enter the room without " + _reqItemName + " in your inventory.");
                    return false;
                }
            }
            return true;
        }
    }
}
