﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Newtonsoft.Json;

namespace Ascension
{
    [Serializable]


    public class saveRoot
    {
        public Player player { get; set; }
    }
    [Serializable]
    public class Player : Character
    //player should take combat txt file
    {
        public bool genocide;
        private double _weightLimit;
        public double WeightLimit { get { return _weightLimit; } }
        private double _volumeLimit;
        public double VolumeLimit { get { return _volumeLimit; } }
        public double heldVolume;
        public double heldWeight;
        public int _exp;
        public GameWorld World;
        public int Exp { get { return _exp; } }
        public int _aptReq;
        public int AptReq { get { return _aptReq; } }
        public int _prevAptReq;
        Weapon stick;
        Armor jacket;

        public Player(GameWorld world) : base(world.Entrance, "player", "yourself")
        {
            _aptReq = 2;
            _prevAptReq = 1;
            _volumeLimit = 40;
            _weightLimit = 30;
            heldVolume = 0;
            heldWeight = 0;
            this.State = States.CHARCREATION; // so only the player starts in character creation
            CurrentRoom = Elevator.Instance;
            World = world;
            stick = new Weapon("stick", "A plain stick", 0, 1, 3, 5);
            Inventory.Add(stick);
            stick.Found = true;
            EquippedWeapon = stick;
            jacket = new Armor("jacket", "A musty old jacket", 0, 2, 4, 2);
            Inventory.Add(jacket);
            jacket.Found = true;
            EquippedArmor = jacket;
            Inventory.Remove(stick);
            Inventory.Remove(jacket);
            Eyriskel = 0;
            NotificationCenter.Instance.AddObserver("PlayerMovedFloors", PlayerMovedFloors);
            //world.Entrance.FloorMap[0, 1].MakeLockedRoom("stick");
            //I also think that locked rooms should be in gameworld not player 
            //issue with this line when saving self ref loop 
            //also only locks south room

            NotificationCenter.Instance.AddObserver("CharacterArrived", CharacterArrived);
        }
        private static Player _instance = null;
        public static Player Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Player(GameWorld.Instance);
                }
                return _instance;
            }
        }
        public void CharacterArrived(Notification notification)
        {
            if (notification.Object as Character != null)
            {

                Character namedChar = (Character)notification.Object;
                if (namedChar.CurrentRoom.Equals(CurrentRoom))
                {
                    InfoMessage("\n" + namedChar.Name + " arrived in the room.");
                    namedChar.inPlayerRoom = true;
                }
                else
                {
                    if (namedChar.inPlayerRoom)
                    {
                        InfoMessage("\n" + namedChar.Name + " departed.");
                        namedChar.inPlayerRoom = false;
                    }
                }
            }
        }

        public void PlayerMovedFloors(Notification notification)
        {
            Player player = (Player)notification.Object;
            if (player != null)
            {
                PastRooms.Clear();
                player.NormalMessage("\n" + "Player cleared pastrooms list.");
            }
            else
            {
                player.ErrorMessage("\n" + "Player cannot clear pastrooms list");
            }

        }

        public Player SelfRef()
        {
            return this;
        }

        public void Saveinfo()
        { //really cheap way to save ask teacher about it
            //Simpleton for now
            SaveSystem sv = new SaveSystem(this); //never actually saves just stops here
            sv.SavePlayerinfo(); 
        }
        public void Loadinfo()
        {
            Notification notification = new Notification("PlayerLoadedFile", this);
            NotificationCenter.Instance.PostNotification(notification);
            //SaveSystem sv = new SaveSystem(this);
            //Console.WriteLine(sv.LoadPlayerinfo());
            //State = States.ELEVATOR;
        }
        public override void AttackEnemy(String name)
        {

            switch (Elevator.Instance.floorLvl)
            {
                case 1:
                    if (World.U_hospitalCharacters.Exists(character => character.Name.ToLower() == name && character.inPlayerRoom))
                    {
                        Character character = World.U_hospitalCharacters.Find(character => character.Name.ToLower() == name && character.inPlayerRoom);
                        cs.Attack(character);
                    }
                    break;
                case 2:
                    if (World.U_schoolCharacters.Exists(character => character.Name.ToLower() == name && character.inPlayerRoom))
                    {
                        Character character = World.U_schoolCharacters.Find(character => character.Name.ToLower() == name && character.inPlayerRoom);
                        cs.Attack(character);
                    }
                    break;
                case 3:
                    if (World.U_hellCharacters.Exists(character => character.Name.ToLower() == name && character.inPlayerRoom))
                    {
                        Character character = World.U_hellCharacters.Find(character => character.Name.ToLower() == name && character.inPlayerRoom);
                        cs.Attack(character);
                    }
                    break;

                default:
                    break;
            }
        }
        public void Fight(String name)
        {
            switch (Elevator.Instance.floorLvl)
            {
                case 1:
                    if (World.U_hospitalCharacters.Exists(character => character.Name.ToLower() == name && character.inPlayerRoom))
                    {
                        Character character = World.U_hospitalCharacters.Find(character => character.Name.ToLower() == name && character.inPlayerRoom);
                        State = States.COMBAT;
                        character.State = States.COMBAT;
                        cs = new CombatSystem(this, character);
                        character.cs = cs;
                        cs.Attack(character);
                    }
                    if (World.Bosses[0].Name.ToLower() == name && World.Bosses[0].inPlayerRoom)
                    {
                        Character character = World.Bosses[0];
                        State = States.COMBAT;
                        character.State = States.COMBAT;
                        cs = new CombatSystem(this, character);
                        character.cs = cs;
                        cs.Attack(character);
                    }
                    break;
                case 2:
                    if (World.U_schoolCharacters.Exists(character => character.Name.ToLower() == name && character.inPlayerRoom))
                    {
                        Character character = World.U_schoolCharacters.Find(character => character.Name.ToLower() == name && character.inPlayerRoom);
                        State = States.COMBAT;
                        character.State = States.COMBAT;
                        cs = new CombatSystem(this, character);
                        character.cs = cs;
                        cs.Attack(character);
                    }
                    if (World.Bosses[1].Name.ToLower() == name && World.Bosses[1].inPlayerRoom)
                    {
                        Character character = World.Bosses[1];
                        State = States.COMBAT;
                        character.State = States.COMBAT;
                        cs = new CombatSystem(this, character);
                        character.cs = cs;
                        cs.Attack(character);
                    }
                    break;
                case 3:
                    if (World.U_hellCharacters.Exists(character => character.Name.ToLower() == name && character.inPlayerRoom))
                    {
                        Character character = World.U_hellCharacters.Find(character => character.Name.ToLower() == name && character.inPlayerRoom);
                        State = States.COMBAT;
                        character.State = States.COMBAT;
                        cs = new CombatSystem(this, character);
                        character.cs = cs;
                        cs.Attack(character);
                    }
                    if (World.Bosses[2].Name.ToLower() == name && World.Bosses[1].inPlayerRoom)
                    {
                        Character character = World.Bosses[2];
                        State = States.COMBAT;
                        character.State = States.COMBAT;
                        cs = new CombatSystem(this, character);
                        character.cs = cs;
                        cs.Attack(character);
                    }
                    break;

                default:
                    break;
            }
            if(name == ElevatorAttendant.Instance.Name && ElevatorAttendant.Instance.inPlayerRoom)
            {
                State = States.COMBAT;
                ElevatorAttendant.Instance.State = States.COMBAT;
                cs = new CombatSystem(this, ElevatorAttendant.Instance);
                ElevatorAttendant.Instance.cs = cs;
                cs.Attack(ElevatorAttendant.Instance);
            }
        }
        public void DodgeAttack()
        {
            cs.Dodge();
        }

        public void Backto()
        {
            if (PastRooms.Count > 1)
            {

                _currentRoom = PastRooms.Pop();//gets most recent past room

                NormalMessage("\n" + this.CurrentRoom.Description());
            }
            else if (PastRooms.Count == 1) {
                _currentRoom = PastRooms.Pop();//gets most recent past room

                NormalMessage("\n" + this.CurrentRoom.Description());
                State = States.ELEVATOR;
            }

            else
            {
                State = States.ELEVATOR;
                ErrorMessage("\nCan't go Back!");
            }


        }

        public void Enddialouge()
        {
            DialogueParser.Instance.enddialouge();
            this.State = States.GAME;
            DialogueParser.Instance.character.State = States.GAME;

        }

        public void continuedialouge()
        {
            DialogueParser.Instance.pl.ContinueDialouge();
        }

        //CHANGED HERE
        public void TakeAll(string secondword)
        {
            List<Item> temp = new List<Item>();

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
                foreach (Item tempItem in temp)
                {
                    CurrentRoom.items.Remove(tempItem);
                }
                temp.Clear();
            }
            else
            {
                WarningMessage("Nothing to pick up");
            }
            CurrentRoom.MonsterAttack(this);
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
            ErrorMessage(String.Format("You DIED!!!!\n Your items are on floor {0} at position ({1}, {2}).", Elevator.Instance.floorLvl, CurrentRoom.pos[0], CurrentRoom.pos[1]));
            this.Alive = true;
            CurrentHealth = aptitudeLvl.health;
            this.CurrentRoom = Elevator.Instance;
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
            CurrentRoom.MonsterAttack(this);
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
            CurrentRoom.MonsterAttack(this);
        }

        public void Drop(string itemName)
        {
            if (Inventory.Count > 0)
            {
                if (Inventory.Exists(item => item.Name.ToLower().Equals(itemName)))
                {
                    Item item = Inventory.Find(item => item.Name.ToLower().Equals(itemName));
                    InfoMessage("You set down " + item.Name);
                    CurrentRoom.items.Add(item);
                    Inventory.Remove(item);
                    heldWeight -= item.Weight;
                    heldVolume -= item.Volume;
                }
                else
                {
                    WarningMessage("No " + itemName + " to set down");
                }
            }
            else
            {
                WarningMessage("No " + itemName + " to set down");
            }
            CurrentRoom.MonsterAttack(this);
        }

        //CHANGED HERE
        public void TakeOne(string itemName)
        {//TODO: custom warrning messages based on if too heavy or if item does not exist
            if (itemName == null || itemName == "" || itemName == "all") { TakeAll(itemName); }
            else
            {
                if (CurrentRoom.items.Count > 0)
                {
                    if (CurrentRoom.items.Exists(item => item.Name.ToLower().Equals(itemName)))
                    {
                        Item item = CurrentRoom.items.Find(item => item.Name.ToLower().Equals(itemName));
                        if (heldWeight + item.Weight <= WeightLimit && heldVolume + item.Volume <= VolumeLimit)
                        {
                            InfoMessage("You picked up " + item.Name);
                            if (item.Found != true)
                            {
                                XpUp(2);
                                item.Found = true;
                            }
                            Inventory.Add(item);
                            CurrentRoom.items.Remove(item);
                            heldWeight += item.Weight;
                            heldVolume += item.Volume;
                        }
                        else
                        {
                            WarningMessage("Cannot fit " + itemName + " into your inventory.");
                        }
                    }
                    else
                    {
                        WarningMessage("There is no " + itemName + " to pick up");
                    }

                }
                CurrentRoom.MonsterAttack(this);
            }
        }

        public void equip(string SecondWord)
        {
            if (Inventory.Exists(item => item.Name.ToLower().Equals(SecondWord)))
            {
                Item item = Inventory.Find(item => item.Name.ToLower().Equals(SecondWord));
                var weapon = item as Weapon;
                var armor = item as Armor;
                if (weapon != null)
                {
                    EquipWeapon(weapon);
                }
                else if (armor != null)
                {
                    EquipArmor(armor);
                }
                else
                {
                    WarningMessage("Cannot equip " + SecondWord + " it is neither weapon not armor.");
                }
            }
            else
            {
                WarningMessage("Could not find " + SecondWord);
            }
            CurrentRoom.MonsterAttack(this);
        }

        public void unequip(string SecondWord)
        {
            if (SecondWord.Equals("armor"))
            {
                if (EquippedArmor != null)
                {
                    if (heldWeight + EquippedArmor.Weight <= WeightLimit && heldVolume + EquippedArmor.Volume <= VolumeLimit)
                    {
                        Inventory.Add(EquippedArmor);
                        InfoMessage("Unequipped " + EquippedArmor.Name);
                        heldWeight += EquippedArmor.Weight;
                        heldVolume += EquippedArmor.Volume;
                        EquippedArmor = null;
                    }
                    else
                    {
                        WarningMessage("Cannot fit " + EquippedArmor.Name + " into your inventory.");
                    }
                }
                else
                {
                    InfoMessage("Nothing equipped there.");
                }
            }
            else if (SecondWord.Equals("weapon"))
            {
                if (EquippedWeapon != null)
                {
                    if (heldWeight + EquippedWeapon.Weight <= WeightLimit && heldVolume + EquippedWeapon.Volume <= VolumeLimit)
                    {
                        Inventory.Add(EquippedWeapon);
                        InfoMessage("Unequipped " + EquippedWeapon.Name);
                        heldWeight += EquippedWeapon.Weight;
                        heldVolume += EquippedWeapon.Volume;
                        EquippedWeapon = null;
                    }
                    else
                    {
                        WarningMessage("Cannot fit " + EquippedWeapon.Name + " into your inventory.");
                    }
                }
                else
                {
                    InfoMessage("Nothing equipped there.");
                }
            }
            else
            {
                WarningMessage("Cannot unequip " + SecondWord);
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

        //TODO: Check if works
        public void Ascend()
        {
            if (World.floors[Elevator.Instance.floorLvl].Unlocked)
            {
                Notification notification = new Notification("PlayerMovedFloors", this);
                NotificationCenter.Instance.PostNotification(notification);
                CurrentFloor = World.floors[Elevator.Instance.floorLvl];
                Elevator.Instance.floorLvl += 1;
                //var pl = player as Player;
                if (this != null)
                {
                    if (Elevator.Instance.floorLvl.Equals(4))
                    {
                        WhenYouWin();
                        notification = new Notification("YouWinTrue", this);
                        NotificationCenter.Instance.PostNotification(notification);
                    }
                }
            }
            else
            {
                ErrorMessage("That floor is still locked, you must beat any bosses left on this floor.");
            }

        }


        public void Descend()
        {
            int floorNum = Elevator.Instance.floorLvl - 2;
            if (floorNum <= 0)
            {
                Notification notification = new Notification("PlayerMovedFloors", this);
                NotificationCenter.Instance.PostNotification(notification);
                floorNum = GameWorld.Instance.floors.Count + floorNum;
            }
            if (GameWorld.Instance.floors[floorNum].Unlocked)
            {
                CurrentFloor = GameWorld.Instance.floors[floorNum];
                Elevator.Instance.floorLvl = floorNum + 1;
                if (this != null)
                {
                    if (Elevator.Instance.floorLvl.Equals(4))
                    {
                        WhenYouWin();
                        Notification notification = new Notification("YouWinGenocide", this);
                        NotificationCenter.Instance.PostNotification(notification);
                    }
                }
            }
            else
            {
                ErrorMessage(GameWorld.Instance.floors[floorNum] + " is still locked, you must beat any bosses left on this floor.");
            }
        }


        
        public void enchant(string SecondWord)
        {
            //only enchants exisitng items
            if (SecondWord.Equals("weapon"))
            {
                if (EquippedWeapon != null)
                {
                    InfoMessage("You enchanted " + EquippedWeapon.Name);
                    EquippedWeapon.enchanted = true;
                }
                else
                {
                    WarningMessage("No Weapon to Enchant");
                }
            }
            else if (SecondWord.Equals("armor"))
            {
                if (EquippedArmor != null)
                {
                    InfoMessage("You enchanted " + EquippedArmor.Name);
                    EquippedArmor.enchanted = true;
                }
                else
                {
                    WarningMessage("No Armor to Enchant");
                }
            }
            else
            {
                WarningMessage("Cannot enchant that.");
            }
            CurrentRoom.MonsterAttack(this);
        }

        //TODO: Check if works
        public void levelup(string SecondWord)
        {

            if (aptPoints > 0)
            {
                Skills skills = aptitudeLvl;
                if (SecondWord.Equals("health"))
                {
                    skills.health += (int)Math.Ceiling(skills.health * .12);
                    aptPoints -= 1;
                    InfoMessage("Your health: " + skills.health + "\nAptitude Points Remaining: " + aptPoints);
                }
                else if (SecondWord.Equals("strength"))
                {
                    skills.strength += 2;
                    aptPoints -= 1;
                    InfoMessage("Your strength: " + skills.strength + "\nAptitude Points Remaining: " + aptPoints);
                }
                else if (SecondWord.Equals("intelligence"))
                {
                    skills.intelligence += 2;
                    aptPoints -= 1;
                    InfoMessage("Your intelligence: " + skills.intelligence + "\nAptitude Points Remaining: " + aptPoints);
                }
                else if (SecondWord.Equals("magic"))
                {
                    skills.magic += 2;
                    aptPoints -= 1;
                    InfoMessage("Your magic: " + skills.magic + "\nAptitude Points Remaining: " + aptPoints);
                }
                else if (SecondWord.Equals("speed"))
                {
                    skills.speed += 2;
                    aptPoints -= 1;
                    InfoMessage("Your speed: " + skills.speed + "\nAptitude Points Remaining: " + aptPoints);
                }
                else
                {
                    WarningMessage("Cannot level " + SecondWord);
                }
                aptitudeLvl = skills;
            }
            else
            {
                WarningMessage("No Apt Points to level with.");
            }


        }

        public void lookgeneral(string SecondWord)
        { //looks at room or inventory
            //how look works
            //look (room or inventory) (insert thing here)
            //(insert thing here is optional)
            if (SecondWord.Equals("room"))
            {
                CurrentRoom.Description();
                CheckCharacters();
            }
            else if (SecondWord.Equals("inventory"))
            {
                InfoMessage("\nEyriskel (coins): " + Eyriskel + "\nYour Inventory currently contains:\n" + GetInventory() +
                "\nYou have " + heldVolume + "/" + VolumeLimit + " Volume taken up.\nYou have " + heldWeight + "/" +
                WeightLimit + " Weight taken up.");
            }
            CurrentRoom.MonsterAttack(this);
        }

        public void lookSpecfic(string SecondWord, string ThirdWord)
        {
            if (SecondWord.Equals("room"))
            {
                if (CurrentRoom.items.Exists(item => item.Name.ToLower().Equals(ThirdWord)))
                {
                    Item item = CurrentRoom.items.Find(item => item.Name.ToLower().Equals(ThirdWord));
                    NormalMessage("\nItem in Room:\n" + item.Name + ": " + item.Description);
                }
                else
                {
                    WarningMessage("Could not find " + ThirdWord);
                }
            }
            else if (SecondWord.Equals("inventory"))
            {
                if (Inventory.Exists(item => item.Name.ToLower().Equals(ThirdWord)))
                {
                    Item item = Inventory.Find(item => item.Name.ToLower().Equals(ThirdWord));
                    NormalMessage("\nItem in Inventory:\n" + item.Name + ": " + item.Description);
                }
                else
                {
                    WarningMessage("Could not find " + ThirdWord);
                }
            }
            CurrentRoom.MonsterAttack(this);
        }



        public void talkto(string npc)
        {
            switch (Elevator.Instance.floorLvl)
            {
                case 1:
                    if (World.U_hospitalCharacters.Exists(character => character.Name.ToLower() == npc && character.inPlayerRoom))
                    {
                        Character character = World.U_hospitalCharacters.Find(charact => charact.Name.ToLower() == npc && charact.inPlayerRoom);
                        State = States.DIALOGUE;
                        if (character.generalDialouge != null)
                        {
                            character.State = States.DIALOGUE;
                            DialogueParser.Instance.setCurrentItems(this,character);
                            DialogueParser.Instance.readfile();
                        }
                        else {
                            WarningMessage("NPC does not have dialouge");
                        }
                        return;
                        
                    }
                    else
                    {
                        WarningMessage(npc + " is not in the room!");
                    }
                    break;
                case 2:
                    if (World.U_schoolCharacters.Exists(character => character.Name.ToLower() == npc && character.inPlayerRoom))
                    {

                        Character character = World.U_schoolCharacters.Find(charact => charact.Name.ToLower() == npc && charact.inPlayerRoom);
                        State = States.DIALOGUE;
                        if (character.generalDialouge != null)
                        {
                           
                            character.State = States.DIALOGUE;
                            DialogueParser.Instance.setCurrentItems(this, character);
                            DialogueParser.Instance.readfile();
                        }
                        else
                        {
                            WarningMessage("NPC does not have dialouge");
                        }
                        return;
                    }
                    else
                    {
                        WarningMessage(npc + " is not in the room!");
                    }
                    break;
                case 3:
                    if (World.U_hellCharacters.Exists(character => character.Name.ToLower() == npc && character.inPlayerRoom))
                    {
                        Character character = World.U_hellCharacters.Find(charact => charact.Name.ToLower() == npc && charact.inPlayerRoom);
                        State = States.DIALOGUE;
                        if (character.generalDialouge != null)
                        {
                  
                            character.State = States.DIALOGUE;
                            DialogueParser.Instance.setCurrentItems(this, character);
                            DialogueParser.Instance.readfile();
                        }
                        else
                        {
                            WarningMessage("NPC does not have dialouge");
                        }
                        return;
                    }
                    else
                    {
                        WarningMessage(npc + " is not in the room!");
                    }
                    break;
                default:
                    break;

            }
        }




        public override void WalkTo(string direction)
        {
            int[] newPos = validRoomPos(direction);
            if (newPos != null)
            {
                if (CurrentFloor.FloorMap[newPos[0], newPos[1]].Condition != null)
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
                        CheckCharacters();
                    }
                }
                else
                {
                    PastRooms.Push(CurrentRoom); //stores current room as a past room
                    CurrentRoom = CurrentFloor.FloorMap[newPos[0], newPos[1]]; //Move rooms
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
                    CheckCharacters();
                }
            }
            else
            {
                ErrorMessage("\nThere is no door to the " + direction + ".");
            }
        }
        public void CheckCharacters()
        {
            foreach (Character character in World.Bosses)
            {
                if (character.CurrentRoom.Equals(CurrentRoom))
                {
                    character.inPlayerRoom = true;
                    ErrorMessage("BOSS ROOM!"); 
                    CombatSystem cs = new CombatSystem(this, character);
                    cs.Attack(character);
                }
            }
            foreach (Character character in World.U_hospitalCharacters)
            {
                if (character.CurrentRoom.Equals(CurrentRoom))
                {
                    InfoMessage(character.Name + " is in the room.");
                    character.inPlayerRoom = true;
                }
                else
                {
                    if (character.inPlayerRoom)
                    {
                        InfoMessage(character.Name + " was left behind.");
                        character.inPlayerRoom = false;
                    }

                }
            }
            foreach (Character character in World.U_schoolCharacters)
            {
                if (character.CurrentRoom.Equals(CurrentRoom))
                {
                    InfoMessage(character.Name + " is in the room.");
                    character.inPlayerRoom = true;
                }
                else
                {
                    if (character.inPlayerRoom)
                    {
                        InfoMessage(character.Name + " was left behind.");
                        character.inPlayerRoom = false;
                    }

                }
            }
            foreach (Character character in World.U_hellCharacters)
            {
                if (character.CurrentRoom.Equals(CurrentRoom))
                {
                    InfoMessage(character.Name + " is in the room.");
                    character.inPlayerRoom = true;
                }
                else
                {
                    if (character.inPlayerRoom)
                    {
                        InfoMessage(character.Name + " was left behind.");
                        character.inPlayerRoom = false;
                    }

                }
            }
            CurrentRoom.MonsterAttack(this);
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
        public override void HitMonster()
        {
            if (CurrentRoom.monster != null)
            {
                double damage = 0;
                if (EquippedWeapon != null)
                {
                    damage = EquippedWeapon.GetDamage(this);
                    if (EquippedWeapon.enchanted) { EquippedWeapon.enchanted = false; }
                }
                else
                {
                    damage = (double)aptitudeLvl.strength / 10;
                }

                int remainingHealth = CurrentRoom.monster.GetMonster().MonsterHurt(damage);
                string status = "still alive";
                if (remainingHealth <= 0)
                {
                    status = "dead";
                    CurrentRoom.monster.GetMonster().Die();
                    Eyriskel += CurrentRoom.monster.GetMonster().Eyriskel;
                    InfoMessage(String.Format("You did {0} damage! The monster is {1}.", (int)Math.Ceiling(damage), status));
                    XpUp(Elevator.Instance.floorLvl * 2);
                    Item drop = CurrentRoom.monster.GetMonster().GetItem();
                    if (drop != null)
                    {
                        CurrentRoom.items.Add(drop);
                    }
                    CurrentRoom.monster = null;
                }
                else
                {
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
