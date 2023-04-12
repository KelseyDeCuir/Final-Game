using System.Collections;
using System.Collections.Generic;
using System;

namespace Ascension
{
    /*
     * Spring 2023
     */
    public struct Skills
    {
        int strength;
        int health;
        int speed;
        int intelligence;
        int magic;
    }
    public class Character : ICharacter
    {
        public List<Item> Inventory { set; get; }
        public Double Health { set; get; }
        public Double Attack { set; get; }
        public Double Evasiveness { set; get; }
        public Boolean CanMove { set; get; }
        public Boolean Alive { set; get; }
        public Skills skillName { set; get; }
        //Thinking about what to do with this
        public Command[] actions { set; get; }
        private string _name = null;
        private string _description = null;
        public string Name { get { return _name; } }
        public string Description { get { return _description; } }
        private Floor _currentFloor = null; //rooms are separated by floors so that is stored instead
        public Floor CurrentFloor { get { return _currentFloor;} set { _currentFloor = value;} }
        private int[] _currentRoom = null;
        public Room CurrentRoom { get { return CurrentFloor.FloorMap[_currentRoom[0],_currentRoom[1]]; } }
        public Weapon EquippedWeapon { set; get; }
        public Armor EquippedArmor { set; get; }

        public Character(Floor floor, string name, string desc)
        {
            _currentFloor = floor;
            _currentRoom = new int[] {0,0};
            _name = name;
            _description = desc;
            Inventory = new List<Item>();
        }

        // gets the room position for the matrix if it is a valid room
        public int[] validRoomPos(string direction)
        {
            switch (direction)
            {
                case "north":
                    if (_currentRoom[1] != 0)
                    {
                        return new int[] {_currentRoom[0], _currentRoom[1] -1};
                    }
                    else
                    {
                        return null;
                    }
                case "east":
                    if (_currentRoom[0] != 1)
                    {
                        return new int[] {_currentRoom[0] +1 , _currentRoom[1]};
                    }
                    else
                    {
                        return null;
                    }
                case "south":
                    if (_currentRoom[1] != 2)
                    {
                        return new int[] {_currentRoom[0], _currentRoom[1] +1};
                    }
                    else
                    {
                        return null;
                    }
                case "west":
                    if (_currentRoom[0] != 0)
                    {
                        return new int[] {_currentRoom[0] -1, _currentRoom[1]};
                    }
                    else
                    {
                        return null;
                    }
                default:
                    return null;
            }
        }
        public void WalkTo(string direction)
        {
            int[] newPos = validRoomPos(direction);
            if (newPos != null)
            {
                _currentRoom = newPos; //Move rooms
                NormalMessage("\n" + this.CurrentRoom.Description());
            }
            else
            {
                ErrorMessage("\nThere is no door to the " + direction + ".");
            }
        }
        public string GetInventory()
        {
            if (Inventory.Count > 0)
            {
                string itemNames = "Items:";
                foreach (Item item in Inventory)
                {
                    itemNames += " " + item.Name;
                }
                return itemNames;
            }
            return "Nothing!";
        }
        public string GetEquipped()
        {
            string equipped = "";
            if (EquippedWeapon != null)
            {
                equipped += "Weapon: " + EquippedWeapon.Name;
            }
            else
            {
                equipped += "No Weapon Equipped!";
            }
            if (EquippedArmor != null)
            {
                equipped += "\nArmor: " + EquippedArmor.Name;
            }
            else
            {
                equipped += "\nNo Armor Equipped!";
            }
            return equipped;
        }
        public void OutputMessage(string message)
        {
            Console.WriteLine(message);
        }

        public void ColoredMessage(string message, ConsoleColor newColor)
        {
            ConsoleColor oldColor = Console.ForegroundColor;
            Console.ForegroundColor = newColor;
            OutputMessage(message);
            Console.ForegroundColor = oldColor;
        }

        public void NormalMessage(string message)
        {
            ColoredMessage(message, ConsoleColor.White);
        }

        public void InfoMessage(string message)
        {
            ColoredMessage(message, ConsoleColor.Cyan);
        }

        public void WarningMessage(string message)
        {
            ColoredMessage(message, ConsoleColor.Yellow);
        }

        public void ErrorMessage(string message)
        {
            ColoredMessage(message, ConsoleColor.Red);
        }
    }

}
