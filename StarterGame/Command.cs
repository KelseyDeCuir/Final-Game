using System.Collections;
using System.Collections.Generic;
using System;

namespace StarterGame
{

    /*
     * Spring 2023
     */
    public abstract class Command
    {
        private string _name;
        public string Name { get { return _name; } set { _name = value; } }
        private string _secondWord;
        public string SecondWord { get { return _secondWord; } set { _secondWord = value; } }
        // Added a third word for multiword processing
        private string _thirdword;
        public string Thirdword { get { return _thirdword; } set { _thirdword = value; } }


        public Command()
        {
            this.Name = "";
            this.SecondWord = null;
            this.Thirdword = null;
        }

        public bool HasSecondWord()
        {
            return this.SecondWord != null;
        }
        public bool HasThirdWord()
        {
            return this.Thirdword != null;
        }

        public abstract bool Execute(Character player);
    }
}
