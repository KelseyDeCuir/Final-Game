using System;
using System.Collections.Generic;
using System.Text;

namespace Ascension
{
    //Singleton design pattern
    //The game world is a singleton becasue it is the checkpoint for when the player dies
    class GameWorld
    {
        private static GameWorld _instance = null;
        public static GameWorld Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new GameWorld();
                }
                return _instance;
            }
        }
        private Room _entrance; 
        public Room Entrance { get { return _entrance; } }
        private GameWorld()
        {
            _entrance = CreateWorld();
        }

        private Room CreateWorld()
        {

        }

    }
}
