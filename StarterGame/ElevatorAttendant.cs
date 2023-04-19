using System;
using System.Collections.Generic;
using System.Text;

namespace Ascension
{
   public class ElevatorAttendant : Character //Should inherit from boss?
    {
        ElevatorAttendant(Floor floor, string name, string desc) : base(floor, name, desc, new int[] { 0, 0 })
        {

        }
    }
}
