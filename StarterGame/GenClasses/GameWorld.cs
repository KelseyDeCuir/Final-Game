﻿using System;
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
                Console.WriteLine("Characters are being generated.");
            }
        }
        
        
        
        Weapon rustyScalpel = new Weapon("rusty-scalpel", "A rusted surgery tool.", 5, 1, 1, 8, Floor1Items);
        Weapon rubberGloves = new Weapon("rubber-gloves", "A pair of rubber gloves.", 6, 2, 2, 6, Floor1Items);
        Armor scrubs = new Armor("scrubs", "No, I know, I'm no superman.", 4, 3, 5, 5, Floor1Items);
        Armor paperJacket = new Armor("paper-jacket", "You're so c- co- cold!", 3, 1, 4, 3, Floor1Items);
        Item mdID = new Item("md-id", "(Key) An M.D.'s ID Card. This can get you anywhere in here...", 0, 1, 1, Floor1Items);
        Item jID = new Item("janitor-id", "(Key) A janitor's ID Card. Maybe this can get you into the south-west room.", 0, 1, 1, Floor1Items);

        Weapon metalYardstick = new Weapon("yardstick", "A sharp metal yardstick.", 9, 3, 3, 12, Floor2Items);
        Weapon whip = new Weapon("extension-cords", "Several extension cords braided together like a whip.", 10, 5, 3, 13, Floor2Items);
        Armor tweedCoat = new Armor("tweed-coat", "A tweed coat with patches on the elbows.", 7, 3, 4, 8, Floor2Items);
        Armor suitJacket = new Armor("suit", "A nice suit jacket.", 8, 2, 4, 7, Floor2Items);
        Item masterKey = new Item("master-key", "(Key) The one key to rule them all, and in the darkness bind them. Or however that goes.", 0, 1, 2, Floor2Items);
        Item lanyard = new Item("lanyard", "(Key) Lanyard of keys, this can probably get you into that locked room on the East edge.", 0, 3, 2, Floor2Items);

        Weapon pitchfork = new Weapon("pitchfork", "A large red pitchfork. Stereotypical if you ask me", 14, 5, 4, 18, Floor3Items);

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
        private Room[] Floor1Rooms = { new Room("Patient 1A", "The room reeks of death, but there is no body to be found.", Floor1Items), new Room("Patient 1B","The patiet's bed is overturned and things are strewn across the floor.", Floor1Items), new Room("ER Lobby","The Emergancy Room is probably far cleaner now than it ever was during operation.",Floor1Items), new Room("OR-1", "Operating Room 1 was luckily not recently used before whatever happened here.", Floor1Items), new Room("Radiology Screening", "The waiting room in the radiology department has a lot of mattressless cots, it might have been a good place for a kip otherwise.", Floor1Items), new Room("R4 X-RAY","The X-RAY machine is broken... you hope.", Floor1Items), new Room("Pediactrics Lobby", "There's some poor souls bunny rabbit fused to the floor here.", Floor1Items), new Room("OBGYN Lobby", "The chairs in the room have been splintered into so many pieces.", Floor1Items) };

        private Room[] Floor2Rooms = {new Room("Classroom 101","A plain classroom, probably taught young kids.", Floor2Items), new Room("Classroom 102", "A messy classroom, definitely taught young kids.", Floor2Items), new Room("Classroom 103", "Either the kids were well behaved or the teacher was very strict, this classroom is incredibly tidy.", Floor2Items), new Room("Classroom 104", "This classroom was likely for the older kids. This based almost entirely on the amount of pencils still stuck in the ceiling.", Floor2Items), new Room("Administrator's Office", "The Office is amess of papers, out of a sense of privacy you avoid reading them.", Floor2Items), new Room("Teachers' Lounge", "You have the feeling this room isn't supposed to smell this strongly of booze.", Floor2Items), new Room("Locker Room", "Still smells sweaty... Ew.", Floor2Items), new Room("Quad", "The grass out here is real enough, dead though. Looking up you can see sky painted on the ceiling, with the paint chipping away. There's a busted bulb of a sunlamp.", Floor2Items) };

        private Room[] Floor3Rooms = { new Room("Sulfer Pits", "Stinks of rotten eggs.", Floor3Items), new Room("Bridge of Doom", "Yep. That hole does not have a bottem, good thing the bridge is there.", Floor3Items), new Room("Dunes of Despair", "These sandy dunes cry out in the anguish of the souls trapped within.", Floor3Items), new Room("Lava Mount", "HOT!!!!!!!!", Floor3Items), new Room("Abandoned Elevator", "This room looks strangely similar to the one that you've been travellig around in...", Floor3Items), new Room("Shattered Bone Fields", "it's in the name really. You try to watch your step.", Floor3Items), new Room("Sanguine Sea", "A sea of blood rages below you, a wooden dock leads to the other exits.", Floor3Items), new Room("Infinite Factory", "The machinery is so loud, the hellscape of dangerous moving machines continues outwards for ages.", Floor3Items) };
        private GameWorld()
        {
            _entrance = CreateWorld();
            NotificationCenter.Instance.AddObserver("PlayerMovedRooms", PlayerMovedRooms);
            NotificationCenter.Instance.AddObserver("PlayerEnteredElevator", PlayerEnteredElevator);
            NotificationCenter.Instance.AddObserver("ItemObtained", ItemObtained);
            NotificationCenter.Instance.AddObserver("YouWinGenocide", YouWinGenocide);
            NotificationCenter.Instance.AddObserver("YouWinTrue", YouWinTrue);
            NotificationCenter.Instance.AddObserver("CharactersInRoom", CharactersInRoom);
            NotificationCenter.Instance.AddObserver("PlayerEndedDialouge", PlayerEndedDialouge);
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
            Player player  = (Player)notification.Object;
            if (player != null)
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
        public void CharactersInRoom(Notification notification)
        {
            GameWorld gw = (GameWorld)notification.Object;
            if (gw != null)
            {
                Console.WriteLine("\n" + "Characters: " + U_hospitalCharacters);
                //character.NormalMessage("\n" + "Characters: " + U_hospitalCharacters);
            }
            else
            {
                Console.WriteLine("\n" + "Characters list unavaliable.");
                //character.ErrorMessage("\n" + "Characters list unavaliable.");
            }

        }

        //TODO: PLAYER OPTIONS
        public void PlayerEndedDialouge(Notification notification)
        {
            PlayerOptions playeroptions = (PlayerOptions)notification.Object;
            if (playeroptions != null)
            {
                Console.WriteLine("\n" + "Player ended dialouge");
            }
            else
            {
                Console.WriteLine("\n" + "Player did not end dialouge");
            }
        }

        private Floor CreateWorld()
        {
            MonsterBuilder hospitalBuilder = new HospitalGhost();
            MonsterBuilder schoolBuilder = new HospitalGhost();
            MonsterBuilder hellBuilder = new HospitalGhost();
            abandonedHospital = new Floor(Floor1Rooms, Floor1Items, hospitalBuilder);
            abandonedHospital.Unlocked = true;
            abandonedHospital.FloorMap[0, 2].items.Add(mdID);
            abandonedHospital.FloorMap[0, 2].MakeLockedRoom(jID.Name);
            abandonedHospital.FloorMap[1, 2].MakeBossRoom(mdID.Name);
            Room room = new Room("Blank", "blank test", Floor1Items);
            Room [] blankRoomLists = new Room[] {room, room, room, room, room, room, room, room, room };
            abandonedSchool = new Floor(Floor2Rooms, Floor2Items, schoolBuilder);
            abandonedSchool.FloorMap[1, 1].items.Add(masterKey);
            abandonedSchool.FloorMap[1, 1].MakeLockedRoom(lanyard.Name);
            abandonedSchool.FloorMap[1, 2].MakeBossRoom(masterKey.Name);
            hell = new Floor(Floor3Rooms, Floor3Items, hellBuilder);
            winZone = new Floor(blankRoomLists, Floor1Items, hospitalBuilder);
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
            GenCharacters(abandonedHospital, A_hospitalCharacters, U_hospitalCharacters); //Generates characters
            Notification notification = new Notification("CharactersInRoom", this);
            NotificationCenter.Instance.PostNotification(notification);
            floors = new List<Floor>();
            floors.Add(abandonedHospital);
            floors.Add(abandonedSchool);
            floors.Add(hell);
            floors.Add(winZone);
            return abandonedHospital;
        }
    }
} 