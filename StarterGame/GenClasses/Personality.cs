using System;
using System.Collections.Generic;
using System.Text;

namespace Ascension
{
    public enum Personality
    {
        COWARD, AGRESSIVE, KIND, BRAVE
    }
    public class CPersonality
    {
        static Dictionary<Command, double> cowardCommands = new Dictionary<Command, double>() { {new GoCommand(), 0.25 } };
        static Dictionary<Command, double> agroCommands = new Dictionary<Command, double>() { { new GoCommand(), 0.25 } };
        static Dictionary<Command, double> kindCommands = new Dictionary<Command, double>() { { new GoCommand(), 0.25 } };
        static Dictionary<Command, double> braveCommands = new Dictionary<Command, double>() { { new GoCommand(), 0.25 } };

        Dictionary<Personality, Dictionary<Command, double>> personalities = new Dictionary<Personality, Dictionary<Command, double>>() { { Personality.COWARD, cowardCommands }, { Personality.AGRESSIVE, agroCommands }, { Personality.KIND, kindCommands }, { Personality.BRAVE, braveCommands } };
        private Dictionary<Command, double> _commands;
        Random rnd = new Random();
        public Command AIcommand { get { double check = rnd.NextDouble(); foreach (Command command in _commands.Keys) { if (_commands[command] > check){ return command; } } return new NullCommand(); } }
        public CPersonality(Personality personality)
        {
            _commands = personalities[personality];
        }
    }

}
