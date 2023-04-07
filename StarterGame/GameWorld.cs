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
            Floor abandonedHospital = new Floor(new string[] {"1A", "1B", "Hallway", "1C", "ER", "1H", "OGBYN", "OR1", "Rad5", "X-ray Exam 3", "Pediactrics" });
            return abandonedHospital.FloorMap[0,0];
        }
    }

    }
            /*
            Room outside = new Room("outside the main entrance of the university");
            Room scctparking = new Room("in the parking lot at SCCT");
            Room boulevard = new Room("on the boulevard");
            Room universityParking = new Room("in the parking lot at University Hall");
            Room parkingDeck = new Room("in the parking deck");
            Room scct = new Room("in the SCCT building");
            Room theGreen = new Room("in the green in from of Schuster Center");
            Room universityHall = new Room("in University Hall");
            Room schuster = new Room("in the Schuster Center");

            outside.SetExit("west", boulevard);

            boulevard.SetExit("east", outside);
            boulevard.SetExit("south", scctparking);
            boulevard.SetExit("west", theGreen);
            boulevard.SetExit("north", universityParking);

            scctparking.SetExit("west", scct);
            scctparking.SetExit("north", boulevard);

            scct.SetExit("east", scctparking);
            scct.SetExit("north", schuster);

            schuster.SetExit("south", scct);
            schuster.SetExit("north", universityHall);
            schuster.SetExit("east", theGreen);

            theGreen.SetExit("west", schuster);
            theGreen.SetExit("east", boulevard);

            universityHall.SetExit("south", schuster);
            universityHall.SetExit("east", universityParking);

            universityParking.SetExit("south", boulevard);
            universityParking.SetExit("west", universityHall);
            universityParking.SetExit("north", parkingDeck);

            parkingDeck.SetExit("south", universityParking);

            return outside;
            */

