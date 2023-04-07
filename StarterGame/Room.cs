using System.Collections;
using System.Collections.Generic;
using System;

namespace Ascension
{
    /*
     * Spring 2023
     */
    public class Room
    {
        private Dictionary<string, Room> _exits;
        private string _tag;
        private string _generaldescription;
        public string Tag { get { return _tag; } set { _tag = value; } }
        public string GeneralDescription { get { return _generaldescription; } set { _generaldescription = value; } }

        public Room() : this("No Tag", "No Description"){}

        // Designated Constructor
        public Room(string tag, string description)
        {
            _exits = new Dictionary<string, Room>();
            this.Tag = tag;
            this.GeneralDescription = description;

        }

        public void SetExit(string exitName, Room room)
        {
            _exits[exitName] = room;
        }

        public Room GetExit(string exitName)
        {
            Room room = null;
            _exits.TryGetValue(exitName, out room);
            return room;
        }

        public string GetExits()
        {
            string exitNames = "Exits: ";
            Dictionary<string, Room>.KeyCollection keys = _exits.Keys;
            foreach (string exitName in keys)
            {
                exitNames += " " + exitName;
            }

            return exitNames;
        }

        public string Description()
        {
            return "You are in " + this.Tag +". " + this.GeneralDescription + ".";
        }
        public string BaseDescription()
        {
            return "You are in " + this.Tag + ".\n ";
        }
    }
}
