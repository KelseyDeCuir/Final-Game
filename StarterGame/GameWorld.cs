using System;
using System.Collections.Generic;
using System.Text;

namespace Ascension
{
    //Singleton design pattern
    //The game world is a singleton becasue it is the checkpoint for when the player dies
    class GameWorld
    {
        //These static lists are utilized to randomly generate items in the rooms on each floor
        public static List<Item> Floor1Items = new List<Item>();
        public static List<Item> Floor2Items = new List<Item>();
        public static List<Item> Floor3Items = new List<Item>();

        Item A = new Item("A", "B,", 1, 2, 3, Floor1Items);
        Item B = new Item("B", "C", 2, 3, 4, Floor1Items);
        Item C = new Item("C", "D", 3, 4, 5, Floor1Items);
        Item D = new Item("D", "E", 4, 5, 6, Floor1Items);
        Item E = new Item("E", "F", 5, 6, 7, Floor1Items);
        Item F = new Item("F", "G", 6, 7, 8, Floor1Items);
        Item G = new Item("G", "H", 7, 8, 9, Floor1Items);
        Item H = new Item("H", "I", 8, 9, 10, Floor1Items);
        Item I = new Item("I", "J", 9, 10, 11, Floor1Items);
        Item J = new Item("J", "K", 10, 11, 12, Floor1Items);

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
        private Floor _entrance; 
        public Floor Entrance { get { return _entrance; } }
        private Room[] Floor1Rooms = { new Room("1A", "Description 1", Floor1Items), new Room("1B", "Description 2", Floor1Items), new Room ("Hallway", "Description 3", Floor1Items), new Room("1C", "Description 4", Floor1Items), new Room("ER", "Description 5", Floor1Items ), new Room("1H", "Description 6", Floor1Items), new Room("OGBYN", "Description 7", Floor1Items), new Room("Rad5", "Description 8", Floor1Items), new Room("X-ray Exam 3", "Description 9", Floor1Items), new Room("Pediactrics", " Description 10", Floor1Items) };
        private GameWorld()
        {
            _entrance = CreateWorld();
        }

        private Floor CreateWorld()
        {
            Floor abandonedHospital = new Floor(Floor1Rooms, Floor1Items);
            return abandonedHospital;
        }

        //Method to create items in each room on each floor 
        /*private void CreateItem()
        {
            Item A = new Item("A", "B,", 1, 2, 3, Floor1Items);
            Item B = new Item("B", "C", 2, 3, 4, Floor1Items);
            Item C = new Item("C", "D", 3, 4, 5, Floor1Items);
            Item D = new Item("D", "E", 4, 5, 6, Floor1Items);
            Item E = new Item("E", "F", 5, 6, 7, Floor1Items);
            Item F = new Item("F", "G", 6, 7, 8, Floor1Items);
            Item G = new Item("G", "H", 7, 8, 9, Floor1Items);
            Item H = new Item("H", "I", 8, 9, 10, Floor1Items);
            Item I = new Item("I", "J", 9, 10, 11, Floor1Items);
            Item J = new Item("J", "K", 10, 11, 12, Floor1Items);
            
            Floor1Items = new List<Item> { A, B, C, D, E};
        */
        }
    } //{ "1A", "1B", "Hallway", "1C", "ER", "1H", "OGBYN", "OR1", "Rad5", "X-ray Exam 3", "Pediactrics" 

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

