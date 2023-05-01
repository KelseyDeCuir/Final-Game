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
        //player should take combat txt file
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
        public void CharacterArrived(Notification notification)
        {
            if (notification.Object as Character != null) {

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

        public void Saveinfo() { //really cheap way to save ask teacher about it
            //Simpleton for now
            SaveSystem sv = new SaveSystem(this); //never actually saves just stops here
            sv.SavePlayerinfo();
        }
        public void Loadinfo() {


            Notification notification = new Notification("PlayerLoadedFile", this);
            NotificationCenter.Instance.PostNotification(notification);
            //SaveSystem sv = new SaveSystem(this);
            //Console.WriteLine(sv.LoadPlayerinfo());
            //State = States.ELEVATOR;
            
        }
        public void Backto()
        {
            if (PastRooms.Count != 0)
            {

                _currentRoom = PastRooms.Pop();//gets most recent past room
                NormalMessage("\n" + this.CurrentRoom.Description());
            }
            else
            {
                ErrorMessage("\nCan't go Back!");
            }


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

        public void equip(string SecondWord) {
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

        public void unequip(string SecondWord) {
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
        public void Ascend() {
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


        //TODO: Check if works
        public void enchant(string SecondWord) {
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
        public void levelup(string SecondWord) { 
        
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

        public void lookgeneral(string SecondWord) { //looks at room or inventory
            //how look works
            //look (room or inventory) (insert thing here)
            //(insert thing here is optional)
            if (SecondWord.Equals("room"))
            {
                CurrentRoom.Description();
                CheckCharacters();
                //TODO: print characters in room
                //print characters in room           
            }
            else if (SecondWord.Equals("inventory")) {
                InfoMessage("\nEyriskel (coins): " + Eyriskel + "\nYour Inventory currently contains:\n" + GetInventory() +
                "\nYou have " + heldVolume + "/" + VolumeLimit + " Volume taken up.\nYou have " + heldWeight + "/" +
                WeightLimit + " Weight taken up.");
            }
            CurrentRoom.MonsterAttack(this);
        }

        public void lookSpecfic(string SecondWord,string ThirdWord) {
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
                        CheckCharacters();
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
