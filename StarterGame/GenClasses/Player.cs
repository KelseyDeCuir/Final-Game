using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using System.Text;
using Newtonsoft.Json;

namespace Ascension
{
    [Serializable]
    public class Player : Character
    {
        public bool genocide;
        private double _weightLimit;
        public double WeightLimit { get { return _weightLimit; } }
        private double _volumeLimit;
        public double VolumeLimit { get { return _volumeLimit; } }
        public double heldVolume;
        public double heldWeight;
        private int _exp;
        public GameWorld World;
        public int Exp {get {return _exp; } }
        private int _aptReq;
        public int AptReq { get { return _aptReq; } }
        private int _prevAptReq;
        Weapon stick;
        Armor jacket;

        public Player(GameWorld world) : base(world.Entrance, "player", "yourself")
        {
            Character me = this;
            _aptReq = 2;
            _prevAptReq = 1;
            _volumeLimit = 40;
            _weightLimit = 30;
            heldVolume = 0;
            heldWeight = 0;
            this.State = States.CHARCREATION; // so only the player starts in character creation
            CurrentRoom = Elevator.Instance;
            World = world;
            stick = new Weapon("stick", "A plain stick", 0, 1, 3, 5, Inventory);
            stick.Found = true;
            EquippedWeapon = stick;
            jacket = new Armor("jacket", "A musty old jacket", 0, 2, 4, 2, Inventory);
            jacket.Found = true;
            EquippedArmor = jacket;
            Inventory.Remove(stick);
            Inventory.Remove(jacket);
            Eyriskel = 0;
            //world.Entrance.FloorMap[0, 1].MakeLockedRoom("stick");
            //I also think that locked rooms should be in gameworld not player 
            //issue with this line when saving self ref loop 
            //also only locks south room

        }
        public Player SelfRef()
        {
            return this;
        }

        public void Saveinfo() { //really cheap way to save ask teacher about it
            //Simpleton for now
            SaveSystem sv = new SaveSystem(this); //never actually saves just stops here
            sv.SavePlayerinfo();
        }
        public void Loadinfo() {
            SaveSystem sv = new SaveSystem(this);
            Console.WriteLine(sv.LoadPlayerinfo());
            
        }

        //CHANGED HERE
        public void TakeAll(string secondword) {
            List<Item> temp= new List<Item>();
       
                if (CurrentRoom.items.Count > 0)
                {
                    foreach (Item item in CurrentRoom.items) //gettign an exception her due to removeing while modifying
                 
                    {
                        if (heldWeight + item.Weight <= WeightLimit && heldVolume + item.Volume <= VolumeLimit)
                        {

                            Notification notification = new Notification("ItemObtained", this);
                            NotificationCenter.Instance.PostNotification(notification);
                            InfoMessage("You picked up " + item.Name);
                            if (item.Found != true)
                            {
                                XpUp(2);
                                item.Found = true;
                            }
                            Inventory.Add(item);
                            temp.Add(item); 
                  
                            heldWeight += item.Weight;
                            heldVolume += item.Volume;
                        }
                        else
                        {
                            WarningMessage("Cannot fit " + item.Name + " into your inventory.");
                            break;
                        }
                    }
                    foreach (Item tempItem in temp) { 
                        CurrentRoom.items.Remove(tempItem); 
                    }
                    temp.Clear();
                }
                else
                {
                    WarningMessage("Nothing to pick up");
                }
            }

        public override void Die(Character killer)
        {
            var plForXp = killer as Player;
            this.Alive = false;
            if (plForXp != null)
            {
                plForXp.Eyriskel += this.Eyriskel;
            }
            foreach (Item item in Inventory)
            {
                CurrentRoom.items.Add(item);
            }
            Inventory.Clear();
            heldVolume = 0;
            heldWeight = 0;
            ElevatorAttendant.Instance.NameAttendant(this.Name);
            this.Alive = true;
            CurrentHealth = aptitudeLvl.health;
            this.State = States.CHARCREATION;
        }

        public override void EquipWeapon(Weapon weapon)
        {
            if (EquippedWeapon != null)
            {
                Inventory.Add(EquippedWeapon);
                heldWeight += EquippedWeapon.Weight;
                heldVolume += EquippedWeapon.Volume;

            }
            EquippedWeapon = weapon;
            Inventory.Remove(weapon);
            heldWeight -= weapon.Weight;
            heldVolume -= weapon.Volume;
            InfoMessage("You Equipped the weapon " + weapon.Name);
        }

        public override void EquipArmor(Armor armor)
        {
            if (EquippedArmor != null)
            {
                Inventory.Add(EquippedArmor);
                heldWeight += EquippedArmor.Weight;
                heldVolume += EquippedArmor.Volume;
            }
            EquippedArmor = armor;
            Inventory.Remove(armor);
            heldWeight -= armor.Weight;
            heldVolume -= armor.Volume;
            InfoMessage("You Equipped the armor " + armor.Name);
        }

