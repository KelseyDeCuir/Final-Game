using System;
using System.Collections.Generic;
using System.Text;

namespace Ascension
{
   public class ElevatorAttendant : Character //Should inherit from boss?
    {
        String txtfile;
        ElevatorAttendant(Floor floor, string name, string desc, string txtfile) : base(floor, name, desc, new int[] { 0, 0 }) 
        {



        }
    }
}
