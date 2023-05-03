using System;
using System.Collections.Generic;
using System.Text;

namespace Ascension
{
    
    //Elevator attendant is also the final boss so it will have special abilities
    //todo: add ability to trade 
    public class ElevatorAttendant : Character 
    {
        private static ElevatorAttendant _instance = null;
        public static ElevatorAttendant Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ElevatorAttendant(GameWorld.Instance);
                }
                return _instance;
            }
        }
        public ElevatorAttendant(GameWorld world) : base(world.Entrance, "Steve", "A creepy elevator operator with an inhuman smile.")
        {
            CurrentRoom = Elevator.Instance;
            Inventory = new List<Item>();
            State = States.ELEVATOR;
            aptitudeLvl = new Skills(50, 500, 50, 50, 50); //Edit to fit combat system

        }
        public void NameAttendant(string name)
        {
            this._name = name;
        }
    }
}
