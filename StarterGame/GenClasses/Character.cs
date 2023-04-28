using System.Collections;
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
        public int CurrentHealth; // stores current health, automatically goes to max health on level
        //actions currently do not store or do anything
        //the intended purpose for actions is to store
        //comamands for npcs based on their personality
        public Command[] actions { set; get; }
        protected string _name = null;
        private string _description = null;
        public string Name { get { return _name; } }
        public string Description { get { return _description; } }
        private Floor _currentFloor = null; //rooms are separated by floors so that is stored instead
        public Floor CurrentFloor { get { return _currentFloor;} set { _currentFloor = value;} }
        protected Room _currentRoom;
        public Room CurrentRoom { get { return _currentRoom; } set { _currentRoom = value; } }
        //PastRoom Locations, 
        public Stack<Room> PastRooms = new Stack<Room>();
        public Weapon EquippedWeapon { set; get; }
        public Armor EquippedArmor { set; get; }
        public int Eyriskel { set; get; }
        public int aptPoints;
        BossDelegate bossDelegate;
        public string textfile;

        public Character(Floor floor, string name, string desc)
        {
            _currentFloor = floor;
            //current default pos for all chars is in elevator
            //_currentRoom = room;
            _name = name;
            _description = desc;
            Inventory = new List<Item>();
            State = States.GAME;
            aptitudeLvl = new Skills(10, 100, 10, 10, 10);
            aptPoints = 1;
            CurrentHealth = aptitudeLvl.health;
            Eyriskel = 5;
            //A bit worried this might affect all instances 
            //NotificationCenter.Instance.AddObserver("PlayerEndedDialouge", PlayerEndedDialouge);

        }

        // gets the room position for the matrix if it is a valid room
        public int[] validRoomPos(string direction)
        {
            switch (direction)
            {
                case "north":
                    if (_currentRoom.pos[1] != 0)
                    {
                        return new int[] {_currentRoom.pos[0], _currentRoom.pos[1] -1};
                    }
                    else
                    {
                        return null;
                    }
                case "east":
                    if (_currentRoom.pos[0] != 1)
                    {
                        return new int[] {_currentRoom.pos[0] +1 , _currentRoom.pos[1]};
                    }
                    else
                    {
                        return null;
                    }
                case "south":
                    if (_currentRoom.pos[1] != 2)
                    {
                        return new int[] {_currentRoom.pos[0], _currentRoom.pos[1] +1};
                    }
                    else
                    {
                        return null;
                    }
                case "west":
                    if (_currentRoom.pos[0] != 0)
                    {
                        return new int[] {_currentRoom.pos[0] -1, _currentRoom.pos[1]};
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
                _currentRoom = CurrentFloor.FloorMap[newPos[0], newPos[1]]; //Move rooms
                //NormalMessage("\n" + this.CurrentRoom.Description());
            }
            else
            {
                //ErrorMessage("\nThere is no door to the " + direction + ".");
            }
        }
        public void Backto()
        {
            if (PastRooms.Count != 0 ) 
            {
              
                _currentRoom = PastRooms.Pop();//gets most recent past room
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
                    itemNames += ": " + item.GetDescription(this);
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
                equipped += "Weapon: " + EquippedWeapon.GetDescription(this);
            }
            else
            {
                equipped += "No Weapon Equipped!";
            }
            if (EquippedArmor != null)
            {
                equipped += "\nArmor: " + EquippedArmor.GetDescription(this);
            }
            else
            {
                equipped += "\nNo Armor Equipped!";
            }
            return equipped;
        }

        public virtual void EquipWeapon(Weapon weapon)
        {
            if (EquippedWeapon != null)
            {
                Inventory.Add(EquippedWeapon);
            }
            EquippedWeapon = weapon;
            Inventory.Remove(weapon);
            InfoMessage("You Equipped the weapon " + weapon.Name);
        }

        public virtual void EquipArmor(Armor armor)
        {
            if (EquippedArmor != null)
            {
                Inventory.Add(EquippedArmor);
            }
            EquippedArmor = armor;
            Inventory.Remove(armor);
            InfoMessage("You Equipped the armor " + armor.Name);
        }

        public int TakeDamage(Character attacker, double damage)
        {
            int damagetoTake = 0;
            if (EquippedArmor != null)
            {
                damagetoTake = (int)Math.Ceiling((double)damage * (1 - (double)aptitudeLvl.speed / 100) - EquippedArmor.GetDefense(this));
            }
            else
            {
                damagetoTake = (int)Math.Ceiling((double)damage * (1 - (double)aptitudeLvl.speed / 100));
            }
            if (damagetoTake > 0) {
                CurrentHealth -= damagetoTake;
                if(CurrentHealth < 0)
                {
                    CurrentHealth = 0;
                }
                this.InfoMessage(this.Name + " health: " + CurrentHealth);
                if(CurrentHealth == 0)
                {
                    var pl = this as Player;
                    if (pl == null)
                    {
                        Die(attacker);
                    }
                    else
                    {
                        pl.Die(attacker);
                    }
                }
                return damagetoTake;
            }
            else
            {
                return 0;
            }
        }

        public virtual void Die(Character killer)
        {
            var plForXp = killer as Player;
            this.Alive = false;
            if (bossDelegate != null)
            {
                bossDelegate();
                if (plForXp != null)
                {
                    plForXp.XpUp(15);
                }
            }
            else
            {
                if (plForXp != null)
                {
                    plForXp.XpUp(5);
                }
            }
            if (plForXp != null)
            {
                plForXp.Eyriskel += this.Eyriskel;
            }
        }
        public void MakeBoss(Floor floor, int moneyBonus)
        {
            var obj = new Boss(floor);
            this.Eyriskel += moneyBonus;
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
        public void AchievementMessage(string message)
        {
            ColoredMessage(message, ConsoleColor.Magenta);
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
