using System;
using System.Collections.Generic;
using System.Text;

namespace Ascension
{
    enum Personality
    {
        COWARD, AGRESSIVE, KIND, BRAVE
    }
    public class CPersonality
    {
        static Dictionary<Command, double> cowardCommands = new Dictionary<Command, double>() { };
        static Dictionary<Command, double> agroCommands = new Dictionary<Command, double>() { };
        static Dictionary<Command, double> kindCommands = new Dictionary<Command, double>() { };
        static Dictionary<Command, double> braveCommands = new Dictionary<Command, double>() { };

        Dictionary<Personality, Dictionary<Command, double>> personalities = new Dictionary<Personality, Dictionary<Command, double>>() { { Personality.COWARD, cowardCommands }, { Personality.AGRESSIVE, agroCommands }, { Personality.KIND, kindCommands }, {Personality.BRAVE, braveCommands } };
    }

}
