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
        public List<Character> Bosses = new List<Character>();

        public void GenCharacters(Floor floor, List<Character> A, List<Character> U)
        {
            Random rnd = new Random();
            int charsToGen = rnd.Next(1, A.Count);
            for (int i = 0; i < charsToGen; i++)
            {
                int index = rnd.Next(0, A.Count);
                while (U.Contains(A[index]))
                {
                    index = rnd.Next(0, A.Count);
                }
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



        Weapon rustyScalpel = new Weapon("rusty-scalpel", "A rusted surgery tool.", 5, 1, 1, 8);
        Weapon rubberGloves = new Weapon("rubber-gloves", "A pair of rubber gloves.", 6, 2, 2, 6);
        Armor scrubs = new Armor("scrubs", "No, I know, I'm no superman.", 4, 3, 5, 5);
        Armor paperJacket = new Armor("paper-jacket", "You're so c- co- cold!", 3, 1, 4, 3);
        Item mdID = new Item("md-id", "(Key) An M.D.'s ID Card. This can get you anywhere in here...", 0, 1, 1);
        Item jID = new Item("janitor-id", "(Key) A janitor's ID Card. Maybe this can get you into the south-west room.", 0, 1, 1);

        Weapon metalYardstick = new Weapon("yardstick", "A sharp metal yardstick.", 9, 3, 3, 12);
        Weapon whip = new Weapon("extension-cords", "Several extension cords braided together like a whip.", 10, 5, 3, 13);
        Armor tweedCoat = new Armor("tweed-coat", "A tweed coat with patches on the elbows.", 7, 3, 4, 8);
        Armor suitJacket = new Armor("suit", "A nice suit jacket.", 8, 2, 4, 7);
        Item masterKey = new Item("master-key", "(Key) The one key to rule them all, and in the darkness bind them. Or however that goes.", 0, 1, 2);
        Item lanyard = new Item("lanyard", "(Key) Lanyard of keys, this can probably get you into that locked room on the East edge.", 0, 3, 2);

        Weapon pitchfork = new Weapon("pitchfork", "A large red pitchfork. Stereotypical if you ask me.", 14, 5, 4, 18);
        Weapon blade = new Weapon("blade", "The Blade of Abaddon the Destroyer.", 15, 4, 5, 20);
        Armor cloak = new Armor("cloak", "A cloak of hellfire.", 11, 1, 6, 14);
        Armor wings = new Armor("wings", "Samael's wings. You hope he hasn't been looking for these...", 10, 4, 9, 18);
        Item rune = new Item("rune", "(key) Rune of ▓░▒░▓▒.", 0, 5, 1);
        Item sigil = new Item("sigil", "(key) A Sigil of sins", 0, 2, 2);


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
        private GameWorld()
        {
            _entrance = CreateWorld();
            NotificationCenter.Instance.AddObserver("PlayerMovedRooms", PlayerMovedRooms);
            NotificationCenter.Instance.AddObserver("PlayerEnteredElevator", PlayerEnteredElevator);
            NotificationCenter.Instance.AddObserver("ItemObtained", ItemObtained);
            NotificationCenter.Instance.AddObserver("YouWinGenocide", YouWinGenocide);
            NotificationCenter.Instance.AddObserver("YouWinTrue", YouWinTrue);
            NotificationCenter.Instance.AddObserver("CharactersInRoom", CharactersInRoom);
            NotificationCenter.Instance.AddObserver("SuccessfulCommand", SuccessfulCommand);
        }

        public void SuccessfulCommand(Notification notification)
        {
            foreach(Character character in U_hospitalCharacters)
            {
                character.AI();
            }
            foreach (Character character in U_schoolCharacters)
            {
                character.AI();
            }
            foreach (Character character in U_hellCharacters)
            {
                character.AI();
            }
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
            Floor1Items.Add(rustyScalpel);
            Floor1Items.Add(rubberGloves);
            Floor1Items.Add(scrubs);
            Floor1Items.Add(paperJacket);
            Floor1Items.Add(mdID);
            Floor1Items.Add(jID);

            Floor2Items.Add(metalYardstick);
            Floor2Items.Add(whip);
            Floor2Items.Add(tweedCoat);
            Floor2Items.Add(suitJacket);
            Floor2Items.Add(masterKey);
            Floor2Items.Add(lanyard);

            Floor3Items.Add(pitchfork);
            Floor3Items.Add(blade);
            Floor3Items.Add(cloak);
            Floor3Items.Add(wings);
            Floor3Items.Add(rune);
            Floor3Items.Add(sigil);
            List<Room> Floor1Rooms = new List<Room>() { new Room("Patient 1A", "The room reeks of death, but there is no body to be found.", Floor1Items), new Room("Patient 1B", "The patiet's bed is overturned and things are strewn across the floor.", Floor1Items), new Room("ER Lobby", "The Emergancy Room is probably far cleaner now than it ever was during operation.", Floor1Items), new Room("OR-1", "Operating Room 1 was luckily not recently used before whatever happened here.", Floor1Items), new Room("Radiology Screening", "The waiting room in the radiology department has a lot of mattressless cots, it might have been a good place for a kip otherwise.", Floor1Items), new Room("R4 X-RAY", "The X-RAY machine is broken... you hope.", Floor1Items), new Room("Pediactrics Lobby", "There's some poor souls bunny rabbit fused to the floor here.", Floor1Items), new Room("OBGYN Lobby", "The chairs in the room have been splintered into so many pieces.", Floor1Items) };

            List<Room> Floor2Rooms = new List<Room>() { new Room("Classroom 101", "A plain classroom, probably taught young kids.", Floor2Items), new Room("Classroom 102", "A messy classroom, definitely taught young kids.", Floor2Items), new Room("Classroom 103", "Either the kids were well behaved or the teacher was very strict, this classroom is incredibly tidy.", Floor2Items), new Room("Classroom 104", "This classroom was likely for the older kids. This based almost entirely on the amount of pencils still stuck in the ceiling.", Floor2Items), new Room("Administrator's Office", "The Office is amess of papers, out of a sense of privacy you avoid reading them.", Floor2Items), new Room("Teachers' Lounge", "You have the feeling this room isn't supposed to smell this strongly of booze.", Floor2Items), new Room("Locker Room", "Still smells sweaty... Ew.", Floor2Items), new Room("Quad", "The grass out here is real enough, dead though. Looking up you can see sky painted on the ceiling, with the paint chipping away. There's a busted bulb of a sunlamp.", Floor2Items) };

            List<Room> Floor3Rooms = new List<Room>() { new Room("Sulfer Pits", "Stinks of rotten eggs.", Floor3Items), new Room("Bridge of Doom", "Yep. That hole does not have a bottem, good thing the bridge is there.", Floor3Items), new Room("Dunes of Despair", "These sandy dunes cry out in the anguish of the souls trapped within.", Floor3Items), new Room("Lava Mount", "HOT!!!!!!!!", Floor3Items), new Room("Abandoned Elevator", "This room looks strangely similar to the one that you've been travellig around in...", Floor3Items), new Room("Shattered Bone Fields", "it's in the name really. You try to watch your step.", Floor3Items), new Room("Sanguine Sea", "A sea of blood rages below you, a wooden dock leads to the other exits.", Floor3Items), new Room("Infinite Factory", "The machinery is so loud, the hellscape of dangerous moving machines continues outwards for ages.", Floor3Items) };


        MonsterBuilder hospitalBuilder = new HospitalGhost();
            MonsterBuilder schoolBuilder = new PossessedJanitor();
            MonsterBuilder hellBuilder = new HellDemon();
            abandonedHospital = new Floor(Floor1Rooms, Floor1Items, hospitalBuilder);
            abandonedHospital.Unlocked = true;
            abandonedHospital.FloorMap[0, 2].items.Add(mdID);
            abandonedHospital.FloorMap[0, 2].MakeLockedRoom(jID.Name);
            abandonedHospital.FloorMap[1, 2].MakeBossRoom(mdID.Name);
            Room room = new Room("Blank", "blank test", Floor1Items);
             List<Room> blankRoomLists = new List<Room>() { room, room, room, room, room, room, room, room, room };
            abandonedSchool = new Floor(Floor2Rooms, Floor2Items, schoolBuilder);
            abandonedSchool.FloorMap[1, 1].items.Add(masterKey);
            abandonedSchool.FloorMap[1, 1].MakeLockedRoom(lanyard.Name);
            abandonedSchool.FloorMap[1, 2].MakeBossRoom(masterKey.Name);
            hell = new Floor(Floor3Rooms, Floor3Items, hellBuilder);
            hell.FloorMap[0, 2].items.Add(rune);
            hell.FloorMap[0, 2].MakeLockedRoom(sigil.Name);
            hell.FloorMap[1, 2].MakeBossRoom(rune.Name);
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
            Character Boss1 = new Character(abandonedHospital, "Hospital Boss", "Hospital boss desc");
            Boss1.CurrentRoom = Boss1.CurrentFloor.FloorMap[1, 2];
            Boss1.MakeBoss(abandonedSchool, 10);
            Character Boss2 = new Character(abandonedSchool, "School Boss", "School boss desc");
            Boss2.CurrentRoom = Boss2.CurrentFloor.FloorMap[1, 2];
            Boss2.MakeBoss(hell, 25);
            Character Servant = new Character(hell, "Servant", "Servant of ▓░▒░▓▒. They look like you, except for the eyes. Please not the EYES.");
            Servant.CurrentRoom = Servant.CurrentFloor.FloorMap[1, 2];
            Servant.MakeBoss(winZone, 50);
            Bosses.Add(Boss1);
            Bosses.Add(Boss2);
            Bosses.Add(Servant);
            //todo: either characters have lowercase names or talk command is change to check for names as lower case or parser is changes

    



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