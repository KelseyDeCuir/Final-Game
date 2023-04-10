using System;
using System.Collections.Generic;
using System.Text;

namespace Ascension
{
   public class ElevatorAttendant : Character
    {
        public string Name { set; get; }
        public string Description { get; }
        public Item[] Inventory { set; get; }
        public Double Health { set; get; }
        public Double Attack { set; get; }
        public Double Evasiveness { set; get; }
        public Boolean CanMove { set; get; }
        public Boolean Alive { set; get; }
        public Skills skillName { set; get; }
        //Thinking about what to do with this
        public Command[] actions { set; get; }
        ElevatorAttendant(Floor floor) : base(floor)
        {

        }
    }
}
