using System.Collections;
using System.Collections.Generic;
using System;

namespace Ascension
{
    /*
     * Spring 2023
     */
    public class CommandWords
    {
        private static Command[] _commandArrayInMenu = { new QuitCommand() };
        private static Command[] _commandArrayInGame = { new GoCommand(), new QuitCommand(), new ReflectCommand(), new BackCommand(), new MenuCommand() };
        private static Command[] _commandArrayInCombat = { new QuitCommand() };
        private static Command[] _commandArrayInDialogue = { new QuitCommand() };
        private static Dictionary<States, Command[]> _commandArrays = new Dictionary<States, Command[]>() { { States.MENU, _commandArrayInMenu }, { States.GAME, _commandArrayInGame }, { States.COMBAT, _commandArrayInCombat }, { States.DIALOGUE, _commandArrayInDialogue } };
        private Dictionary<States, Dictionary<string, Command>> _commands;

        public CommandWords() : this(_commandArrays) {}

        // Designated Constructor
        public CommandWords(Dictionary<States, Command[]> commandlist)
        {
            _commands = new Dictionary<States, Dictionary<string, Command>>();
            _commands[States.MENU] = new Dictionary<string, Command>();
            _commands[States.GAME] = new Dictionary<string, Command>();
            _commands[States.COMBAT] = new Dictionary<string, Command>();
            _commands[States.DIALOGUE] = new Dictionary<string, Command>();
            foreach (States state in Enum.GetValues(typeof(States))) {
                foreach (Command command in commandlist[state])
                {
                    _commands[state][command.Name] = command;
                }
            Command help = new HelpCommand(this);
            _commands[state][help.Name] = help;
        }
        }

        public Command Get(States state, string word)
        {
            Command command = null;
            _commands[state].TryGetValue(word, out command);
            return command;
        }

        public string Description(States states)
        {
            string commandNames = "";
            Dictionary<string, Command>.KeyCollection keys = _commands[states].Keys;
            foreach (string commandName in keys)
            {
                commandNames += " " + commandName;
            }
            return commandNames;
        }
    }
}
