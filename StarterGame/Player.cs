using System;
using System.Collections.Generic;
using System.Text;

namespace Ascension
{
    public class Player : Character
    {
        private double _weightLimit;
        public double WeightLimit { get { return _weightLimit; }}
        private double _volumeLimit;
        public double VolumeLimit { get { return _volumeLimit; }}
        public Player(Floor floor, string name, string desc) : base(floor, name, desc)
        {
            _volumeLimit = 40;
            _weightLimit = 30;
        }
    }
}
