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
        private GameWorld World;
        private double _weightLimit;
        public double WeightLimit { get { return _weightLimit; } }
        private double _volumeLimit;
        public double VolumeLimit { get { return _volumeLimit; } }
    
        public Player(GameWorld world) : base(world.Entrance, "player", "yourself", new int[] { 0, 0 })
        {
            _volumeLimit = 40;
            _weightLimit = 30;
            this.State = States.CHARCREATION; // so only the player starts in character creation
            World = world;
        }

        public void Saveinfo() { //really cheap way to save ask teacher about it
            //Simpleton for now
            SaveSystem sv = new SaveSystem(this);
            sv.SavePlayerinfo();
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
