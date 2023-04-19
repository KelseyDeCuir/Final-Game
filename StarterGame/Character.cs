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
        public int strength;
        public int health;
        public int speed;
        public int intelligence;
        public int magic;
        public Skills(int str, int hlt, int spd, int itl, int mag)
        {
            this.strength = str;
            this.health = hlt;
            this.speed = spd;
            this.intelligence = itl;
            this.magic = mag;
        }
    }
    public class Character : ICharacter
    {
        public States State;
        public List<Item> Inventory { set; get; }
        public Boolean CanMove { set; get; }
        public Boolean Alive { set; get; }
        public Skills aptitudeLvl { set; get; }
        //actions currently do not store or do anything
        //the intended purpose for actions is to store
        //comamands for npcs based on their personality
        public Command[] actions { set; get; }
        private string _name = null;
        private string _description = null;
        public string Name { get { return _name; } }
        public string Description { get { return _description; } }
        private Floor _currentFloor = null; //rooms are separated by floors so that is stored instead
        public Floor CurrentFloor { get { return _currentFloor;} set { _currentFloor = value;} }
        protected int[] _currentRoom = null;
        public Room CurrentRoom { get { return CurrentFloor.FloorMap[_currentRoom[0],_currentRoom[1]]; } }
        //PastRoom Locations, 
        public Stack<Room> PastRooms = new Stack<Room>();

        public Weapon EquippedWeapon { set; get; }
        public Armor EquippedArmor { set; get; }
        public int aptPoints;
        BossDelegate bossDelegate;

        public Character(Floor floor, string name, string desc, int[]pos)
        {
            _currentFloor = floor;
            //current default pos for all chars is in elevator
            _currentRoom = pos;
            _name = name;
            _description = desc;
            Inventory = new List<Item>();
            State = States.GAME;
            aptitudeLvl = new Skills(10, 100, 10, 10, 10);
            aptPoints = 1;
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
        public virtual void WalkTo(string direction) //virtual so that it can be overridden because no characters but the player and elevator attendant can be in the 0,0 position
        {
            int[] newPos = validRoomPos(direction);
            if (newPos != null && !(newPos[0] == 0 && newPos[1] == 0))
            {
                PastRooms.Push(CurrentRoom); //stores current room as a past room
                _currentRoom = newPos; //Move rooms
                NormalMessage("\n" + this.CurrentRoom.Description());
            }
            else
            {
                ErrorMessage("\nThere is no door to the " + direction + ".");
            }
        }
        public void Backto()
        {
            if (PastRooms.Count != 0 ) 
            {
              
                _currentRoom = PastRooms.Pop().pos;//gets pos of most recent past room
                NormalMessage("\n" + this.CurrentRoom.Description());
            }
            else 
            {
                ErrorMessage("\nCan't go Back!");
            }


        }

        public void SetName(string name)
        {
            _name = name;
        }

        public string GetStats()
        {
            string rtnString = "";
            rtnString += "\tHealth: " + aptitudeLvl.health;
            rtnString += "\n\tStrength: " + aptitudeLvl.strength;
            rtnString += "\n\tSpeed: " + aptitudeLvl.speed;
            rtnString += "\n\tIntelligence: " + aptitudeLvl.intelligence;
            rtnString += "\n\tMagic: " + aptitudeLvl.magic;
            return rtnString;
        }

        public string GetInventory()
        {
            if (Inventory.Count > 0)
            {
                string itemNames = "Items:";
                foreach (Item item in Inventory)
                {
                    itemNames += "\n\t" + item.Name;
                    itemNames += ": " + item.GetDescription();
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
                equipped += "Weapon: " + EquippedWeapon.Name + " -> " + EquippedWeapon.damage + " Damage";
            }
            else
            {
                equipped += "No Weapon Equipped!";
            }
            if (EquippedArmor != null)
            {
                equipped += "\nArmor: " + EquippedArmor.Name + " -> " + EquippedArmor.defense + " Defense";
            }
            else
            {
                equipped += "\nNo Armor Equipped!";
            }
            return equipped;
        }

        public void MakeBoss(Floor floor)
        {
            var obj = new Boss(floor);
            bossDelegate = new BossDelegate(obj.UnlockFloor);
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
        public void Die()
        {
            this.Alive = false;
            if(bossDelegate != null)
            {
                bossDelegate();
            }
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

    public delegate void BossDelegate();
    public class Boss
    {
        public Floor nextFloor;
        public Boss(Floor nxtFlr)
        {
            nextFloor = nxtFlr;
        }
        public void UnlockFloor()
        {
            
            if (nextFloor != null)
            {
                nextFloor.Unlocked = true;
            }
        }
    }

}
