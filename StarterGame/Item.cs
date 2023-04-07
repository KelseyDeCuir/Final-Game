using System;
using System.Collections.Generic;
using System.Text;

namespace Ascension
{
    public class Item : iItems
    {
        public String Name { set; get; }
        public String Description { set; get; }
        public int Value { set; get; }
        public int Weight { set; get; }
        public int Volume { set; get; }
    }
}
