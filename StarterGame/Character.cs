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
        private Room _currentRoom = null;
        public Room CurrentRoom { get { return _currentRoom; } set { _currentRoom = value; } }

        public Character(Room room)
        {
            _currentRoom = room;
        }

        public void WalkTo(string direction)
        {
            Room nextRoom = this.CurrentRoom.GetExit(direction);
            if (nextRoom != null)
            {
                CurrentRoom = nextRoom; //Move rooms
                NormalMessage("\n" + this.CurrentRoom.Description());
            }
            else
            {
                ErrorMessage("\nThere is no door on " + direction);
            }
        }
        //WIP
        public String Name { set; get; }
        public String Description { get; }
        public Item[] Inventory { set; get; }
        public Double Health { set; get; }
        public Double Attack { set; get; }
        public Double Evasiveness { set; get; }
        public Boolean CanMove { set; get; }
        public Boolean Alive { set; get; }
        public Skills skillName { set; get; }
        //Thinking about what to do with this
        public Command[] actions { set; get; }

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
            ColoredMessage(message, ConsoleColor.Blue);
        }

        public void WarningMessage(string message)
        {
            ColoredMessage(message, ConsoleColor.DarkYellow);
        }

        public void ErrorMessage(string message)
        {
            ColoredMessage(message, ConsoleColor.Red);
        }
    }

}
