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
        static Dictionary<Command, double> agroCommands = new Dictionary<Command, double>() { { new GoCommand(), 0.25 }, { new CHitCommand(), 0.5 } };
        static Dictionary<Command, double> kindCommands = new Dictionary<Command, double>() { { new GoCommand(), 0.25 } };
        static Dictionary<Command, double> braveCommands = new Dictionary<Command, double>() { { new GoCommand(), 0.25 }, {new CHitCommand(), 0.65 } };
       
        static Dictionary<Command, double> cowardCombatCommands = new Dictionary<Command, double>() { { new AttackCommand(), 1.0 } };
        static Dictionary<Command, double> agroCombatCommands = new Dictionary<Command, double>() { { new CHitCommand(), 0.5 }, { new AttackCommand(), 1.0 } };
        static Dictionary<Command, double> kindCombatCommands = new Dictionary<Command, double>() { { new AttackCommand(), 1.0 } };
        static Dictionary<Command, double> braveCombatCommands = new Dictionary<Command, double>() { { new CHitCommand(), 0.65 }, { new AttackCommand(), 1.0 } };
        Personality personality1;
        [Newtonsoft.Json.JsonIgnore]
        Dictionary<Personality,Dictionary<States, Dictionary<Command, double>>> personalities = new Dictionary<Personality, Dictionary<States, Dictionary<Command, double>>>() { { Personality.COWARD, new Dictionary<States, Dictionary<Command, double>>() { { States.GAME, cowardCommands }, { States.COMBAT, cowardCombatCommands } } },{ Personality.AGRESSIVE, new Dictionary<States, Dictionary<Command, double>>() { { States.GAME, agroCommands }, { States.COMBAT, agroCombatCommands } } }, { Personality.KIND, new Dictionary<States, Dictionary<Command, double>>() { { States.GAME, kindCommands }, { States.COMBAT, kindCombatCommands } } }, { Personality.BRAVE, new Dictionary<States, Dictionary<Command, double>>() {{States.GAME, braveCommands }, { States.COMBAT, braveCombatCommands } } } };
        public Dictionary<States,Dictionary<Command, double>> _commands;
        Random rnd = new Random();
        [Newtonsoft.Json.JsonIgnore]
        public Dictionary<Personality, Dictionary<States, Dictionary<Command, double>>> personalities = new Dictionary<Personality, Dictionary<States, Dictionary<Command, double>>>() { { Personality.COWARD, new Dictionary<States, Dictionary<Command, double>>() { { States.GAME, cowardCommands } } }, { Personality.AGRESSIVE, new Dictionary<States, Dictionary<Command, double>>() { { States.GAME, agroCommands } } }, { Personality.KIND, new Dictionary<States, Dictionary<Command, double>>() { { States.GAME, kindCommands } } }, { Personality.BRAVE, new Dictionary<States, Dictionary<Command, double>>() { { States.GAME, braveCommands } } } };
        [System.Runtime.Serialization.OnDeserialized]
        void OnDeserialized(System.Runtime.Serialization.StreamingContext context)
        {
            _commands = personalities[personality1];
        }

        public Command AIcommand(States state) { 

            double check = rnd.NextDouble();
            if (state == States.GAME)
            {
                foreach (Command command in _commands[state].Keys)
                {
                    if (_commands[state][command] > check)
                    {
                        return command;
                    }
                }
            }
            else if (state == States.COMBAT)
            {
                foreach (Command command in _commands[state].Keys)
                {
                    if (_commands[state][command] > check)
                    {
                        return command;
                    }
                }
            }
            return new NullCommand();
        }
        public CPersonality(Personality personality)
        {
            _commands = personalities[personality];
            personality1 = personality;
        }
    }

}