        //CHANGED HERE
        public void TakeOne(string item)
        {//TODO: custom warrning messages based on if too heavy or if item does not exist
            if (item == null || item == "" || item == "all") { TakeAll(item); }
            else { 
            if (CurrentRoom.items.Count > 0) {
                foreach (Item i in CurrentRoom.items)
                {
                    if (i.Name == item)
                    {
                        if (heldWeight + i.Weight <= WeightLimit && heldVolume + i.Volume <= VolumeLimit)
                        {
                            InfoMessage("You picked up " + i.Name);
                            if (i.Found != true)
                            {
                                XpUp(2);
                                i.Found = true;
                            }
                            Inventory.Add(i);
                            CurrentRoom.items.Remove(i);
                            heldWeight += i.Weight;
                            heldVolume += i.Volume;
                            break;
                        }
                            else
                            {
                                WarningMessage("Cannot fit " + item + " into your inventory.");
                                break;
                            }
                    }
                }
                }
            }
            CurrentRoom.MonsterAttack(this);
        }


        public void XpUp(int exp)
        {
            _exp += exp;
            this.InfoMessage("+" + exp + " EXP.\nYou now have " + this.Exp + " EXP.");
            int i = 0;
            while (_exp >= _aptReq)
            {
                aptPoints += 1;
                _aptReq += _prevAptReq;
                _prevAptReq = _aptReq - _prevAptReq;
                i++;
            }
            this.InfoMessage("+" + i + " Aptitude Points.\nYou now have " + this.aptPoints + " Aptitude Points. You get your next Aptitude point at " + _aptReq + " EXP.");
        }
            

        public override void WalkTo(string direction) 
        {
            int[] newPos = validRoomPos(direction);
            if (newPos != null)
            {
                if(CurrentFloor.FloorMap[newPos[0], newPos[1]].Condition != null)
                {
                    if (CurrentFloor.FloorMap[newPos[0], newPos[1]].Condition(this))
                    {
                        PastRooms.Push(CurrentRoom); //stores current room as a past room
                        CurrentRoom = CurrentFloor.FloorMap[newPos[0], newPos[1]]; //Move rooms
                        Notification notification = new Notification("PlayerMovedRooms", this);
                        NotificationCenter.Instance.PostNotification(notification);
                        if (CurrentRoom.pos[0] == 0 && CurrentRoom.pos[1] == 0)
                        {
                            notification = new Notification("PlayerEnteredElevator", this);
                            NotificationCenter.Instance.PostNotification(notification);
                            State = States.ELEVATOR;
                        }
                        else if (State == States.ELEVATOR)
                        {
                            State = States.GAME;
                        }
                        NormalMessage("\n" + this.CurrentRoom.Description());
                    }
                }
                else
                {
                    PastRooms.Push(CurrentRoom); //stores current room as a past room
                CurrentRoom = CurrentFloor.FloorMap[newPos[0],newPos[1]]; //Move rooms
                Notification notification = new Notification("PlayerMovedRooms", this);
                NotificationCenter.Instance.PostNotification(notification); //Move rooms
                if (CurrentRoom.pos[0] == 0 && CurrentRoom.pos[1] == 0)
                {
                    notification = new Notification("PlayerEnteredElevator", this);
                    NotificationCenter.Instance.PostNotification(notification);
                    State = States.ELEVATOR;
                }
                else if (State == States.ELEVATOR)
                {
                    State = States.GAME;
                }
                NormalMessage("\n" + this.CurrentRoom.Description());
                }
            }
            else
            {
                ErrorMessage("\nThere is no door to the " + direction + ".");
            }
        }
        public void WhenYouWin()
        {
            if (genocide)
            {
                ErrorMessage("YOU MONSTER.");
            }
            else
            {
                InfoMessage("You're finally Free");
            }
        }
        public void HitMonster()
        {
            if(CurrentRoom.monster != null)
            {
                double damage = EquippedWeapon.GetDamage(this);
                if(EquippedWeapon.enchanted) { EquippedWeapon.enchanted = false; }
                int remainingHealth = CurrentRoom.monster.GetMonster().MonsterHurt(damage);
                string status = "still alive";
                if(remainingHealth <= 0)
                {
                    status = "dead";
                    CurrentRoom.monster.GetMonster().Die();
                    Eyriskel += CurrentRoom.monster.GetMonster().Eyriskel;
                    InfoMessage(String.Format("You did {0} damage! The monster is {1}.", (int)Math.Ceiling(damage), status));
                    XpUp(Elevator.Instance.floorLvl * 2);
                    Item drop = CurrentRoom.monster.GetMonster().GetItem();
                    if(drop != null)
                    {
                        CurrentRoom.items.Add(drop);
                    }
                    CurrentRoom.monster = null;
                }
                else{
                    InfoMessage(String.Format("You did {0} damage! The monster is {1}.", (int)Math.Ceiling(damage), status));
                }
                
            }
            else
            {
                WarningMessage("There's no monster to hit!");
            }
            CurrentRoom.MonsterAttack(this);
        }
    }
}
