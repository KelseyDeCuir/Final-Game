using System;
using System.Collections.Generic;
using System.Text;

namespace Ascension
{
    //Singleton design pattern
    //The game world is a singleton becasue it is the checkpoint for when the player dies
    public class GameWorld
    {
        //These static character lists are utilized to randomly generate characters in the rooms on each floor
        public static Floor abandonedHospital;
        public static Floor abandonedSchool;
        public static Floor hell;
        public static Floor winZone;
        public List<Floor> floors;
        //Defined global floor variable
        //These static lists are utilized to randomly generate items in the rooms on each floor
        public static List<Item> Floor1Items = new List<Item>();
        public static List<Item> Floor2Items = new List<Item>();
        public static List<Item> Floor3Items = new List<Item>();

        //These lists are utilized to generate characters 
        public List<Character> A_hospitalCharacters = new List<Character>();
        public List<Character> U_hospitalCharacters = new List<Character>();
        public List<Character> A_schoolCharacters = new List<Character>();
        public List<Character> U_schoolCharacters = new List<Character>();
        public List<Character> A_hellCharacters = new List<Character>();
        public List<Character> U_hellCharacters = new List<Character>();

        public void GenCharacters(Floor floor, List<Character> A, List<Character> U)
        {
            Random rnd = new Random();
            int charsToGen = rnd.Next(1, 7);
            for (int i = 0; i < charsToGen; i++)
            {
                int index = rnd.Next(0, A.Count);
                U.Add(A[index]);
                int rpos1 = rnd.Next(0, 2);
                int rpos2 = rnd.Next(0, 3);
                while(rpos1 == 0 && rpos2 == 0)
                {
                    rpos1 = rnd.Next(0, 2);
                    rpos2 = rnd.Next(0, 3);
                }
                A[index].CurrentRoom = floor.FloorMap[rpos1, rpos2];
            }
        }
        
        
        
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
            NotificationCenter.Instance.AddObserver("PlayerMovedRooms", PlayerMovedRooms);
            NotificationCenter.Instance.AddObserver("PlayerEnteredElevator", PlayerEnteredElevator);
            NotificationCenter.Instance.AddObserver("ItemObtained", ItemObtained);
            NotificationCenter.Instance.AddObserver("YouWinGenocide", YouWinGenocide);
            NotificationCenter.Instance.AddObserver("YouWinTrue", YouWinTrue);
        }
        public void PlayerMovedRooms(Notification notification)
        {
            Player player = (Player)notification.Object;
            if (player != null)
            {
                player.NormalMessage("\n" + "Player moved rooms.");
            }
            else
            {
                player.ErrorMessage("\n" + "Player did not move!");
            }
        }

        public void PlayerEnteredElevator(Notification notification)
        {
            Player player = (Player)notification.Object;
            if (player != null)
            {
                player.NormalMessage("\n" + "Player entered elevator.");
            }
            else
            {
                player.ErrorMessage("\n" + "Player has not entered the elevator");
            }
        }
        public void ItemObtained(Notification notification)
        {
            TakeCommand take = (TakeCommand)notification.Object;
            if (take != null)
            {
                Console.WriteLine("\n" + "Player obtained a new item.");
                //NormalMessage("\n" + "Player obtained a new item.");
            }
            else
            {
                Console.WriteLine("\n" + "Item unclaimed.");
                //ErrorMessage("\n" + "Item unclaimed.");
            }
        }
        public void YouWinGenocide(Notification notification)
        {
            DescendCommand descend = (DescendCommand)notification.Object;
            if (descend != null)
            {
                Console.WriteLine("\n" + "Achievement Unlocked: Devil inside awakened.");
                //NormalMessage("\n" + "Player obtained a new item.");
            }
            else
            {
                Console.WriteLine("\n" + "Player has not yet reached Monster status.");
                //ErrorMessage("\n" + "Item unclaimed.");
            }
        }
        public void YouWinTrue(Notification notification)
        {
            AscendCommand aescend = (AscendCommand)notification.Object;
            if (aescend != null)
            {
                Console.WriteLine("\n" + "Achievement Unlocked: Congratulations, you have ascended. The cycle has officially been broken.");
                //NormalMessage("\n" + "Player obtained a new item.");
            }
            else
            {
                Console.WriteLine("\n" + "Player has not yet ascended.");
                //ErrorMessage("\n" + "Item unclaimed.");
            }
        }

        private Floor CreateWorld()
        {
            abandonedHospital = new Floor(Floor1Rooms, Floor1Items);
            abandonedHospital.Unlocked = true;
            abandonedSchool = new Floor(Floor1Rooms, Floor1Items);
            hell = new Floor(Floor1Rooms, Floor1Items);
            winZone = new Floor(Floor1Rooms, Floor1Items);
            winZone.Unlocked = true;
            Character AB = new Character(abandonedHospital, "A", "B");
            Character BC = new Character(abandonedHospital, "B", "C");
            Character CD = new Character(abandonedHospital, "C", "D");
            Character DE = new Character(abandonedHospital, "D", "E");
            Character EF = new Character(abandonedHospital, "E", "F");
            Character FG = new Character(abandonedHospital, "F", "G");
            Character GH = new Character(abandonedHospital, "G", "H");
            Character HI = new Character(abandonedHospital, "H", "I");
            Character IJ = new Character(abandonedHospital, "I", "J");
            Character JK = new Character(abandonedHospital, "J", "K");
            A_hospitalCharacters.Add(AB);
            A_hospitalCharacters.Add(BC);
            A_hospitalCharacters.Add(CD);
            A_hospitalCharacters.Add(DE);
            A_hospitalCharacters.Add(EF);
            A_hospitalCharacters.Add(FG);
            A_hospitalCharacters.Add(GH);
            A_hospitalCharacters.Add(HI);
            A_hospitalCharacters.Add(IJ);
            A_hospitalCharacters.Add(JK);
            GenCharacters(abandonedHospital, A_hospitalCharacters, U_hospitalCharacters);
            floors = new List<Floor>();
            floors.Add(abandonedHospital);
            floors.Add(abandonedSchool);
            floors.Add(hell);
            floors.Add(winZone);
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

