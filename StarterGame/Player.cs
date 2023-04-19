using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace Ascension
{
    [Serializable]
    public class Player : Character
    {
        private double _weightLimit;
        public double WeightLimit { get { return _weightLimit; } }
        private double _volumeLimit;
        public double VolumeLimit { get { return _volumeLimit; } }
        public double heldVolume;
        public double heldWeight;
        private int _exp;
        private GameWorld World;
        public int Exp {get {return _exp; } }
        private int _aptReq;
        public int AptReq { get { return _aptReq; } }
        private int _prevAptReq;
        Weapon stick;
        Armor jacket;

        public Player(GameWorld world) : base(world.Entrance, "player", "yourself", new int[] { 0, 0 })
        {
            _aptReq = 2;
            _prevAptReq = 1;
            _volumeLimit = 40;
            _weightLimit = 30;
            heldVolume = 0;
            heldWeight = 0;
            this.State = States.CHARCREATION; // so only the player starts in character creation
            World = world;
            stick = new Weapon("stick", "A plain stick", 0, 1, 3, 2, Inventory);
            stick.Found = true;
            EquippedWeapon = stick;
            jacket = new Armor("jacket", "A musty old jacket", 0, 2, 4, 1, Inventory);
            jacket.Found = true;
            EquippedArmor = jacket;
            Inventory.Remove(stick);
            Inventory.Remove(jacket);
        }

        public void Saveinfo() { //really cheap way to save ask teacher about it
            //Simpleton for now
            SaveSystem sv = new SaveSystem(this);
            sv.SavePlayerinfo();
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
            this.InfoMessage("+" + i + " Aptitude Points.\nYou now have " + this.aptPoints + " Aptitude Points.You get your next Aptitude point at " + _aptReq + " EXP.");
        }

        public override void WalkTo(string direction) 
        {
            int[] newPos = validRoomPos(direction);
            if (newPos != null)
            {
                PastRooms.Push(CurrentRoom); //stores current room as a past room
                _currentRoom = newPos; //Move rooms
                if (_currentRoom[0] == 0 && _currentRoom[1] == 0)
                {
                    State = States.ELEVATOR;
                }
                else if (State == States.ELEVATOR)
                {
                    State = States.GAME;
                }
                NormalMessage("\n" + this.CurrentRoom.Description());
            }
            else
            {
                ErrorMessage("\nThere is no door to the " + direction + ".");
            }
        }
    }
}
